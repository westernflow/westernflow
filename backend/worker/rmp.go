package worker

import (
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"strings"
	model "uwo-tt-api/model/domain"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
)

func PopulateRMP(db *mongo.Database) {
	// tempCollection := db.Collection("professors_temp")
	// finalCollection := db.Collection("professors")
	// drop temp

	// tempCollection.Drop(context.Background())

	fmt.Println("Populating RMP database...")
	data := getData()
	profs := data.Data.Search.Teachers.Edges
	cleanedProfs := []model.Professor{}
	for _, prof := range profs {
		prof := prof.Node
		// fmt.Println(prof)
		uwoReviews := []model.Review{}
		for _, review := range prof.Ratings.Edges {
			review := review.Node
			// create mongoReview
			mongoReview := model.Review{
				ProfessorID: prof.ID,
				Quality:     review.QualityRating,
				Clarity:     review.ClarityRating,
				Difficulty:  review.DifficultyRating,
				Helpful:     review.HelpfulRating,
				Date:        review.Date,
				ReviewText:  review.Comment,
			}
			uwoReviews = append(uwoReviews, mongoReview)
		}
		mongoProf := model.Professor{
			RMPName: prof.FirstName + " " + prof.LastName,
			Reviews: uwoReviews,
			RMPId:   prof.ID,
		}

		cleanedProfs = append(cleanedProfs, mongoProf)
	}

	// insert professors into database
	// insertProfessors(cleanedProfs, tempCollection)
	aggregateProfessors(db)
	// aggregate professors into courses
}

func aggregateProfessors(db *mongo.Database) {
	ctx := context.Background()
	// get the courses collection
	// get the instructors array
	// try matching them based on regex
	// if match, add the professor's

	coursesCollection := db.Collection("courses_temp")
	tempProfessorsCollection := db.Collection("professors_temp")
	professorsCollection := db.Collection("professors")
	// get all courses
	courses := []model.Course{}
	cursor, err := coursesCollection.Find(ctx, bson.M{})
	if err != nil {
		log.Fatal(err)
	}
	defer cursor.Close(ctx)
	if err := cursor.All(ctx, &courses); err != nil {
		log.Fatal(err)
	}
	for _, course := range courses {
		for _, section := range course.SectionData {
			courseString := course.CourseData.Faculty + " " + (course.CourseData.Number) + course.CourseData.Suffix

			for _, instructor := range section.Instructors {
				var existingProfessor model.Professor
				var finalProfessor model.Professor

				firstInitial := instructor[0:1]
				restOfName := instructor[3:]
				regexString := "^" + firstInitial + "." + restOfName + "*" + "$"
				filter := bson.M{"name": bson.M{"$regex": regexString}}
				coursesUpdate := bson.D{
					{Key: "$addToSet", Value: bson.D{
						{Key: "currentCourses", Value: courseString}}},
				}
				departmentUpdate := bson.D{
					{Key: "$addToSet", Value: bson.D{{Key: "departments", Value: course.CourseData.Faculty}}},
				}
				tempErr := tempProfessorsCollection.FindOne(ctx, filter).Decode(&finalProfessor)

				// fmt.Println(tempErr)
				fmt.Println("professor", instructor)
				if tempErr == nil {
					fmt.Println("professor found", instructor)
					tempProfessorsCollection.FindOneAndUpdate(ctx, filter, coursesUpdate).Decode(&finalProfessor)
					tempProfessorsCollection.FindOneAndUpdate(ctx, filter, departmentUpdate).Decode(&finalProfessor)
					// if a professor is found in the professors collection, then add their existing
					// reviews to the final professors collection
					err := professorsCollection.FindOne(ctx, filter).Decode(&existingProfessor)

					// check if the professor is already in the professors collection, and update the courses array
					if err == nil {
						reviewUpdate := bson.D{
							{Key: "$addToSet", Value: bson.D{{Key: "reviews", Value: existingProfessor.Reviews}}},
						}
						// add all reviews from the existing professor to the final professor
						tempProfessorsCollection.FindOneAndUpdate(ctx, filter, reviewUpdate).Decode(&finalProfessor)
					}
					continue
				}

			}
		}

	}
}

func getData() model.TeacherSearchResults {
	data := getKProfessorAtCursor(1000, "")

	return data
}

func getKProfessorAtCursor(k int, cursor string) model.TeacherSearchResults {
	url := "https://www.ratemyprofessors.com/graphql"
	method := "POST"

	payload := model.GetAllProfsQuery(k, cursor)
	client := &http.Client{}
	req, err := http.NewRequest(method, url, strings.NewReader(string(payload)))
	if err != nil {
		fmt.Println(err)
		panic(err)

	}
	req.Header.Add("Authorization", "Basic dGVzdDp0ZXN0")
	req.Header.Add("Content-Type", "application/json")

	res, err := client.Do(req)
	if err != nil {
		fmt.Println(err)
		panic(err)

	}
	defer res.Body.Close()

	body, err := ioutil.ReadAll(res.Body)
	if err != nil {
		fmt.Println(err)
		panic(err)

	}

	var response model.TeacherSearchResults
	err = json.Unmarshal(body, &response)
	if err != nil {
		fmt.Println(err)
		panic(err)
	}
	return response
}

func insertProfessors(profs []model.Professor, tempCollection *mongo.Collection) {
	// insert professors into database
	for _, prof := range profs {
		_, err := tempCollection.InsertOne(context.Background(), prof)
		if err != nil {
			fmt.Println(err)
			panic(err)
		}
	}
}
