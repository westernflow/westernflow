package joiner

// SELECT * FROM weather JOIN cities ON city = name;

// join two tables in sql

import (
	"fmt"
	"log"
	connector "scraper/db"

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

func main() {
	JoinTables()
}
