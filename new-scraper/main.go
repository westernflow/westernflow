package main

// scraper that scrapes
// this page https://westerncalendar.uwo.ca/Courses.cfm?SelectedCalendar=Live&ArchiveID=
import (
	"fmt"
	connector "scraper/db"
	"scraper/models"
	"strconv"
	"strings"
	"sync"

	colly "github.com/gocolly/colly"
	_ "github.com/lib/pq"
)

var wg sync.WaitGroup
var mu sync.Mutex
var courses []models.ScrapedCourse
var faculties map[string][]string = make(map[string][]string)

func configureCollyForBreadth(c *colly.Collector) *colly.Collector {

	c.OnHTML("table", func(e *colly.HTMLElement) {

		e.ForEach("a", func(_ int, el *colly.HTMLElement) {
			// find href attribute
			href := el.Attr("href")
			faculty := strings.Split(href, "=")[1]
			faculty = strings.Split(strings.TrimSpace(faculty), "&")[0]
			res := strings.TrimSpace(el.DOM.Parent().Parent().Text())
			if strings.Contains(res, "CATEGORY") {
				cats := strings.Split(res, "CATEGORY")
				// trim the spaces from cats
				for i, cat := range cats {
					cats[i] = strings.TrimSpace(cat)
				}
				faculties[faculty] = append(faculties[faculty], cats[1:]...)
			}
		})
	})

	return c
}

func configureCollyForCourses(c *colly.Collector) *colly.Collector {
	// On every a element which has href attribute call callback
	c.OnHTML("div.panel-group", func(e *colly.HTMLElement) {
		var course models.ScrapedCourse
		e.ForEach("strong", func(_ int, el *colly.HTMLElement) {
			divText := strings.TrimSpace(el.DOM.Parent().Text())
			bold := strings.TrimSpace(el.Text)
			switch {
			case strings.Contains(divText, "Antirequisite(s): "):
				course.Antireqs = removeMatchedText(divText, bold)
			case strings.Contains(divText, "Prerequisite(s): ") || strings.Contains(divText, "Corequisite(s): "):
				switch {
				case strings.Contains(divText, "Prerequisite(s): ") && !strings.Contains(divText, "Corequisite(s): "):
					course.Prereqs = removeMatchedText(divText, bold)
				case strings.Contains(divText, "Corequisite(s): ") && !strings.Contains(divText, "Prerequisite(s): "):
					course.CoReqs = removeMatchedText(divText, bold)
				default:
					// handling the parsing for both prereqs and coreqs.
					// also grab the internal ids of each link in the string.
				}

			case strings.Contains(divText, "Course Weight:"):
				res := strings.TrimSpace(removeMatchedText(divText, bold))
				if res == "" {
					break
				}
				val, err := strconv.ParseFloat(res, 64)
				if err != nil {
					fmt.Println("Failed to parse course weight:", err)
				}
				course.Weight = val
			case strings.Contains(divText, "Extra Information: "):
				course.ExtraInfo = removeMatchedText(divText, bold)
			}
			id := e.Attr("id")
			course.InternalID = strings.Split(id, "_")[2]
		})

		panel := e.DOM.Find("div.panel-body")
		// in the panel look for div class=" col-xs-12"
		desc := panel.Find("div.col-xs-12").First()
		course.Description = strings.TrimSpace(desc.Text())

		// find the div that has panel-heading class and get the text
		e.ForEach("div.panel-heading", func(_ int, el *colly.HTMLElement) {
			course.CourseName = strings.TrimSpace(el.Text)
		})
		faculty := e.Request.URL.Query().Get("Subject")
		course.BreadthCategory = strings.Join(faculties[faculty], ", ")
		if course.InternalID != "" {
			// course internalid must be unique
			mu.Lock()
			courses = append(courses, course)
			mu.Unlock()
		}
		// fmt.Println(course.InternalID)
	})

	return c
}

