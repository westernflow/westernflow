package model

// simpler Ccomponent

type IndexedCourse struct {
	Faculty string `bson:"faculty" json:"faculty" example:"MATH"`
	Number  string `bson:"number" json:"number" example:"101"`
	Suffix  string `bson:"suffix" json:"suffix" example:"A"`
	Name    string `bson:"name" json:"name" example:"Applied Linear Algebra"`
}

type IndexedProfessors struct {
	Name    string `bson:"name" json:"name" example:"J. Smith"`
	RMPName string `bson:"rmp_name" json:"rmp_name" example:"John Smith or J. Smith"`
}

type IndexedSearchData struct {
	IndexedCourses []IndexedCourse    `bson:"indexed_courses" json:"indexed_courses"`
	IndexedProfessors   []IndexedProfessors `bson:"indexed_profs" json:"indexed_profs"`
}
