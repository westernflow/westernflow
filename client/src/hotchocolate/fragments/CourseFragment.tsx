import gql from 'graphql-tag';

const CourseFragment = {
  courseInfo: gql`
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
  `,
  courseSchedule: gql`
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
  `,
  courseRequirements: gql`
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
  `,
  courseRating: gql`
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
  `,
};

export default CourseFragment;
