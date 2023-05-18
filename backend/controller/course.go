package controller

import (
	"context"
	"encoding/json"
	"net/http"
	client "uwo-tt-api/model/client"
	domain "uwo-tt-api/model/domain"

	"go.mongodb.org/mongo-driver/bson"
)

// ListSections godoc
// @Summary List all sections
// @Description Grabs each individual model.Section that matches query filters and options
// @Tags course
// @ID courses-list-sections
// @Accept plain
// @Produce json
// @Param test query CourseQueryParams false "Option filter, sort, pagination"
// @Success 200 {array} model.Section
// @Failure 400 {object} HTTPError
// @Router /sections [get]
func (c *Controller) ListSections(w http.ResponseWriter, r *http.Request) {
	HitEndpoint("courses")

	// Set response headers
	w.Header().Set("Content-Type", "application/json")
	w.Header().Set("Access-Control-Allow-Origin", "*")

	// Connect to courses collection
	collection := c.DB.Collection("courses")

	// Check if url can be parsed
	if err := r.ParseForm(); err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to parse course query parameters")
		return
	}

	// Extract find filters
	findFilter, err := ExtractCourseFilter(r)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to extract course filters")
		return
	}

	// Extract find options
	findOptions, err := ExtractCourseParams(r)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to extract course options")
		return
	}

	// Perform DB query
	cur, err := collection.Find(context.TODO(), findFilter, findOptions)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "DB query failed; malformed filter or option")
		return
	}

	// Define an array to store the decoded documents
	var sections []domain.Section

	for cur.Next(context.TODO()) {
		//Create a value into which the single document can be decoded
		var elem domain.Section
		err := cur.Decode(&elem)
		if err != nil {
			w = NewError(w, http.StatusBadRequest, err, "Failed to decode db result")
			return
		}

		sections = append(sections, elem)
	}

	if err := cur.Err(); err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to iterate over db results")
		return
	}

	//Close the cursor once finished
	cur.Close(context.TODO())

	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(sections)
}

// ListCourses godoc
// @Summary List all courses
// @Description Grabs each individual section that matches query filters and options and combines them into a course struct for all sections that have matching course info
// @Tags course
// @ID courses-list-courses
// @Accept plain
// @Produce json
// @Param test query CourseQueryParams false "Course filter, sort, pagination"
// @Success 200 {array} model.Section
// @Failure 400 {object} HTTPError
// @Router /courses [get]
func (c *Controller) ListCourses(w http.ResponseWriter, r *http.Request) {
	HitEndpoint("courses")

	// Set response headers
	w.Header().Set("Content-Type", "application/json")
	w.Header().Set("Access-Control-Allow-Origin", "*")

	// Connect to courses collection
	collection := c.DB.Collection("courses")

	// Check if url can be parsed
	if err := r.ParseForm(); err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to parse course query parameters")
		return
	}

	// Extract find filters
	findFilter, err := ExtractCourseFilter(r)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to extract course filters")
		return
	}

	// Extract find options
	findOptions, err := ExtractCourseParams(r)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to extract course options")
		return
	}

	// Perform DB query
	cur, err := collection.Find(context.TODO(), findFilter, findOptions)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "DB query failed; malformed filter or option")
		return
	}

	// Define an array to store the decoded documents
	var courses []domain.Course

	for cur.Next(context.TODO()) {
		//Create a value into which the single document can be decoded
		var elem domain.Course
		err := cur.Decode(&elem)
		if err != nil {
			w = NewError(w, http.StatusBadRequest, err, "Failed to decode db result")
			return
		}

		courses = append(courses, elem)
	}

	if err := cur.Err(); err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to iterate over db results")
		return
	}

	//Close the cursor once finished
	cur.Close(context.TODO())

	// include cors header for local development
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(courses)
}

func (c *Controller) ListIndexedSearchData(w http.ResponseWriter, r *http.Request) {
	HitEndpoint("IndexedSearchData")

	// Set response headers
	w.Header().Set("Content-Type", "application/json")
	w.Header().Set("Access-Control-Allow-Origin", "*")

	// Connect to courses collection
	collection := c.DB.Collection("courses")
	profCollection := c.DB.Collection("professors")
	// get all course data
	cur, err := collection.Find(context.TODO(), bson.M{})
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "DB query failed; malformed filter or option")
		return
	}

	// Define an array to store the decoded documents

	var courses []client.IndexedCourse

	for cur.Next(context.TODO()) {
		//Create a value into which the single document can be decoded
		var elem domain.Course
		err := cur.Decode(&elem)
		if err != nil {
			w = NewError(w, http.StatusBadRequest, err, "Failed to decode db result")
			return
		}

		// simplify the course data
		var indexedCourse client.IndexedCourse

		indexedCourse.Faculty = elem.CourseData.Faculty
		indexedCourse.Number = elem.CourseData.Number
		indexedCourse.Suffix = elem.CourseData.Suffix
		indexedCourse.Name = elem.CourseData.Name

		courses = append(courses, indexedCourse)
	}

	// same thing but for prof data

	var professors []client.IndexedProfessors

	cur, err = profCollection.Find(context.TODO(), bson.M{})
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "DB query failed; malformed filter or option")
		return
	}

	for cur.Next(context.TODO()) {
		//Create a value into which the single document can be decoded
		var elem domain.Professor
		err := cur.Decode(&elem)
		if err != nil {
			w = NewError(w, http.StatusBadRequest, err, "Failed to decode db result")
			return
		}

		// simplify the course data
		var indexedProfessor client.IndexedProfessors

		indexedProfessor.Name = elem.Name
		indexedProfessor.RMPName = elem.RMPName

		professors = append(professors, indexedProfessor)
	}

	// serialize professors and courses into a single struct and send it back
	var indexedSearchData client.IndexedSearchData

	indexedSearchData.IndexedCourses = courses
	indexedSearchData.IndexedProfessors = professors

	if err := cur.Err(); err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to iterate over db results")
		return
	}

	//Close the cursor once finished
	cur.Close(context.TODO())

	// include cors header for local development
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(indexedSearchData)
}
