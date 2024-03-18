package db

import (
	"database/sql"
	"fmt"
	"scraper/models"
)

func WriteCoursesToDB(courses []models.ScrapedCourse, db *sql.DB) (bool, error) {
	// Start a transaction
	tx, err := db.Begin()
	if err != nil {
		fmt.Println("Failed to begin transaction:", err)
		return false, err
	}
	defer tx.Rollback() // Rollback the transaction if there's an error

	// Create the table if it doesn't exist
	_, err = tx.Exec(`CREATE TABLE IF NOT EXISTS public."NewCourses" ("CourseName" TEXT, "InternalID" VARCHAR(255) PRIMARY KEY, "Antireqs" TEXT, "Prereqs" TEXT, "CoReqs" TEXT, "Weight" FLOAT, "ExtraInfo" TEXT, "BreadthCategory" TEXT, "Description" TEXT)`)

	if err != nil {
		fmt.Println("Failed to create table:", err)
		return false, err
	}

	// Prepare the insert statement
	stmt, err := tx.Prepare(`INSERT INTO public."NewCourses" ("CourseName", "InternalID", "Antireqs", "Prereqs", "CoReqs", "Weight", "ExtraInfo", "BreadthCategory", "Description") VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9)`)
	if err != nil {
		fmt.Println("Failed to prepare statement:", err)
		return false, err
	}
	defer stmt.Close()

	// Insert each course in the transaction
	for _, course := range courses {
		_, err := stmt.Exec(course.CourseName, course.InternalID, course.Antireqs, course.Prereqs, course.CoReqs, course.Weight, course.ExtraInfo, course.BreadthCategory, course.Description)
		if err != nil {
			fmt.Println("Failed to insert data into the database:", err)
			return false, err
		}
	}

	// Commit the transaction
	err = tx.Commit()
	if err != nil {
		fmt.Println("Failed to commit transaction:", err)
		return false, err
	}

	return true, nil
}

func ConnectToDB(env string) *sql.DB {
	// PostgreSQL connection string
	var connStr string
	if env == "PROD" {
		connStr = ""
	} else if env == "LOCAL" {
		connStr = "user=postgres password=example dbname=postgres host=localhost port=5432 sslmode=disable"
	} else {
		fmt.Println("Invalid environment")
		return nil
	}

	// Open a connection to the database
	db, err := sql.Open("postgres", connStr)
	if err != nil {
		fmt.Println("Failed to connect to the database:", err)
		return nil
	}

	// Test the connection
	err = db.Ping()
	if err != nil {
		fmt.Println("Failed to ping the database:", err)
		return nil
	}

	fmt.Println("Connected to the database")
	return db
}

func FetchExistingCourses(db *sql.DB) []models.Course {
	// Query the database for existing courses
	rows, err := db.Query(`SELECT * FROM public."Courses"`)
	if err != nil {
		fmt.Println("Failed to query the database:", err)
		return nil
	}
	defer rows.Close()

	// Iterate over the rows and populate the data into existingCourses
	var existingCourses []models.Course
	for rows.Next() {
		var course models.Course
		err := rows.Scan(&course.ID, &course.FacultyID, &course.Number, &course.Name, &course.AntirequisiteString, &course.PrerequisiteString, &course.CorequisiteString, &course.BreadthCategories, &course.InternalCourseID, &course.Weight, &course.ExtraInformation, &course.Description)
		if err != nil {
			fmt.Println("Failed to scan row:", err)
			continue
		}
		existingCourses = append(existingCourses, course)
	}
	if err := rows.Err(); err != nil {
		fmt.Println("Error iterating over rows:", err)
		return nil
	}

	return existingCourses
}

func UpdateCourses(db *sql.DB, courses []models.Course) error {
	// Start a transaction
	tx, err := db.Begin()
	if err != nil {
		fmt.Println("Failed to begin transaction")
		return err
	}
	defer tx.Rollback() // Rollback the transaction if there's an error

	// Prepare the update statement
	stmt, err := tx.Prepare(`UPDATE public."Courses" SET "AntirequisiteString" = $1, "PrerequisiteString" = $2, "CorequisiteString" = $3, "Weight" = $4, "ExtraInformation" = $5 WHERE "InternalCourseId" = $6`)
	if err != nil {
		fmt.Println("Failed to prepare statement")
		return err

	}
	defer stmt.Close()

	// Update each course in the transaction
	for _, course := range courses {
		_, err := stmt.Exec(course.AntirequisiteString, course.PrerequisiteString, course.CorequisiteString, course.Weight, course.ExtraInformation, course.InternalCourseID)
		if err != nil {
			fmt.Println("Failed to update data in the database")
			return err
		}
	}

	// Commit the transaction
	err = tx.Commit()
	if err != nil {
		fmt.Println("Failed to commit transaction")
		return err
	}
	return nil

}
