import gql from 'graphql-tag';
import CourseFragment from 'hotchocolate/fragments/CourseFragment';

export const GET_COURSE = gql`
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
  ${CourseFragment.courseInfo}
  ${CourseFragment.courseSchedule}
  ${CourseFragment.courseRequirements}
  ${CourseFragment.courseRating}
`;
