import React from 'react';
import { CourseInfoFragment, CourseRatingFragment } from 'hc-generated/graphql';

import RatingBox, { RATING_BOX_WIDTH } from 'components/display/RatingBox';
import ShortlistStar from 'components/input/ShortlistStar';
import { formatCourseCode } from 'utils/Misc';

import {
  CourseCode,
  CourseCodeAndNameSection,
  CourseCodeAndStar,
  CourseDescriptionSection,
  CourseInfoHeaderWrapper,
  CourseName,
  CourseNameWrapper,
  Description,
  RatingsSection,
  StarAlignmentWrapper,
} from './styles/CourseInfoHeader';

type CourseInfoHeaderProps = {
  course: CourseInfoFragment & CourseRatingFragment;
  shortlisted: boolean;
};

const CourseInfoHeader = ({ course, shortlisted }: CourseInfoHeaderProps) => {
  const {
    averageLiked,
    averageEasiness,
    averageUsefulness,
    totalComments,
    totalReviews,
  } = course.rating!;

  return (
    <CourseInfoHeaderWrapper>
      <CourseCodeAndNameSection>
        <CourseCodeAndStar>
          <CourseCode ratingBoxWidth={RATING_BOX_WIDTH}>
            {formatCourseCode(course.code)}
          </CourseCode>
          <StarAlignmentWrapper>
            <ShortlistStar
              size={36}
              initialState={shortlisted}
              courseId={course.id}
              courseCode={course.code}
            />
          </StarAlignmentWrapper>
        </CourseCodeAndStar>
        <CourseNameWrapper>
          <CourseName ratingBoxWidth={RATING_BOX_WIDTH}>
            {course.name}
          </CourseName>
        </CourseNameWrapper>
      </CourseCodeAndNameSection>
      <CourseDescriptionSection>
        <RatingsSection>
          <RatingBox
            numRatings={totalReviews}
            numComments={totalComments}
            percentages={[
              {
                displayName: 'Likes',
                percent: averageLiked,
              },
              {
                displayName: 'Easy',
                percent: averageEasiness,
              },
              {
                displayName: 'Useful',
                percent: averageUsefulness,
              },
            ]}
          />
        </RatingsSection>
        <Description ratingBoxWidth={RATING_BOX_WIDTH}>
          {course.description}
        </Description>
      </CourseDescriptionSection>
    </CourseInfoHeaderWrapper>
  );
};

export default CourseInfoHeader;
