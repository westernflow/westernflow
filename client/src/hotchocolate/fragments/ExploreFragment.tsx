import gql from 'graphql-tag';

const ExploreFragment = {
  courseExplore: gql`
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
  `,
  profExplore: gql`
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
  `,
};

export default ExploreFragment;
