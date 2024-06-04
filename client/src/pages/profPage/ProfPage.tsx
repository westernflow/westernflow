import React from 'react';
import { useQuery } from 'react-apollo';
import { Helmet } from 'react-helmet';
import { useRouteMatch } from 'react-router-dom';
import {
  GetProfQuery,
  GetProfQueryVariables,
  ProfCoursesTaughtFragment,
  ProfInfoFragment,
  ProfRatingFragment,
} from 'hc-generated/graphql';
import { GET_PROF } from 'hotchocolate/queries/Prof';

import LoadingSpinner from 'components/display/LoadingSpinner';
import { DEFAULT_ERROR, NOT_FOUND } from 'constants/Messages';
import NotFoundPage from 'pages/notFoundPage/NotFoundPage';
import ProfInfoHeader from 'pages/profPage/ProfInfoHeader';
import ProfReviews from 'pages/profPage/ProfReviews';

import {
  Column1,
  Column2,
  ColumnWrapper,
  ProfPageWrapper,
} from './styles/ProfPage';

type ProfPageContentProps = {
  prof: ProfInfoFragment & ProfCoursesTaughtFragment & ProfRatingFragment;
};

const ProfPageContent = ({ prof }: ProfPageContentProps) => {
  return (
    <>
      <ProfInfoHeader prof={prof} />
      <ColumnWrapper>
        <Column1>
          <ProfReviews profId={prof.id} />
        </Column1>
        <Column2 />
      </ColumnWrapper>
    </>
  );
};

export const ProfPage = () => {
  const match = useRouteMatch<{ profCode: string }>();

  const profCode = match.params.profCode.toLowerCase();
  const { loading, error, data } = useQuery<
    GetProfQuery,
    GetProfQueryVariables
  >(GET_PROF, {
    variables: { uwoId: profCode },
  });

  return loading ? (
    <ProfPageWrapper>
      <LoadingSpinner />
    </ProfPageWrapper>
  ) : error || !data ? (
    <NotFoundPage text={DEFAULT_ERROR} title="" />
  ) : data.professors.length === 0 ? (
    <NotFoundPage text={NOT_FOUND.prof} />
  ) : (
    <ProfPageWrapper>
      <Helmet>
        <title>{data.professors[0].name} - UWO Flow</title>
        <meta
          name="description"
          content={`Professor ${data.professors[0].name} at the University of Waterloo.`}
        />
      </Helmet>
      <ProfPageContent prof={data.professors[0]} />
    </ProfPageWrapper>
  );
};

export default ProfPage;
