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
	_, err = tx.Exec(`CREATE TABLE IF NOT EXISTS public."NewCourses" ("CourseName" TEXT, "InternalID" VARCHAR(255) PRIMARY KEY, "Antireqs" TEXT, "Prereqs" TEXT, "CoReqs" TEXT, "Weight" FLOAT, "ExtraInfo" TEXT)`)

	if err != nil {
		fmt.Println("Failed to create table:", err)
		return false, err
	}

	// Prepare the insert statement
	stmt, err := tx.Prepare(`INSERT INTO public."NewCourses" ("CourseName", "InternalID", "Antireqs", "Prereqs", "CoReqs", "Weight", "ExtraInfo") VALUES ($1, $2, $3, $4, $5, $6, $7)`)
	if err != nil {
		fmt.Println("Failed to prepare statement:", err)
		return false, err
	}
	defer stmt.Close()

	// Insert each course in the transaction
	for _, course := range courses {
		_, err := stmt.Exec(course.CourseName, course.InternalID, course.Antireqs, course.Prereqs, course.CoReqs, course.Weight, course.ExtraInfo)
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
