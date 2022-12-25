// package model

// // TimeComponent represents a single meeting time for a course section
// export interface TimeComponent struct {
// 	Day       string `bson:"days" 		json:"days" 		example:"M"`
// 	StartTime string `bson:"startTime" 	json:"startTime" 	example:"8:30 AM"`
// 	EndTime   string `bson:"endTime" 	json:"endTime" 		example:"1:30 PM"`
// }

// // SectionComponent represents the section specific data for a course section
// export interface SectionComponent struct {
// 	Number      int             `bson:"number" 		json:"number" 		example:"001"`
// 	Component   string          `bson:"component" 	json:"component" 	example:"LEC"`
// 	ClassNumber int             `bson:"classNumber" json:"classNumber" 	example:"5000"`
// 	Location    string          `bson:"location" 	json:"location" 	example:"NS 145"`
// 	Instructor  string          `bson:"instructor" 	json:"instructor" 	example:"Haffie"`
// 	Reqs        string          `bson:"requisites" 	json:"requisites" 	example:"REQUISITES:..."`
// 	Status      string          `bson:"status" 		json:"status" 		example:"Full"`
// 	Campus      string          `bson:"campus" 		json:"campus" 		example:"Main"`
// 	Delivery    string          `bson:"delivery" 	json:"delivery" 	example:"Distance Studies/Online"`
// 	Times       []TimeComponent `bson:"times" 		json:"times"`
// }

// // CourseComponent - represents the specific data common to all courses sections of any given course
// export interface CourseComponent struct {
// 	Faculty     string `bson:"faculty" 		json:"faculty" 		example:"CLASSICS"`
// 	Number      int    `bson:"number" 		json:"number" 		example:"2053"`
// 	Suffix      string `bson:"suffix" 		json:"suffix" 		example:"B"`
// 	Name        string `bson:"name" 		json:"name" 		example:"MATH FOR FINANCIAL ANALYSIS"`
// 	Description string `bson:"description" 	json:"description" 	example:"Course description"`
// }

// // Course - Returned as endpoint only, stores the information of a course and all its related section information
// export interface Course struct {
// 	Source      SourceInfo         `bson:"source" json:"source"`
// 	Time        TimeInfo           `bson:"time" json:"time"`
// 	CourseData  CourseComponent    `bson:"courseData" json:"courseData"`
// 	SectionData []SectionComponent `bson:"sectionData" json:"sectionData"`
// }

// // Section - Stored in the database/returned as endpoint, stores the information of a section including what course it is a part of
// export interface Section struct {
// 	Source      SourceInfo       `bson:"source" json:"source"`
// 	Time        TimeInfo         `bson:"time" json:"time"`
// 	CourseData  CourseComponent  `bson:"courseData" json:"courseData"`
// 	SectionData SectionComponent `bson:"sectionData" json:"sectionData"`
// }

// create a frontend model for course

// src/Models/Course.ts

export interface Course {
  source: SourceInfo;
  time: TimeInfo;
  courseData: CourseComponent;
  sectionData: SectionComponent[];
};

export interface CourseComponent {
  Faculty: string;
  Number: number;
  Suffix: string;
  Name: string;
  Description: string;
}

export interface SectionComponent {
  number: number;
  component: string;
  classNumber: number;
  location: string;
  instructor: string;
  requisites: string;
  status: string;
  campus: string;
  delivery: string;
  times: TimeComponent[];
}

export interface TimeComponent {
  day: string;
  startTime: string;
  endTime: string;
}

export interface SourceInfo {
  source: string;
  term: number;
  year: number;
}

export interface TimeInfo {
  dateRetrieved: Date;
  dateUpdated: Date;
}

export interface Section {
  source: SourceInfo;
  time: TimeInfo;
  courseData: CourseComponent;
  sectionData: SectionComponent;
}