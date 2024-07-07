import * as ApolloReactCommon from '@apollo/react-common';
import * as ApolloReactHooks from '@apollo/react-hooks';
import gql from 'graphql-tag';

export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: any }> = { [K in keyof T]: T[K] };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
  /** The built-in `Decimal` scalar type. */
  Decimal: any;
};

export type BooleanOperationFilterInput = {
  eq?: Maybe<Scalars['Boolean']>;
  neq?: Maybe<Scalars['Boolean']>;
};

export enum CalendarSource {
  FallWinter = 'FALL_WINTER',
  Summer = 'SUMMER',
}

export type CalendarSourceOperationFilterInput = {
  eq?: Maybe<CalendarSource>;
  neq?: Maybe<CalendarSource>;
  in?: Maybe<Array<CalendarSource>>;
  nin?: Maybe<Array<CalendarSource>>;
};

export enum Campus {
  Unknown = 'UNKNOWN',
  Main = 'MAIN',
  Brescia = 'BRESCIA',
  Huron = 'HURON',
  King = 'KING',
  Ivey = 'IVEY',
  All = 'ALL',
}

export type CampusOperationFilterInput = {
  eq?: Maybe<Campus>;
  neq?: Maybe<Campus>;
  in?: Maybe<Array<Campus>>;
  nin?: Maybe<Array<Campus>>;
};

export enum ComponentType {
  Undefined = 'UNDEFINED',
  Lecture = 'LECTURE',
  Lab = 'LAB',
  Tutorial = 'TUTORIAL',
  All = 'ALL',
}

export type ComponentTypeOperationFilterInput = {
  eq?: Maybe<ComponentType>;
  neq?: Maybe<ComponentType>;
  in?: Maybe<Array<ComponentType>>;
  nin?: Maybe<Array<ComponentType>>;
};

export type Course = {
  __typename?: 'Course';
  faculty?: Maybe<Faculty>;
  courseOfferings?: Maybe<Array<Maybe<CourseOffering>>>;
  code: Scalars['String'];
  rating: CourseReviewAggregate;
  has_prereqs: Scalars['Boolean'];
  prof_ids?: Maybe<Array<Scalars['Int']>>;
  terms: Array<Scalars['Int']>;
  postrequisites?: Maybe<Array<Maybe<Course>>>;
  id: Scalars['Int'];
  number: Scalars['Int'];
  name: Scalars['String'];
  prerequisites: Scalars['String'];
  corequisites: Scalars['String'];
  antirequisites: Scalars['String'];
  description: Scalars['String'];
  extraInformation: Scalars['String'];
  weight?: Maybe<Scalars['Decimal']>;
  internalCourseId: Scalars['String'];
  facultyId: Scalars['Int'];
  relatedProfessorReviews: Array<ProfessorReview>;
};

export type CourseFilterInput = {
  and?: Maybe<Array<CourseFilterInput>>;
  or?: Maybe<Array<CourseFilterInput>>;
  id?: Maybe<IntOperationFilterInput>;
  number?: Maybe<IntOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  prerequisites?: Maybe<StringOperationFilterInput>;
  corequisites?: Maybe<StringOperationFilterInput>;
  antirequisites?: Maybe<StringOperationFilterInput>;
  description?: Maybe<StringOperationFilterInput>;
  extraInformation?: Maybe<StringOperationFilterInput>;
  weight?: Maybe<DecimalOperationFilterInput>;
  internalCourseId?: Maybe<StringOperationFilterInput>;
  facultyId?: Maybe<IntOperationFilterInput>;
  faculty?: Maybe<FacultyFilterInput>;
  courseOfferings?: Maybe<ListFilterInputTypeOfCourseOfferingFilterInput>;
  relatedProfessorReviews?: Maybe<ListFilterInputTypeOfProfessorReviewFilterInput>;
};

export type CourseOffering = {
  __typename?: 'CourseOffering';
  sections?: Maybe<Array<Maybe<Section>>>;
  termId: Scalars['Int'];
  year: Scalars['Int'];
  calendarSource: CalendarSource;
  suffix: Suffix;
  courseId: Scalars['Int'];
  course: Course;
  id: Scalars['Int'];
};

export type CourseOfferingFilterInput = {
  and?: Maybe<Array<CourseOfferingFilterInput>>;
  or?: Maybe<Array<CourseOfferingFilterInput>>;
  termId?: Maybe<IntOperationFilterInput>;
  year?: Maybe<IntOperationFilterInput>;
  calendarSource?: Maybe<CalendarSourceOperationFilterInput>;
  suffix?: Maybe<SuffixOperationFilterInput>;
  courseId?: Maybe<IntOperationFilterInput>;
  course?: Maybe<CourseFilterInput>;
  sections?: Maybe<ListFilterInputTypeOfSectionFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
};

