package models

type ScrapedCourse struct {
	InternalID string
	Antireqs   string
	Prereqs    string
	CoReqs     string
	Weight     float64
	ExtraInfo  string
	CourseName string
	// get desc aswell
	// coreqs is wrong must be Pre-or Corequisite(s):
	// get breadth
}

// Courses model represents the "Courses" table
type Courses struct {
	Id                  uint
	FacultyId           uint
	Number              string
	Name                string
	AntirequisiteString string
	PrerequisiteString  string
	CorequisiteString   string
	BreadthCategory     string
	InternalCourseId    string
	Weight              string
	ExtraInformation    string
	Description         string
}

// JoinedCourses model represents the joined result of "Courses" and "NewCourses" tables
type JoinedCourses struct {
	Id                  uint
	FacultyId           uint
	Number              string
	Name                string
	AntirequisiteString string
	PrerequisiteString  string
	CorequisiteString   string
	BreadthCategory     string
	InternalCourseId    string
	Weight              string
	ExtraInformation    string
	Description         string
}
