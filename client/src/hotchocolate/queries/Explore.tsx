import gql from 'graphql-tag';
import ExploreFragment from 'hotchocolate/fragments/ExploreFragment';

export const EXPLORE_QUERY = gql`
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
  ${ExploreFragment.courseExplore}
  ${ExploreFragment.profExplore}
`;