export type CourseReview = {
  __typename?: 'CourseReview';
  reviewerId: Scalars['Int'];
  reviewer: Reviewer;
  courseId: Scalars['Int'];
  course: Course;
  isLiked: Scalars['Boolean'];
  easyRating: Scalars['Int'];
  usefulRating: Scalars['Int'];
  reviewText: Scalars['String'];
  professorId?: Maybe<Scalars['Int']>;
  professor?: Maybe<Professor>;
  likedBy: Array<Reviewer>;
  id: Scalars['Int'];
  createdDate: Scalars['DateTime'];
  modifiedDate: Scalars['DateTime'];
};

export type CourseReviewAggregate = {
  __typename?: 'CourseReviewAggregate';
  averageLiked: Scalars['Float'];
  averageEasiness: Scalars['Float'];
  averageUsefulness: Scalars['Float'];
  totalComments: Scalars['Int'];
  totalReviews: Scalars['Int'];
};

export type CourseReviewFilterInput = {
  and?: Maybe<Array<CourseReviewFilterInput>>;
  or?: Maybe<Array<CourseReviewFilterInput>>;
  reviewerId?: Maybe<IntOperationFilterInput>;
  reviewer?: Maybe<ReviewerFilterInput>;
  courseId?: Maybe<IntOperationFilterInput>;
  course?: Maybe<CourseFilterInput>;
  isLiked?: Maybe<BooleanOperationFilterInput>;
  easyRating?: Maybe<IntOperationFilterInput>;
  usefulRating?: Maybe<IntOperationFilterInput>;
  reviewText?: Maybe<StringOperationFilterInput>;
  professorId?: Maybe<IntOperationFilterInput>;
  professor?: Maybe<ProfessorFilterInput>;
  likedBy?: Maybe<ListFilterInputTypeOfReviewerFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
  createdDate?: Maybe<DateTimeOperationFilterInput>;
  modifiedDate?: Maybe<DateTimeOperationFilterInput>;
};

export type CreateCourseReviewInput = {
  isLiked: Scalars['Boolean'];
  easyRating: Scalars['Int'];
  usefulRating: Scalars['Int'];
  reviewText?: Maybe<Scalars['String']>;
  professorId?: Maybe<Scalars['Int']>;
  courseId: Scalars['Int'];
};

export type CreateCourseReviewPayload = {
  __typename?: 'CreateCourseReviewPayload';
  courseReview?: Maybe<CourseReview>;
};

export type DateTimeOperationFilterInput = {
  eq?: Maybe<Scalars['DateTime']>;
  neq?: Maybe<Scalars['DateTime']>;
  in?: Maybe<Array<Maybe<Scalars['DateTime']>>>;
  nin?: Maybe<Array<Maybe<Scalars['DateTime']>>>;
  gt?: Maybe<Scalars['DateTime']>;
  ngt?: Maybe<Scalars['DateTime']>;
  gte?: Maybe<Scalars['DateTime']>;
  ngte?: Maybe<Scalars['DateTime']>;
  lt?: Maybe<Scalars['DateTime']>;
  nlt?: Maybe<Scalars['DateTime']>;
  lte?: Maybe<Scalars['DateTime']>;
  nlte?: Maybe<Scalars['DateTime']>;
};

export type DecimalOperationFilterInput = {
  eq?: Maybe<Scalars['Decimal']>;
  neq?: Maybe<Scalars['Decimal']>;
  in?: Maybe<Array<Maybe<Scalars['Decimal']>>>;
  nin?: Maybe<Array<Maybe<Scalars['Decimal']>>>;
  gt?: Maybe<Scalars['Decimal']>;
  ngt?: Maybe<Scalars['Decimal']>;
  gte?: Maybe<Scalars['Decimal']>;
  ngte?: Maybe<Scalars['Decimal']>;
  lt?: Maybe<Scalars['Decimal']>;
  nlt?: Maybe<Scalars['Decimal']>;
  lte?: Maybe<Scalars['Decimal']>;
  nlte?: Maybe<Scalars['Decimal']>;
};

export enum DeliveryType {
  Undefined = 'UNDEFINED',
  Online = 'ONLINE',
  InPerson = 'IN_PERSON',
  Hybrid = 'HYBRID',
  Other = 'OTHER',
}

export type DeliveryTypeOperationFilterInput = {
  eq?: Maybe<DeliveryType>;
  neq?: Maybe<DeliveryType>;
  in?: Maybe<Array<DeliveryType>>;
  nin?: Maybe<Array<DeliveryType>>;
};

