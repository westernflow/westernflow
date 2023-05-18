export interface IndexedCourse {
  faculty: string;
  number: string;
  suffix: string;
  name: string;
}

export interface IndexedProfessor {
  name: string;
  rmp_name: string;
}

export interface IndexedSearchData {
  indexed_courses: IndexedCourse[];
  indexed_profs: IndexedProfessor[];
}