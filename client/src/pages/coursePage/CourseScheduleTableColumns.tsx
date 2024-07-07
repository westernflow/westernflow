import React, { Fragment } from 'react';
import { Cell } from 'react-table';
import { getProfPageRoute } from 'Routes';

import { LAB, LEC, TUT } from 'constants/CourseSection';
import { weekDayLetters } from 'utils/Misc';

import { Professor } from '../../hc-generated/graphql';

import ScheduleNotificationBell from './ScheduleNotificationBell';
import {
  BoldWeekDay,
  ColorBar,
  ContentWrapper,
  EnrollmentText,
  GreyWeekDay,
  InstructorLink,
  NormalCellWrapper,
  SectionCellWrapper,
  SectionContentWrapper,
  SpaceMargin,
} from './styles/CourseSchedule';

type CellProps = {
  cell: Cell<object, any>;
};

const contentSpace = (spaces: number) => {
  const content = [];
  for (let i = 0; i < spaces; i += 1) {
    content.push(<ContentWrapper key={i} />);
  }
  return content;
};

const processWeekDays = (days: string[]) =>
  weekDayLetters.map((day) =>
    days.includes(day) ? (
      <BoldWeekDay key={day}>{day}</BoldWeekDay>
    ) : (
      <GreyWeekDay key={day}>{day}</GreyWeekDay>
    ),
  );

const SectionCell = ({ cell }: CellProps) => (
  <SectionCellWrapper numRows={cell.value.numRows}>
    <ColorBar
      color={
        cell.value.includes(LEC) ? LEC : cell.value.includes(LAB) ? LAB : TUT
      }
    />
    <SectionContentWrapper>{cell.value}</SectionContentWrapper>
  </SectionCellWrapper>
);

const ClassCell = ({ cell }: CellProps) => (
  <NormalCellWrapper>
    <ContentWrapper>{cell.value}</ContentWrapper>
  </NormalCellWrapper>
);

const EnrolledCell = ({ cell }: CellProps) => (
  <NormalCellWrapper>
    <ContentWrapper>
      {(cell.value.filled >= cell.value.capacity || cell.value.selected) && (
        <ScheduleNotificationBell
          key={cell.value.section_id}
          sectionId={cell.value.section_id}
          courseId={cell.value.course_id}
          initialState={cell.value.selected}
          userEmail={cell.value.userEmail}
        />
      )}
      <EnrollmentText
        filled={cell.value.filled >= cell.value.capacity}
        hasBell={cell.value.hasBell}
      >
        {cell.value.filled}/{cell.value.capacity}
      </EnrollmentText>
    </ContentWrapper>
  </NormalCellWrapper>
);

const TimeCell = ({ cell }: CellProps) => (
  <NormalCellWrapper>
    {console.log('CellProps', cell)}
    {cell.value.map((cl: any, idx: number) => (
      <Fragment key={idx}>
        {contentSpace(cl.spaces)}
        <ContentWrapper>{cl}</ContentWrapper>
        {idx < cell.value.length - 1 && <SpaceMargin />}
      </Fragment>
    ))}
  </NormalCellWrapper>
);

const DateCell = ({ cell }: CellProps) => (
  <NormalCellWrapper>
    {cell.value.length === 0 && <ContentWrapper />}
    {cell.value.map((dowBitmap: string, idx: number) => (
      <Fragment key={idx}>{dowBitmap}</Fragment>
    ))}
  </NormalCellWrapper>
);

const LocationCell = ({ cell }: CellProps) => (
  <NormalCellWrapper>
    {cell.value.map((cl: any, idx: number) => (
      <Fragment key={idx}>
        <ContentWrapper>{cl}</ContentWrapper>
        {idx < cell.value.length - 1 && <SpaceMargin />}
      </Fragment>
    ))}
  </NormalCellWrapper>
);

const InstructorCell = ({ cell }: CellProps) => (
  <NormalCellWrapper>
    {cell.value.map((cl: Professor, idx: number) =>
      cl.uwoId ? (
        <Fragment key={idx}>
          <InstructorLink to={getProfPageRoute(cl.uwoId)} key={idx}>
            {cl.name}
          </InstructorLink>
          {idx < cell.value.length - 1 && <SpaceMargin />}
        </Fragment>
      ) : (
        <Fragment key={idx}>
          {idx < cell.value.length - 1 && <SpaceMargin />}
        </Fragment>
      ),
    )}
  </NormalCellWrapper>
);

export const courseScheduleTableColumns = [
  {
    Header: 'Section',
    Cell: SectionCell,
    accessor: 'section',
    style: {
      padding: 0,
    },
    maxWidth: 160,
  },
  {
    Header: 'Class',
    Cell: ClassCell,
    accessor: 'class',
  },
  {
    Header: 'Enrolled',
    Cell: EnrolledCell,
    accessor: 'enrolled',
  },
  {
    Header: 'Time',
    Cell: TimeCell,
    accessor: 'times',
  },
  {
    Header: 'Date',
    Cell: DateCell,
    accessor: 'dates',
    style: {
      paddingBottom: 16,
    },
  },
  {
    Header: 'Location',
    Cell: LocationCell,
    accessor: 'locations',
  },
  {
    Header: 'Instructor',
    Cell: InstructorCell,
    accessor: 'profs',
  },
];