export type Faculty = {
  __typename?: 'Faculty';
  id: Scalars['Int'];
  enumBitmap?: Maybe<Scalars['String']>;
  name: Scalars['String'];
  abbreviation: Scalars['String'];
};

export type FacultyFilterInput = {
  and?: Maybe<Array<FacultyFilterInput>>;
  or?: Maybe<Array<FacultyFilterInput>>;
  id?: Maybe<IntOperationFilterInput>;
  enumBitmap?: Maybe<StringOperationFilterInput>;
  name?: Maybe<StringOperationFilterInput>;
  abbreviation?: Maybe<StringOperationFilterInput>;
};

export type IntOperationFilterInput = {
  eq?: Maybe<Scalars['Int']>;
  neq?: Maybe<Scalars['Int']>;
  in?: Maybe<Array<Maybe<Scalars['Int']>>>;
  nin?: Maybe<Array<Maybe<Scalars['Int']>>>;
  gt?: Maybe<Scalars['Int']>;
  ngt?: Maybe<Scalars['Int']>;
  gte?: Maybe<Scalars['Int']>;
  ngte?: Maybe<Scalars['Int']>;
  lt?: Maybe<Scalars['Int']>;
  nlt?: Maybe<Scalars['Int']>;
  lte?: Maybe<Scalars['Int']>;
  nlte?: Maybe<Scalars['Int']>;
};

