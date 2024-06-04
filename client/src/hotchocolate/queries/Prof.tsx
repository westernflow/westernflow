import gql from 'graphql-tag';
import ProfFragment from 'hotchocolate/fragments/ProfFragment';

export const GET_PROF = gql`
  query getProf($uwoId: String) {
    professors(where: { uwoId: { eq: $uwoId } }) {
      ...ProfInfo
      ...ProfCoursesTaught
      ...ProfRating
    }
  }
  ${ProfFragment.profInfo}
  ${ProfFragment.profCoursesTaught}
  ${ProfFragment.profRating}
`;
