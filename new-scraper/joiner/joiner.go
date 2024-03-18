package joiner

// SELECT * FROM weather JOIN cities ON city = name;

// join two tables in sql

import (
	"database/sql"
	"fmt"
	"log"
	connector "scraper/db"
	"scraper/models"

	_ "github.com/lib/pq"
)

func JoinTables() {
	db := connector.ConnectToDB("LOCAL")
	defer db.Close()

	// Create a new table for the joined data (replace 'JoinedCourses' with your desired table name)
	_, err := db.Exec(`
	CREATE TABLE IF NOT EXISTS "JoinedCourses" AS
	SELECT
		"Courses"."Id",
		"Courses"."FacultyId",
		"Courses"."Number",
		COALESCE("NewCourses"."CourseName", "Courses"."Name") AS "Name",
		COALESCE("NewCourses"."Antireqs", "Courses"."AntirequisiteString") AS "AntirequisiteString",
		COALESCE("NewCourses"."Prereqs", "Courses"."PrerequisiteString") AS "PrerequisiteString",
		COALESCE("NewCourses"."CoReqs", "Courses"."CorequisiteString") AS "CorequisiteString",
		COALESCE("Courses"."BreadthCategory", "Courses"."BreadthCategory") AS "BreadthCategory",
		COALESCE("NewCourses"."InternalID", "Courses"."InternalCourseId") AS "InternalCourseId",
		COALESCE("NewCourses"."Weight", "Courses"."Weight") AS "Weight",
		COALESCE("NewCourses"."ExtraInfo", "Courses"."ExtraInformation") AS "ExtraInformation"
		COALESCE("Courses"."Description", "Courses"."Description") AS "Description"
	FROM "Courses"
	LEFT JOIN "NewCourses" ON "Courses"."InternalCourseId" = "NewCourses"."InternalID"
	WHERE "Courses"."InternalCourseId" IS NOT NULL
	`)
	// update breath category and desc later.

	if err != nil {
		log.Fatal(err)
	}

	fmt.Println("Table 'JoinedCourses' created successfully.")

}

func MergeCourseDataSlices(existing []models.Course, new []models.ScrapedCourse) []models.Course {
	for _, course := range new {
		for _, existingCourse := range existing {
			if course.InternalID == existingCourse.InternalCourseID {
				existingCourse = MergeCourseData(existingCourse, course)
			} else {
				existing = append(existing, models.Course{
					FacultyID:           1,
					Number:              1,
					Name:                course.CourseName,
					InternalCourseID:    course.InternalID,
					AntirequisiteString: sql.NullString{String: course.Antireqs, Valid: true},
					PrerequisiteString:  sql.NullString{String: course.Prereqs, Valid: true},
					CorequisiteString:   sql.NullString{String: course.CoReqs, Valid: true},
					Weight:              sql.NullFloat64{Float64: course.Weight, Valid: true},
					ExtraInformation:    sql.NullString{String: course.ExtraInfo, Valid: true},
					BreadthCategories:   sql.NullString{String: course.BreadthCategory, Valid: true},
					Description:         sql.NullString{String: course.Description, Valid: true},
				})
			}
		}
	}
	return existing
}

func MergeCourseData(existing models.Course, new models.ScrapedCourse) models.Course {

	existing.AntirequisiteString = sql.NullString{String: new.Antireqs, Valid: true}
	existing.PrerequisiteString = sql.NullString{String: new.Prereqs, Valid: true}
	existing.CorequisiteString = sql.NullString{String: new.CoReqs, Valid: true}
	existing.Weight = sql.NullFloat64{Float64: new.Weight, Valid: true}
	existing.ExtraInformation = sql.NullString{String: new.ExtraInfo, Valid: true}
	existing.BreadthCategories = sql.NullString{String: new.BreadthCategory, Valid: true}
	existing.Description = sql.NullString{String: new.Description, Valid: true}
	return existing
}
