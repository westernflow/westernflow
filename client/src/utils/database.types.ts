export type Json =
  | string
  | number
  | boolean
  | null
  | { [key: string]: Json | undefined }
  | Json[];

export type Database = {
  public: {
    Tables: {
      __EFMigrationsHistory: {
        Row: {
          MigrationId: string;
          ProductVersion: string;
        };
        Insert: {
          MigrationId: string;
          ProductVersion: string;
        };
        Update: {
          MigrationId?: string;
          ProductVersion?: string;
        };
        Relationships: [];
      };
      CourseOfferings: {
        Row: {
          CalendarSource: number;
          CourseId: number;
          Id: number;
          Suffix: number;
          TermId: number;
          Year: number;
        };
        Insert: {
          CalendarSource: number;
          CourseId: number;
          Id?: number;
          Suffix: number;
          TermId: number;
          Year: number;
        };
        Update: {
          CalendarSource?: number;
          CourseId?: number;
          Id?: number;
          Suffix?: number;
          TermId?: number;
          Year?: number;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_CourseOfferings_Courses_CourseId';
            columns: ['CourseId'];
            isOneToOne: false;
            referencedRelation: 'Courses';
            referencedColumns: ['Id'];
          },
        ];
      };
      CourseReviewReviewer: {
        Row: {
          CourseReviewsLikedId: number;
          LikedById: number;
        };
        Insert: {
          CourseReviewsLikedId: number;
          LikedById: number;
        };
        Update: {
          CourseReviewsLikedId?: number;
          LikedById?: number;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_CourseReviewReviewer_CourseReviews_CourseReviewsLikedId';
            columns: ['CourseReviewsLikedId'];
            isOneToOne: false;
            referencedRelation: 'CourseReviews';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_CourseReviewReviewer_Reviewers_LikedById';
            columns: ['LikedById'];
            isOneToOne: false;
            referencedRelation: 'Reviewers';
            referencedColumns: ['Id'];
          },
        ];
      };
      CourseReviews: {
        Row: {
          CourseId: number;
          CreatedDate: string;
          EasyRating: number;
          Id: number;
          IsLiked: boolean;
          ModifiedDate: string;
          ProfessorId: number | null;
          ReviewerId: number;
          ReviewText: string;
          UsefulRating: number;
        };
        Insert: {
          CourseId: number;
          CreatedDate: string;
          EasyRating: number;
          Id?: number;
          IsLiked: boolean;
          ModifiedDate: string;
          ProfessorId?: number | null;
          ReviewerId: number;
          ReviewText: string;
          UsefulRating: number;
        };
        Update: {
          CourseId?: number;
          CreatedDate?: string;
          EasyRating?: number;
          Id?: number;
          IsLiked?: boolean;
          ModifiedDate?: string;
          ProfessorId?: number | null;
          ReviewerId?: number;
          ReviewText?: string;
          UsefulRating?: number;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_CourseReviews_Courses_CourseId';
            columns: ['CourseId'];
            isOneToOne: false;
            referencedRelation: 'Courses';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_CourseReviews_Professors_ProfessorId';
            columns: ['ProfessorId'];
            isOneToOne: false;
            referencedRelation: 'Professors';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_CourseReviews_Reviewers_ReviewerId';
            columns: ['ReviewerId'];
            isOneToOne: false;
            referencedRelation: 'Reviewers';
            referencedColumns: ['Id'];
          },
        ];
      };
      Courses: {
        Row: {
          Antirequisites: string;
          Corequisites: string;
          Description: string;
          ExtraInformation: string;
          FacultyId: number;
          Id: number;
          InternalCourseId: string;
          Name: string;
          Number: number;
          Prerequisites: string;
          Weight: number | null;
        };
        Insert: {
          Antirequisites: string;
          Corequisites: string;
          Description: string;
          ExtraInformation: string;
          FacultyId: number;
          Id?: number;
          InternalCourseId: string;
          Name: string;
          Number: number;
          Prerequisites: string;
          Weight?: number | null;
        };
        Update: {
          Antirequisites?: string;
          Corequisites?: string;
          Description?: string;
          ExtraInformation?: string;
          FacultyId?: number;
          Id?: number;
          InternalCourseId?: string;
          Name?: string;
          Number?: number;
          Prerequisites?: string;
          Weight?: number | null;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_Courses_Faculties_FacultyId';
            columns: ['FacultyId'];
            isOneToOne: false;
            referencedRelation: 'Faculties';
            referencedColumns: ['Id'];
          },
        ];
      };
      Faculties: {
        Row: {
          Abbreviation: string;
          EnumBitmap: string | null;
          Id: number;
          Name: string;
        };
        Insert: {
          Abbreviation: string;
          EnumBitmap?: string | null;
          Id?: number;
          Name: string;
        };
        Update: {
          Abbreviation?: string;
          EnumBitmap?: string | null;
          Id?: number;
          Name?: string;
        };
        Relationships: [];
      };
      ProfessorReviewReviewer: {
        Row: {
          LikedById: number;
          ProfessorReviewsLikedId: number;
        };
        Insert: {
          LikedById: number;
          ProfessorReviewsLikedId: number;
        };
        Update: {
          LikedById?: number;
          ProfessorReviewsLikedId?: number;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_ProfessorReviewReviewer_ProfessorReviews_ProfessorReviewsLi~';
            columns: ['ProfessorReviewsLikedId'];
            isOneToOne: false;
            referencedRelation: 'ProfessorReviews';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_ProfessorReviewReviewer_Reviewers_LikedById';
            columns: ['LikedById'];
            isOneToOne: false;
            referencedRelation: 'Reviewers';
            referencedColumns: ['Id'];
          },
        ];
      };
      ProfessorReviews: {
        Row: {
          Clarity: number;
          CourseId: number | null;
          CreatedDate: string;
          Difficulty: number;
          Helpful: number;
          Id: number;
          ModifiedDate: string;
          ProfessorId: number;
          Quality: number;
          ReviewerId: number;
          ReviewText: string;
        };
        Insert: {
          Clarity: number;
          CourseId?: number | null;
          CreatedDate: string;
          Difficulty: number;
          Helpful: number;
          Id?: number;
          ModifiedDate: string;
          ProfessorId: number;
          Quality: number;
          ReviewerId: number;
          ReviewText: string;
        };
        Update: {
          Clarity?: number;
          CourseId?: number | null;
          CreatedDate?: string;
          Difficulty?: number;
          Helpful?: number;
          Id?: number;
          ModifiedDate?: string;
          ProfessorId?: number;
          Quality?: number;
          ReviewerId?: number;
          ReviewText?: string;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_ProfessorReviews_Courses_CourseId';
            columns: ['CourseId'];
            isOneToOne: false;
            referencedRelation: 'Courses';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_ProfessorReviews_Professors_ProfessorId';
            columns: ['ProfessorId'];
            isOneToOne: false;
            referencedRelation: 'Professors';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_ProfessorReviews_Reviewers_ReviewerId';
            columns: ['ReviewerId'];
            isOneToOne: false;
            referencedRelation: 'Reviewers';
            referencedColumns: ['Id'];
          },
        ];
      };
      Professors: {
        Row: {
          Email: string;
          Id: number;
          Name: string;
          RmpId: string | null;
          UwoId: string;
        };
        Insert: {
          Email: string;
          Id?: number;
          Name: string;
          RmpId?: string | null;
          UwoId: string;
        };
        Update: {
          Email?: string;
          Id?: number;
          Name?: string;
          RmpId?: string | null;
          UwoId?: string;
        };
        Relationships: [];
      };
      ProfessorSection: {
        Row: {
          ProfessorsId: number;
          SectionsId: number;
        };
        Insert: {
          ProfessorsId: number;
          SectionsId: number;
        };
        Update: {
          ProfessorsId?: number;
          SectionsId?: number;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_ProfessorSection_Professors_ProfessorsId';
            columns: ['ProfessorsId'];
            isOneToOne: false;
            referencedRelation: 'Professors';
            referencedColumns: ['Id'];
          },
          {
            foreignKeyName: 'FK_ProfessorSection_Sections_SectionsId';
            columns: ['SectionsId'];
            isOneToOne: false;
            referencedRelation: 'Sections';
            referencedColumns: ['Id'];
          },
        ];
      };
      Reviewers: {
        Row: {
          Id: number;
          SubjectId: string;
        };
        Insert: {
          Id?: number;
          SubjectId: string;
        };
        Update: {
          Id?: number;
          SubjectId?: string;
        };
        Relationships: [];
      };
      Sections: {
        Row: {
          Campus: number;
          ClassNumber: number;
          ComponentType: number;
          CourseOfferingId: number;
          Delivery: number;
          Id: number;
          Number: number;
          ProfessorNames: string[];
          Status: number;
          TimetableRequisiteString: string | null;
          TimingDetailsText: string;
          WaitListSize: number;
        };
        Insert: {
          Campus: number;
          ClassNumber: number;
          ComponentType: number;
          CourseOfferingId: number;
          Delivery: number;
          Id?: number;
          Number: number;
          ProfessorNames: string[];
          Status: number;
          TimetableRequisiteString?: string | null;
          TimingDetailsText: string;
          WaitListSize: number;
        };
        Update: {
          Campus?: number;
          ClassNumber?: number;
          ComponentType?: number;
          CourseOfferingId?: number;
          Delivery?: number;
          Id?: number;
          Number?: number;
          ProfessorNames?: string[];
          Status?: number;
          TimetableRequisiteString?: string | null;
          TimingDetailsText?: string;
          WaitListSize?: number;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_Sections_CourseOfferings_CourseOfferingId';
            columns: ['CourseOfferingId'];
            isOneToOne: false;
            referencedRelation: 'CourseOfferings';
            referencedColumns: ['Id'];
          },
        ];
      };
      TimingDetails: {
        Row: {
          DaysOfWeekBitmap: string;
          Id: number;
          Location: string;
          SectionId: number;
          Time: string;
        };
        Insert: {
          DaysOfWeekBitmap: string;
          Id?: number;
          Location: string;
          SectionId: number;
          Time: string;
        };
        Update: {
          DaysOfWeekBitmap?: string;
          Id?: number;
          Location?: string;
          SectionId?: number;
          Time?: string;
        };
        Relationships: [
          {
            foreignKeyName: 'FK_TimingDetails_Sections_SectionId';
            columns: ['SectionId'];
            isOneToOne: false;
            referencedRelation: 'Sections';
            referencedColumns: ['Id'];
          },
        ];
      };
    };
    Views: {
      [_ in never]: never;
    };
    Functions: {
      [_ in never]: never;
    };
    Enums: {
      [_ in never]: never;
    };
    CompositeTypes: {
      [_ in never]: never;
    };
  };
};

