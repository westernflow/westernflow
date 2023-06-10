// Professor - struct for a Western professor
export interface Professor {
  id: string;
  name: string;
  rmpName: string;
  rmpId: string;
  rating: number;
  difficulty: number;
  currentCourses: string[];
  reviews: ProfessorReview[];
  departments: string[];
}

// ProfessorReview - struct for a professor review
export interface ProfessorReview {
  professorId: string;
  quality: number;
  difficulty: number;
  date: string;
  reviewText: string;
  helpful: number;
  clarity: number;
}