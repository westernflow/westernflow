package controller

import (
	"context"
	"encoding/json"
	"fmt"
	"net/http"
	"uwo-tt-api/model"

	"go.mongodb.org/mongo-driver/bson"
)

func (c *Controller) ListProfessors(w http.ResponseWriter, r *http.Request) {
	HitEndpoint("professors")

	// Set headers
	w.Header().Set("Content-Type", "application/json")
	w.Header().Set("Access-Control-Allow-Origin", "*")

	fmt.Println("Headers set")
	// Connect to professor database
	profcollection := c.DB.Collection("professors")

	// Check if url has query parameters

	if err := r.ParseForm(); err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to parse prof query parameters")
		return
	}

	// route will be in the form of /api/v1/professors/j.smith
	// extract the prof name from the url
	profName := r.URL.Path[len("/api/v1/professors/"):]

	// add a space between the first initial and last name j.smith -> j. smith
	profName = profName[:2] + " " + profName[2:]
	fmt.Println("Searching for prof name: ", profName)

	// Find the professor in the mongoDB database based on the profName and make it case insensitive
	profFilter := bson.D{{Key: "name", Value: bson.D{{Key: "$regex", Value: profName}, {Key: "$options", Value: "i"}}}}
	var profObject model.Professor
	prof := profcollection.FindOne(context.Background(), profFilter)

	// check if the professor was found
	if prof.Err() != nil {
		w = NewError(w, http.StatusBadRequest, prof.Err(), "Failed to find professor")
		return
	}

	// otherwise return the professor in a json object
	err := prof.Decode(&profObject)
	if err != nil {
		w = NewError(w, http.StatusBadRequest, err, "Failed to decode professor")
		return
	}
	
	// turn the bson into a json
	//convert the professor object to json
	profJSON, err := json.Marshal(profObject)
	w.Write(profJSON)
	w.WriteHeader(http.StatusOK)
}