type PublicSchema = Database[Extract<keyof Database, 'public'>];

export type Tables<
  PublicTableNameOrOptions extends
    | keyof (PublicSchema['Tables'] & PublicSchema['Views'])
    | { schema: keyof Database },
  TableName extends PublicTableNameOrOptions extends { schema: keyof Database }
    ? keyof (Database[PublicTableNameOrOptions['schema']]['Tables'] &
        Database[PublicTableNameOrOptions['schema']]['Views'])
    : never = never,
> = PublicTableNameOrOptions extends { schema: keyof Database }
  ? (Database[PublicTableNameOrOptions['schema']]['Tables'] &
      Database[PublicTableNameOrOptions['schema']]['Views'])[TableName] extends {
      Row: infer R;
    }
    ? R
    : never
  : PublicTableNameOrOptions extends keyof (PublicSchema['Tables'] &
        PublicSchema['Views'])
    ? (PublicSchema['Tables'] &
        PublicSchema['Views'])[PublicTableNameOrOptions] extends {
        Row: infer R;
      }
      ? R
      : never
    : never;

export type TablesInsert<
  PublicTableNameOrOptions extends
    | keyof PublicSchema['Tables']
    | { schema: keyof Database },
  TableName extends PublicTableNameOrOptions extends { schema: keyof Database }
    ? keyof Database[PublicTableNameOrOptions['schema']]['Tables']
    : never = never,
