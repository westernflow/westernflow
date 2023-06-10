// TimeComponent represents a single meeting time for a course section
export interface TimeComponent {
  days: string;
  startTime: string;
  endTime: string;
}

// SectionComponent represents the section specific data for a course section
export interface SectionComponent {
  number: number;
  component: string;
  classNumber: number;
  location: string;
  instructors: string[];
  requisites: string;
  status: string;
  campus: string;
  delivery: string;
  times: TimeComponent[];
}

// CourseComponent - represents the specific data common to all courses sections of any given course
export interface CourseComponent {
  faculty: string;
  number: string;
  suffix: string;
  name: string;
  description: string;
}

// Course - Returned as endpoint only, stores the information of a course and all its related section information
export interface Course {
  source: SourceInfo;
  time: TimeInfo;
  courseData: CourseComponent;
  sectionData: SectionComponent[];
}

// Section - Stored in the database/returned as endpoint, stores the information of a section including what course it is a part of
export interface Section {
  source: SourceInfo;
  time: TimeInfo;
  courseData: CourseComponent;
  sectionData: SectionComponent;
}

/* SourceInfo struct to define source of information; URL and header string
type SourceInfo struct {
	Title string `bson:"title" json:"title" example:"Fall/Winter Academic Timetable"`
	Year  string `bson:"year" json:"year" example:"2020/2021"`
	URL   string `bson:"url" json:"url" example:"https://studentservices.uwo.ca/secure/timetables/mastertt/ttindex.cfm"`
}

// TimeInfo struct to define time
type TimeInfo struct {
	Added time.Time `bson:"added" json:"added"`
}*/
// Add these interfaces 

// SourceInfo - defines source of information; URL and header string
export interface SourceInfo {
  title: string;
  year: string;
  url: string;
}

// TimeInfo - defines time
export interface TimeInfo {
  added: Date;
}