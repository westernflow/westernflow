import React from 'react';
import { useQuery } from 'react-apollo';
import { Helmet } from 'react-helmet';
import { useSelector } from 'react-redux';
import { useRouteMatch } from 'react-router-dom';
import {
  CourseInfoFragment,
  CourseRatingFragment,
  CourseRequirementsFragment,
  CourseScheduleFragment,
  GetCoursesQuery,
  GetCoursesQueryVariables,
} from 'hc-generated/graphql';
import { GET_COURSE } from 'hotchocolate/queries/Course';

import LoadingSpinner from 'components/display/LoadingSpinner';
import Button from 'components/input/Button';
import LikeCourseToggle from 'components/input/LikeCourseToggle';
import { DEFAULT_ERROR, NOT_FOUND } from 'constants/Messages';
import { AUTH_MODAL, COURSE_REVIEW_COURSE_MODAL } from 'constants/Modal';
import { getIsBrowserDesktop, RootState } from 'data/reducers/RootReducer';
import { GET_COURSE_WITH_USER_DATA } from 'graphql/queries/course/Course';
import useModal from 'hooks/useModal';
import NotFoundPage from 'pages/notFoundPage/NotFoundPage';
import { getUserId } from 'utils/Auth';
import { formatCourseCode } from 'utils/Misc';

import {
  Column1,
  Column2,
  ColumnWrapper,
  CoursePageWrapper,
  CourseQuestionTextAndToggle,
  CourseReviewQuestionBox,
  CourseReviewQuestionText,
  ReviewWrapper,
} from './styles/CoursePage';
import CourseInfoHeader from './CourseInfoHeader';
import CourseRequisites from './CourseRequisites';
import CourseReviews from './CourseReviews';
import CourseSchedule from './CourseSchedule';

type Course = CourseInfoFragment &
  CourseScheduleFragment &
  CourseRequirementsFragment &
  CourseRatingFragment;

type CoursePageContentProps = {
  course: Course;
};

const CoursePageContent = ({ course }: CoursePageContentProps) => {
  const isBrowserDesktop = useSelector(getIsBrowserDesktop);

  const Schedule = (
    <CourseSchedule
      offerings={course.courseOfferings}
      courseCode={course.code}
      courseId={course.id}
    />
  );

  return (
    <>
      <CourseInfoHeader course={course} shortlisted={false} />
      <ColumnWrapper>
        {isBrowserDesktop && Schedule}
        <Column1>
          <ReviewWrapper>
            {!isBrowserDesktop && Schedule}
            {(false || false) && (
              <CourseReviewQuestionBox>
                <CourseQuestionTextAndToggle>
                  <CourseReviewQuestionText>
                    What do you think of {formatCourseCode(course.code)}?
                  </CourseReviewQuestionText>
                </CourseQuestionTextAndToggle>
                <Button
                  width={isBrowserDesktop ? 'max-content' : '100%'}
                  padding="16px 24px"
                  handleClick={() => {}}
                >
                  {false ? 'Edit your review' : 'Add your review'}
                </Button>
              </CourseReviewQuestionBox>
            )}
          </ReviewWrapper>
          {/* <CourseReviews courseId={course.id} profsTeaching={[]} /> */}
        </Column1>
        <Column2>
          <CourseRequisites
            courseCode={course.code}
            prereqs={course.prerequisites}
            antireqs={course.antirequisites}
            coreqs={course.corequisites}
            postreqs={[]}
          />
        </Column2>
      </ColumnWrapper>
    </>
  );
};

const CoursePage = () => {
  const match = useRouteMatch<{ courseCode: string }>();

  const courseCode = match.params.courseCode.toLowerCase();
  const query = GET_COURSE;
  // example code is 'educ2222'
  const variables = {
    facultyAbbreviation: courseCode.match(/^[a-z]+/i)![0].toUpperCase(),
    number: parseInt(courseCode.match(/\d+$/i)![0], 10),
    // ...(isLoggedIn && { user_id: getUserId() }),
  };

  const { loading, error, data } = useQuery<
    GetCoursesQuery,
    GetCoursesQueryVariables
  >(query, { variables });

  const renderContent = (queryData: GetCoursesQuery) => {
    const courses = queryData.courses[0];

    return (
      <>
        <CoursePageContent course={courses} />
      </>
    );
  };

  return loading ? (
    <CoursePageWrapper>
      <LoadingSpinner />
    </CoursePageWrapper>
  ) : error || !data ? (
    <NotFoundPage text={DEFAULT_ERROR} title="" />
  ) : data.courses.length === 0 ? (
    <NotFoundPage text={NOT_FOUND.course} />
  ) : (
    <CoursePageWrapper>
      <Helmet>
        <title>
          {formatCourseCode(data.courses[0].code)} - {data.courses[0].name} -
          UWO Flow
        </title>
        <meta name="description" content={data.courses[0].description!} />
        <meta
          property="og:title"
          content={`${formatCourseCode(data.courses[0].code)} - ${
            data.courses[0].name
          } - UWO Flow`}
        />
        <meta
          property="og:description"
          content={data.courses[0].description!}
        />
      </Helmet>
      {renderContent(data)}
    </CoursePageWrapper>
  );
};

export default CoursePage;