export type ListFilterInputTypeOfCourseOfferingFilterInput = {
  all?: Maybe<CourseOfferingFilterInput>;
  none?: Maybe<CourseOfferingFilterInput>;
  some?: Maybe<CourseOfferingFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfCourseReviewFilterInput = {
  all?: Maybe<CourseReviewFilterInput>;
  none?: Maybe<CourseReviewFilterInput>;
  some?: Maybe<CourseReviewFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfProfessorFilterInput = {
  all?: Maybe<ProfessorFilterInput>;
  none?: Maybe<ProfessorFilterInput>;
  some?: Maybe<ProfessorFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfProfessorReviewFilterInput = {
  all?: Maybe<ProfessorReviewFilterInput>;
  none?: Maybe<ProfessorReviewFilterInput>;
  some?: Maybe<ProfessorReviewFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfReviewerFilterInput = {
  all?: Maybe<ReviewerFilterInput>;
  none?: Maybe<ReviewerFilterInput>;
  some?: Maybe<ReviewerFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfSectionFilterInput = {
  all?: Maybe<SectionFilterInput>;
  none?: Maybe<SectionFilterInput>;
  some?: Maybe<SectionFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListFilterInputTypeOfTimingDetailsFilterInput = {
  all?: Maybe<TimingDetailsFilterInput>;
  none?: Maybe<TimingDetailsFilterInput>;
  some?: Maybe<TimingDetailsFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type ListStringOperationFilterInput = {
  all?: Maybe<StringOperationFilterInput>;
  none?: Maybe<StringOperationFilterInput>;
  some?: Maybe<StringOperationFilterInput>;
  any?: Maybe<Scalars['Boolean']>;
};

export type Mutation = {
  __typename?: 'Mutation';
  createCourseReview: CreateCourseReviewPayload;
};

export type MutationCreateCourseReviewArgs = {
  input: CreateCourseReviewInput;
};

export type Professor = {
  __typename?: 'Professor';
  rating: ReviewAggregate;
  profCourses: Array<Course>;
  name: Scalars['String'];
  email: Scalars['String'];
  uwoId: Scalars['String'];
  rmpId?: Maybe<Scalars['String']>;
  professorReviews: Array<ProfessorReview>;
  courseReviews: Array<CourseReview>;
  sections: Array<Section>;
  id: Scalars['Int'];
};

export type ProfessorFilterInput = {
  and?: Maybe<Array<ProfessorFilterInput>>;
  or?: Maybe<Array<ProfessorFilterInput>>;
  name?: Maybe<StringOperationFilterInput>;
  email?: Maybe<StringOperationFilterInput>;
  uwoId?: Maybe<StringOperationFilterInput>;
  rmpId?: Maybe<StringOperationFilterInput>;
  professorReviews?: Maybe<ListFilterInputTypeOfProfessorReviewFilterInput>;
  courseReviews?: Maybe<ListFilterInputTypeOfCourseReviewFilterInput>;
  sections?: Maybe<ListFilterInputTypeOfSectionFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
};

export type ProfessorReview = {
  __typename?: 'ProfessorReview';
  quality: Scalars['Int'];
  difficulty: Scalars['Int'];
  helpful: Scalars['Int'];
  clarity: Scalars['Int'];
  reviewText: Scalars['String'];
  reviewerId: Scalars['Int'];
  reviewer: Reviewer;
  courseId?: Maybe<Scalars['Int']>;
  course?: Maybe<Course>;
  professorId: Scalars['Int'];
  professor: Professor;
  likedBy: Array<Reviewer>;
  id: Scalars['Int'];
  createdDate: Scalars['DateTime'];
  modifiedDate: Scalars['DateTime'];
};

export type ProfessorReviewFilterInput = {
  and?: Maybe<Array<ProfessorReviewFilterInput>>;
  or?: Maybe<Array<ProfessorReviewFilterInput>>;
  quality?: Maybe<IntOperationFilterInput>;
  difficulty?: Maybe<IntOperationFilterInput>;
  helpful?: Maybe<IntOperationFilterInput>;
  clarity?: Maybe<IntOperationFilterInput>;
  reviewText?: Maybe<StringOperationFilterInput>;
  reviewerId?: Maybe<IntOperationFilterInput>;
  reviewer?: Maybe<ReviewerFilterInput>;
  courseId?: Maybe<IntOperationFilterInput>;
  course?: Maybe<CourseFilterInput>;
  professorId?: Maybe<IntOperationFilterInput>;
  professor?: Maybe<ProfessorFilterInput>;
  likedBy?: Maybe<ListFilterInputTypeOfReviewerFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
  createdDate?: Maybe<DateTimeOperationFilterInput>;
  modifiedDate?: Maybe<DateTimeOperationFilterInput>;
};

export type Query = {
  __typename?: 'Query';
  jwtAsync: Scalars['String'];
  courseByCode?: Maybe<Course>;
  courses: Array<Course>;
  professors: Array<Professor>;
  reviewerById: Reviewer;
  courseById: Course;
  facultyById: Faculty;
  courseOfferingsByCourseId: Array<CourseOffering>;
  sectionsByCourseOfferingId: Array<Section>;
  timingDetailsBySectionId: Array<TimingDetails>;
};

export type QueryCourseByCodeArgs = {
  facultyAbbreviation: Scalars['String'];
  code: Scalars['Int'];
};

export type QueryCoursesArgs = {
  where?: Maybe<CourseFilterInput>;
};

export type QueryProfessorsArgs = {
  where?: Maybe<ProfessorFilterInput>;
};

export type QueryReviewerByIdArgs = {
  id: Scalars['Int'];
};

export type QueryCourseByIdArgs = {
  id: Scalars['Int'];
};

export type QueryFacultyByIdArgs = {
  id: Scalars['Int'];
};

export type QueryCourseOfferingsByCourseIdArgs = {
  courseId: Scalars['Int'];
};

export type QuerySectionsByCourseOfferingIdArgs = {
  courseOfferingId: Scalars['Int'];
};

export type QueryTimingDetailsBySectionIdArgs = {
  sectionId: Scalars['Int'];
};

export type ReviewAggregate = {
  __typename?: 'ReviewAggregate';
  averageQuality: Scalars['Float'];
  averageDifficulty: Scalars['Float'];
  averageHelpfulness: Scalars['Float'];
  averageClarity: Scalars['Float'];
  totalComments: Scalars['Int'];
  totalReviews: Scalars['Int'];
};

export type Reviewer = {
  __typename?: 'Reviewer';
  subjectId: Scalars['String'];
  courseReviewsWritten: Array<CourseReview>;
  professorReviewsWritten: Array<ProfessorReview>;
  courseReviewsLiked: Array<CourseReview>;
  professorReviewsLiked: Array<ProfessorReview>;
  id: Scalars['Int'];
};

export type ReviewerFilterInput = {
  and?: Maybe<Array<ReviewerFilterInput>>;
  or?: Maybe<Array<ReviewerFilterInput>>;
  subjectId?: Maybe<StringOperationFilterInput>;
  courseReviewsWritten?: Maybe<ListFilterInputTypeOfCourseReviewFilterInput>;
  professorReviewsWritten?: Maybe<ListFilterInputTypeOfProfessorReviewFilterInput>;
  courseReviewsLiked?: Maybe<ListFilterInputTypeOfCourseReviewFilterInput>;
  professorReviewsLiked?: Maybe<ListFilterInputTypeOfProfessorReviewFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
};

export type Section = {
  __typename?: 'Section';
  timingDetails?: Maybe<Array<Maybe<TimingDetails>>>;
  professors?: Maybe<Array<Maybe<Professor>>>;
  courseOffering: CourseOffering;
  number: Scalars['Int'];
  componentType: ComponentType;
  classNumber: Scalars['Int'];
  timetableRequisiteString?: Maybe<Scalars['String']>;
  waitListSize: Scalars['Int'];
  status: StatusType;
  campus: Campus;
  delivery: DeliveryType;
  professorNames: Array<Scalars['String']>;
  timingDetailsText: Scalars['String'];
  courseOfferingId: Scalars['Int'];
  id: Scalars['Int'];
};

export type SectionFilterInput = {
  and?: Maybe<Array<SectionFilterInput>>;
  or?: Maybe<Array<SectionFilterInput>>;
  number?: Maybe<IntOperationFilterInput>;
  componentType?: Maybe<ComponentTypeOperationFilterInput>;
  classNumber?: Maybe<IntOperationFilterInput>;
  timetableRequisiteString?: Maybe<StringOperationFilterInput>;
  waitListSize?: Maybe<IntOperationFilterInput>;
  status?: Maybe<StatusTypeOperationFilterInput>;
  campus?: Maybe<CampusOperationFilterInput>;
  delivery?: Maybe<DeliveryTypeOperationFilterInput>;
  professorNames?: Maybe<ListStringOperationFilterInput>;
  timingDetails?: Maybe<ListFilterInputTypeOfTimingDetailsFilterInput>;
  timingDetailsText?: Maybe<StringOperationFilterInput>;
  courseOfferingId?: Maybe<IntOperationFilterInput>;
  courseOffering?: Maybe<CourseOfferingFilterInput>;
  professors?: Maybe<ListFilterInputTypeOfProfessorFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
};

export enum StatusType {
  Undefined = 'UNDEFINED',
  Full = 'FULL',
  NotFull = 'NOT_FULL',
}

export type StatusTypeOperationFilterInput = {
  eq?: Maybe<StatusType>;
  neq?: Maybe<StatusType>;
  in?: Maybe<Array<StatusType>>;
  nin?: Maybe<Array<StatusType>>;
};

export type StringOperationFilterInput = {
  and?: Maybe<Array<StringOperationFilterInput>>;
  or?: Maybe<Array<StringOperationFilterInput>>;
  eq?: Maybe<Scalars['String']>;
  neq?: Maybe<Scalars['String']>;
  contains?: Maybe<Scalars['String']>;
  ncontains?: Maybe<Scalars['String']>;
  in?: Maybe<Array<Maybe<Scalars['String']>>>;
  nin?: Maybe<Array<Maybe<Scalars['String']>>>;
  startsWith?: Maybe<Scalars['String']>;
  nstartsWith?: Maybe<Scalars['String']>;
  endsWith?: Maybe<Scalars['String']>;
  nendsWith?: Maybe<Scalars['String']>;
};

export enum Suffix {
  NoSuffix = 'NO_SUFFIX',
  A = 'A',
  B = 'B',
  E = 'E',
  F = 'F',
  G = 'G',
  H = 'H',
  J = 'J',
  K = 'K',
  L = 'L',
  Q = 'Q',
  R = 'R',
  S = 'S',
  T = 'T',
  U = 'U',
  W = 'W',
  X = 'X',
  Y = 'Y',
  Z = 'Z',
}

export type SuffixOperationFilterInput = {
  eq?: Maybe<Suffix>;
  neq?: Maybe<Suffix>;
  in?: Maybe<Array<Suffix>>;
  nin?: Maybe<Array<Suffix>>;
};

export type TimingDetails = {
  __typename?: 'TimingDetails';
  daysOfWeekBitmap: Scalars['String'];
  location: Scalars['String'];
  time: Scalars['String'];
  sectionId: Scalars['Int'];
  section: Section;
  id: Scalars['Int'];
};

export type TimingDetailsFilterInput = {
  and?: Maybe<Array<TimingDetailsFilterInput>>;
  or?: Maybe<Array<TimingDetailsFilterInput>>;
  daysOfWeekBitmap?: Maybe<StringOperationFilterInput>;
  location?: Maybe<StringOperationFilterInput>;
  time?: Maybe<StringOperationFilterInput>;
  sectionId?: Maybe<IntOperationFilterInput>;
  section?: Maybe<SectionFilterInput>;
  id?: Maybe<IntOperationFilterInput>;
};

export type CourseInfoFragment = { __typename?: 'Course' } & Pick<
  Course,
  'id' | 'number' | 'facultyId' | 'code' | 'name' | 'description'
> & {
    faculty?: Maybe<{ __typename?: 'Faculty' } & Pick<Faculty, 'abbreviation'>>;
    courseOfferings?: Maybe<
      Array<
        Maybe<
          { __typename?: 'CourseOffering' } & Pick<CourseOffering, 'id'> & {
              sections?: Maybe<
                Array<
                  Maybe<
                    { __typename?: 'Section' } & {
                      professors?: Maybe<
                        Array<
                          Maybe<
                            { __typename?: 'Professor' } & Pick<
                              Professor,
                              'id' | 'uwoId' | 'name'
                            > & {
                                rating: {
                                  __typename?: 'ReviewAggregate';
                                } & Pick<
                                  ReviewAggregate,
                                  'averageQuality' | 'totalComments'
                                >;
                              }
                          >
                        >
                      >;
                    }
                  >
                >
              >;
            }
        >
      >
    >;
  };

export type CourseScheduleFragment = { __typename?: 'Course' } & Pick<
  Course,
  'id'
> & {
    courseOfferings?: Maybe<
      Array<
        Maybe<
          { __typename?: 'CourseOffering' } & Pick<
            CourseOffering,
            'id' | 'termId'
          > & {
              sections?: Maybe<
                Array<
                  Maybe<
                    { __typename?: 'Section' } & Pick<
                      Section,
                      | 'id'
                      | 'campus'
                      | 'number'
                      | 'classNumber'
                      | 'componentType'
                      | 'courseOfferingId'
                      | 'waitListSize'
                    > & {
                        courseOffering: {
                          __typename?: 'CourseOffering';
                        } & Pick<CourseOffering, 'termId'>;
                        professors?: Maybe<
                          Array<
                            Maybe<
                              { __typename?: 'Professor' } & Pick<
                                Professor,
                                'name' | 'uwoId' | 'id'
                              >
                            >
                          >
                        >;
                        timingDetails?: Maybe<
                          Array<
                            Maybe<
                              { __typename?: 'TimingDetails' } & Pick<
                                TimingDetails,
                                | 'daysOfWeekBitmap'
                                | 'id'
                                | 'location'
                                | 'sectionId'
                                | 'time'
                              >
                            >
                          >
                        >;
                      }
                  >
                >
              >;
            }
        >
      >
    >;
  };

export type CourseRequirementsFragment = { __typename?: 'Course' } & Pick<
  Course,
  'id' | 'antirequisites' | 'prerequisites' | 'corequisites'
> & {
    postrequisites?: Maybe<
      Array<
        Maybe<{ __typename?: 'Course' } & Pick<Course, 'id' | 'code' | 'name'>>
      >
    >;
  };

export type CourseRatingFragment = { __typename?: 'Course' } & Pick<
  Course,
  'id'
> & {
    rating: { __typename?: 'CourseReviewAggregate' } & Pick<
      CourseReviewAggregate,
      | 'averageLiked'
      | 'averageEasiness'
      | 'averageUsefulness'
      | 'totalReviews'
      | 'totalComments'
    >;
  };

export type CourseExploreFragment = { __typename?: 'Course' } & Pick<
  Course,
  | 'id'
  | 'number'
  | 'name'
  | 'facultyId'
  | 'code'
  | 'has_prereqs'
  | 'prof_ids'
  | 'terms'
> & {
    rating: { __typename?: 'CourseReviewAggregate' } & Pick<
      CourseReviewAggregate,
      'averageLiked' | 'averageEasiness' | 'averageUsefulness' | 'totalReviews'
    >;
  };

export type ProfExploreFragment = { __typename?: 'Professor' } & Pick<
  Professor,
  'email' | 'id' | 'name' | 'uwoId'
> & {
    rating: { __typename?: 'ReviewAggregate' } & Pick<
      ReviewAggregate,
      | 'averageQuality'
      | 'averageDifficulty'
      | 'averageHelpfulness'
      | 'averageClarity'
      | 'totalComments'
      | 'totalReviews'
    >;
    profCourses: Array<{ __typename?: 'Course' } & Pick<Course, 'code'>>;
  };

export type ProfInfoFragment = { __typename?: 'Professor' } & Pick<
  Professor,
  'id' | 'name' | 'uwoId'
>;

export type ProfCoursesTaughtFragment = { __typename?: 'Professor' } & Pick<
  Professor,
  'id'
> & {
    profCourses: Array<
      { __typename?: 'Course' } & Pick<Course, 'id' | 'name' | 'number'> & {
          faculty?: Maybe<
            { __typename?: 'Faculty' } & Pick<Faculty, 'abbreviation'>
          >;
        }
    >;
  };

export type ProfRatingFragment = { __typename?: 'Professor' } & Pick<
  Professor,
  'id'
> & {
    rating: { __typename?: 'ReviewAggregate' } & Pick<
      ReviewAggregate,
      | 'averageQuality'
      | 'averageDifficulty'
      | 'averageHelpfulness'
      | 'averageClarity'
      | 'totalComments'
      | 'totalReviews'
    >;
  };

export type GetCoursesQueryVariables = Exact<{
  facultyAbbreviation: Scalars['String'];
  number?: Maybe<Scalars['Int']>;
}>;

export type GetCoursesQuery = { __typename?: 'Query' } & {
  courses: Array<
    { __typename?: 'Course' } & CourseInfoFragment &
      CourseScheduleFragment &
      CourseRequirementsFragment &
      CourseRatingFragment
  >;
};

export type ExploreQueryVariables = Exact<{
  facultyAbbreviation: Scalars['String'];
}>;

export type ExploreQuery = { __typename?: 'Query' } & {
  courses: Array<{ __typename?: 'Course' } & CourseExploreFragment>;
  professors: Array<{ __typename?: 'Professor' } & ProfExploreFragment>;
};

export type GetProfQueryVariables = Exact<{
  uwoId?: Maybe<Scalars['String']>;
}>;

export type GetProfQuery = { __typename?: 'Query' } & {
  professors: Array<
    { __typename?: 'Professor' } & ProfInfoFragment &
      ProfCoursesTaughtFragment &
      ProfRatingFragment
  >;
};

export const CourseInfoFragmentDoc = gql`
  fragment CourseInfo on Course {
    id
    number
    facultyId
    faculty {
      abbreviation
    }
    code
    name
    description
    courseOfferings {
      id
      sections {
        professors {
          id
          uwoId
          name
          rating {
            averageQuality
            totalComments
          }
        }
      }
    }
  }
`;
export const CourseScheduleFragmentDoc = gql`
  fragment CourseSchedule on Course {
    id
    courseOfferings {
      id
      termId
      sections {
        id
        campus
        number
        classNumber
        componentType
        courseOfferingId
        courseOffering {
          termId
        }
        professors {
          name
          uwoId
          id
        }
        timingDetails {
          daysOfWeekBitmap
          id
          location
          sectionId
          time
        }
        waitListSize
      }
    }
  }
`;
export const CourseRequirementsFragmentDoc = gql`
  fragment CourseRequirements on Course {
    id
    antirequisites
    prerequisites
    corequisites
    postrequisites {
      id
      code
      name
    }
  }
`;
export const CourseRatingFragmentDoc = gql`
  fragment CourseRating on Course {
    id
    rating {
      averageLiked
      averageEasiness
      averageUsefulness
      totalReviews
      totalComments
    }
  }
`;
export const CourseExploreFragmentDoc = gql`
  fragment CourseExplore on Course {
    id
    number
    name
    facultyId
    code
    rating {
      averageLiked
      averageEasiness
      averageUsefulness
      totalReviews
    }
    has_prereqs
    prof_ids
    terms
  }
`;
export const ProfExploreFragmentDoc = gql`
  fragment ProfExplore on Professor {
    email
    id
    name
    uwoId
    rating {
      averageQuality
      averageDifficulty
      averageHelpfulness
      averageClarity
      totalComments
      totalReviews
    }
    profCourses {
      code
    }
  }
`;
export const ProfInfoFragmentDoc = gql`
  fragment ProfInfo on Professor {
    id
    name
    uwoId
  }
`;
export const ProfCoursesTaughtFragmentDoc = gql`
  fragment ProfCoursesTaught on Professor {
    id
    profCourses {
      id
      name
      number
      faculty {
        abbreviation
      }
    }
  }
`;
export const ProfRatingFragmentDoc = gql`
  fragment ProfRating on Professor {
    id
    rating {
      averageQuality
      averageDifficulty
      averageHelpfulness
      averageClarity
      totalComments
      totalReviews
    }
  }
`;
export const GetCoursesDocument = gql`
  query getCourses($facultyAbbreviation: String!, $number: Int) {
    courses(
      where: {
        and: [
          { number: { eq: $number } }
          { faculty: { abbreviation: { startsWith: $facultyAbbreviation } } }
        ]
      }
    ) {
      ...CourseInfo
      ...CourseSchedule
      ...CourseRequirements
      ...CourseRating
    }
  }
  ${CourseInfoFragmentDoc}
  ${CourseScheduleFragmentDoc}
  ${CourseRequirementsFragmentDoc}
  ${CourseRatingFragmentDoc}
`;

/**
 * __useGetCoursesQuery__
 *
 * To run a query within a React component, call `useGetCoursesQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetCoursesQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetCoursesQuery({
 *   variables: {
 *      facultyAbbreviation: // value for 'facultyAbbreviation'
 *      number: // value for 'number'
 *   },
 * });
 */
export function useGetCoursesQuery(
  baseOptions?: ApolloReactHooks.QueryHookOptions<
    GetCoursesQuery,
    GetCoursesQueryVariables
  >,
) {
  return ApolloReactHooks.useQuery<GetCoursesQuery, GetCoursesQueryVariables>(
    GetCoursesDocument,
    baseOptions,
  );
}
export function useGetCoursesLazyQuery(
  baseOptions?: ApolloReactHooks.LazyQueryHookOptions<
    GetCoursesQuery,
    GetCoursesQueryVariables
  >,
) {
  return ApolloReactHooks.useLazyQuery<
    GetCoursesQuery,
    GetCoursesQueryVariables
  >(GetCoursesDocument, baseOptions);
}
export type GetCoursesQueryHookResult = ReturnType<typeof useGetCoursesQuery>;
export type GetCoursesLazyQueryHookResult = ReturnType<
  typeof useGetCoursesLazyQuery
>;
export type GetCoursesQueryResult = ApolloReactCommon.QueryResult<
  GetCoursesQuery,
  GetCoursesQueryVariables
>;
export const ExploreDocument = gql`
  query explore($facultyAbbreviation: String!) {
    courses(
      where: { faculty: { abbreviation: { startsWith: $facultyAbbreviation } } }
    ) {
      ...CourseExplore
    }
    professors {
      ...ProfExplore
    }
  }
  ${CourseExploreFragmentDoc}
  ${ProfExploreFragmentDoc}
`;

/**
 * __useExploreQuery__
 *
 * To run a query within a React component, call `useExploreQuery` and pass it any options that fit your needs.
 * When your component renders, `useExploreQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useExploreQuery({
 *   variables: {
 *      facultyAbbreviation: // value for 'facultyAbbreviation'
 *   },
 * });
 */
export function useExploreQuery(
  baseOptions?: ApolloReactHooks.QueryHookOptions<
    ExploreQuery,
    ExploreQueryVariables
  >,
) {
  return ApolloReactHooks.useQuery<ExploreQuery, ExploreQueryVariables>(
    ExploreDocument,
    baseOptions,
  );
}
export function useExploreLazyQuery(
  baseOptions?: ApolloReactHooks.LazyQueryHookOptions<
    ExploreQuery,
    ExploreQueryVariables
  >,
) {
  return ApolloReactHooks.useLazyQuery<ExploreQuery, ExploreQueryVariables>(
    ExploreDocument,
    baseOptions,
  );
}
export type ExploreQueryHookResult = ReturnType<typeof useExploreQuery>;
export type ExploreLazyQueryHookResult = ReturnType<typeof useExploreLazyQuery>;
export type ExploreQueryResult = ApolloReactCommon.QueryResult<
  ExploreQuery,
  ExploreQueryVariables
>;
export const GetProfDocument = gql`
  query getProf($uwoId: String) {
    professors(where: { uwoId: { eq: $uwoId } }) {
      ...ProfInfo
      ...ProfCoursesTaught
      ...ProfRating
    }
  }
  ${ProfInfoFragmentDoc}
  ${ProfCoursesTaughtFragmentDoc}
  ${ProfRatingFragmentDoc}
`;

/**
 * __useGetProfQuery__
 *
 * To run a query within a React component, call `useGetProfQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetProfQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetProfQuery({
 *   variables: {
 *      uwoId: // value for 'uwoId'
 *   },
 * });
 */
export function useGetProfQuery(
  baseOptions?: ApolloReactHooks.QueryHookOptions<
    GetProfQuery,
    GetProfQueryVariables
  >,
) {
  return ApolloReactHooks.useQuery<GetProfQuery, GetProfQueryVariables>(
    GetProfDocument,
    baseOptions,
  );
}
export function useGetProfLazyQuery(
  baseOptions?: ApolloReactHooks.LazyQueryHookOptions<
    GetProfQuery,
    GetProfQueryVariables
  >,
) {
  return ApolloReactHooks.useLazyQuery<GetProfQuery, GetProfQueryVariables>(
    GetProfDocument,
    baseOptions,
  );
}
export type GetProfQueryHookResult = ReturnType<typeof useGetProfQuery>;
export type GetProfLazyQueryHookResult = ReturnType<typeof useGetProfLazyQuery>;
export type GetProfQueryResult = ApolloReactCommon.QueryResult<
  GetProfQuery,
  GetProfQueryVariables
>;
