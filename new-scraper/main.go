package main

// scraper that scrapes
// this page https://westerncalendar.uwo.ca/Courses.cfm?SelectedCalendar=Live&ArchiveID=
import (
	"fmt"
	"scraper/joiner"
	"scraper/models"
	"strconv"
	"strings"
	"sync"

	connector "scraper/db"

	colly "github.com/gocolly/colly"
	_ "github.com/lib/pq"
)

var wg sync.WaitGroup
var mu sync.Mutex
var courses []models.ScrapedCourse

func configureCollyForCourses(c *colly.Collector) *colly.Collector {
	// On every a element which has href attribute call callback
	c.OnHTML("div.panel-group", func(e *colly.HTMLElement) {
		var course models.ScrapedCourse
		e.ForEach("strong", func(_ int, el *colly.HTMLElement) {
			divText := strings.TrimSpace(el.DOM.Parent().Text())

			switch {
			case strings.Contains(divText, "Antirequisite(s): "):
				course.Antireqs = removeMatchedText(divText, "Antirequisite(s): ")
			case strings.Contains(divText, "Prerequisite(s): "):
				course.Prereqs = removeMatchedText(divText, "Prerequisite(s): ")
			case strings.Contains(divText, "Corequisite(s): "):
				course.CoReqs = removeMatchedText(divText, "Corequisite(s): ")
			case strings.Contains(divText, "Course Weight: "):
				res := removeMatchedText(divText, "Course Weight: ")
				val, err := strconv.ParseFloat(res, 64)
				if err != nil {
					fmt.Println("Failed to parse course weight:", err)
				}
				course.Weight = val
			case strings.Contains(divText, "Extra Information: "):
				course.ExtraInfo = removeMatchedText(divText, "Extra Information: ")
			}
			// the internal id is the id of the div that has class = panel-group
			// in the following format: accordion_MAIN_008534_1, we only want 008534

			id := e.Attr("id")
			course.InternalID = strings.Split(id, "_")[2]
		})

		// find the div that has panel-heading class and get the text
		e.ForEach("div.panel-heading", func(_ int, el *colly.HTMLElement) {
			course.CourseName = strings.TrimSpace(el.Text)
		})

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
	c = configureCollyForCourses(c)

	faculties := get_faculties()
	// faculties = faculties[:2]
	// only use the first faculty for testing
	// faculties := []string{"ACTURSCI", "ASL"}

	// use a go routine to open each page and print the page title
	for _, faculty := range faculties {
		wg.Add(1)
		go func(faculty string, courses *[]models.ScrapedCourse) {

			defer wg.Done()

			url := base_url + "?Subject=" + faculty
			err := c.Visit(url)

			if err != nil {
				fmt.Println("Failed to visit the page:", err)
				return
			}

		}(faculty, &courses)
	}
	wg.Wait()

	// remove duplicates based on course internal id
	courses = removeDuplicates(courses)

	fmt.Println("Found", len(courses), "courses")

	db := connector.ConnectToDB("LOCAL")
	defer db.Close()

	res, err := connector.WriteCoursesToDB(courses, db)

	if err != nil {
		fmt.Println("Failed to write courses to the database:", err)
		return
	}

	if res {
		fmt.Println("Successfully wrote courses to the database")
	} else {
		fmt.Println("Failed to write courses to the database")
	}

	// join the courses with newcourses, based on internal id, in a new table called
	// joinedcourses

	// create a new table called joinedcourses
	_, err = db.Exec(`CREATE TABLE IF NOT EXISTS public."JoinedCourses" ("CourseName" TEXT, "InternalID" VARCHAR(255) PRIMARY KEY, "Antireqs" TEXT, "Prereqs" TEXT, "CoReqs" TEXT, "Weight" FLOAT, "ExtraInfo" TEXT, "Faculty" TEXT)`)
	if err != nil {
		fmt.Println("Failed to create table:", err)
		return
	}

	joiner.JoinTables()

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