func main() {
	fmt.Println("Starting the scraper...")
	base_url := "https://westerncalendar.uwo.ca/Courses.cfm"

	// // https://westerncalendar.uwo.ca/Courses.cfm?Subject=ACTURSCI&SelectedCalendar=Live&ArchiveID=
	c := colly.NewCollector()
	c = configureCollyForBreadth(c)
	c.Visit(base_url)

	c = configureCollyForCourses(c)

	// use a go routine to open each page and print the page title
	// use only the first 3 faculties for testing
	faculties = make(map[string][]string)
	faculties["ACTURSCI"] = []string{"CATEGORY A"}

	for faculty, BreadthCategory := range faculties {
		wg.Add(1)
		go func(faculty string, BreadthCategory []string, courses *[]models.ScrapedCourse) {

			defer wg.Done()

			url := base_url + "?Subject=" + faculty
			err := c.Visit(url)

			if err != nil {
				fmt.Println("Failed to visit the page:", err)
				return
			}

		}(faculty, BreadthCategory, &courses)
	}
	wg.Wait()
	// remove duplicates based on course internal id
	courses = removeDuplicates(courses)

	fmt.Println("Found", len(courses), "courses")
	// print the first 5 courses
	for i := 0; i < 1; i++ {
		fmt.Println("Desc " + courses[i].Description)
		fmt.Println("Name " + courses[i].CourseName)
		fmt.Println("Antireqs " + courses[i].Antireqs)
		fmt.Println("Prereqs " + courses[i].Prereqs)
		fmt.Println("CoReqs " + courses[i].CoReqs)
		fmt.Println("Weight " + strconv.FormatFloat(courses[i].Weight, 'f', 6, 64))
		fmt.Println("ExtraInfo " + courses[i].ExtraInfo)
		fmt.Println("BreadthCategory " + courses[i].BreadthCategory)
		fmt.Println("InternalID " + courses[i].InternalID)
		fmt.Println("------------------------------------------------")
	}
	db := connector.ConnectToDB("LOCAL")
	defer db.Close()

	// // get existing courses from the database
	// existingCourses := connector.FetchExistingCourses(db)
	// fmt.Println("Found", len(existingCourses), "existing courses")
	// mergedCourses := joiner.MergeCourseDataSlices(existingCourses, courses)
	// fmt.Println("Merged", len(mergedCourses), "courses")
	// err := connector.UpdateCourses(db, mergedCourses)
	// if err != nil {
	// 	fmt.Println("Failed to update the database:", err)
	// 	return
	// }
	// fmt.Println("Successfully updated the database")
}

func removeMatchedText(original string, toRemove string) string {
	return strings.Replace(original, toRemove, "", -1)
}

func removeDuplicates(courses []models.ScrapedCourse) []models.ScrapedCourse {
	// Create a map of unique courses
	uniqueCourses := map[string]models.ScrapedCourse{}
	for _, course := range courses {
		uniqueCourses[course.InternalID] = course
	}

	// Convert the map to a slice
	uniqueCoursesSlice := []models.ScrapedCourse{}
	for _, course := range uniqueCourses {
		uniqueCoursesSlice = append(uniqueCoursesSlice, course)
	}

	return uniqueCoursesSlice
}

// get faculties from postgres db
func get_faculties() []string {
	// Connect to the database
	db := connector.ConnectToDB("LOCAL")
	defer db.Close()

	fmt.Println("Querying the database for faculties...")

	// Query the database
	rows, err := db.Query(`SELECT "Abbreviation" FROM public."Faculties"`)

	if err != nil {
		fmt.Println("Failed to query the database:", err)
		return nil
	}
	defer rows.Close()

	// Iterate over the rows and return the data
	faculties := []string{}
	for rows.Next() {
		var abbreviation string
		err = rows.Scan(&abbreviation)
		if err != nil {
			fmt.Println("Failed to scan the row:", err)
			return nil
		}
		faculties = append(faculties, abbreviation)
	}
	// print hte number of faculties found
	fmt.Println("Found", len(faculties), "faculties")

	return faculties
}
