package models

import "database/sql"

type ScrapedCourse struct {
	InternalID      string
	Antireqs        string
	Prereqs         string
	CoReqs          string
	Weight          float64
	ExtraInfo       string
	CourseName      string
	Description     string
	BreadthCategory string
	// get desc aswell
	// coreqs is wrong must be Pre-or Corequisite(s):
	// get breadth
}

// Courses model represents the "Courses" table

type Course struct {
	ID                  int
	FacultyID           int
	Number              int
	Name                string
	Description         sql.NullString
	AntirequisiteString sql.NullString
	CorequisiteString   sql.NullString
	PrerequisiteString  sql.NullString
	InternalCourseID    string
	Weight              sql.NullFloat64
	ExtraInformation    sql.NullString
	BreadthCategories   sql.NullString
}
