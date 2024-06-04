import gql from 'graphql-tag';

const ProfFragment = {
  profInfo: gql`
    fragment ProfInfo on Professor {
      id
      name
      uwoId
    }
  `,
  profCoursesTaught: gql`
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
  `,
  profRating: gql`
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
  `,
};

export default ProfFragment;