> = PublicTableNameOrOptions extends { schema: keyof Database }
  ? Database[PublicTableNameOrOptions['schema']]['Tables'][TableName] extends {
      Insert: infer I;
    }
    ? I
    : never
  : PublicTableNameOrOptions extends keyof PublicSchema['Tables']
    ? PublicSchema['Tables'][PublicTableNameOrOptions] extends {
        Insert: infer I;
      }
      ? I
      : never
    : never;

export type TablesUpdate<
  PublicTableNameOrOptions extends
    | keyof PublicSchema['Tables']
    | { schema: keyof Database },
  TableName extends PublicTableNameOrOptions extends { schema: keyof Database }
    ? keyof Database[PublicTableNameOrOptions['schema']]['Tables']
    : never = never,
> = PublicTableNameOrOptions extends { schema: keyof Database }
  ? Database[PublicTableNameOrOptions['schema']]['Tables'][TableName] extends {
      Update: infer U;
    }
    ? U
    : never
  : PublicTableNameOrOptions extends keyof PublicSchema['Tables']
    ? PublicSchema['Tables'][PublicTableNameOrOptions] extends {
        Update: infer U;
      }
      ? U
      : never
    : never;

export type Enums<
  PublicEnumNameOrOptions extends
    | keyof PublicSchema['Enums']
    | { schema: keyof Database },
  EnumName extends PublicEnumNameOrOptions extends { schema: keyof Database }
    ? keyof Database[PublicEnumNameOrOptions['schema']]['Enums']
    : never = never,
> = PublicEnumNameOrOptions extends { schema: keyof Database }
  ? Database[PublicEnumNameOrOptions['schema']]['Enums'][EnumName]
  : PublicEnumNameOrOptions extends keyof PublicSchema['Enums']
    ? PublicSchema['Enums'][PublicEnumNameOrOptions]
    : never;
