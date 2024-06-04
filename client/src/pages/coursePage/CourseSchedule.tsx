import React, { useState } from 'react';
import { CourseScheduleFragment, Section } from 'hc-generated/graphql';
import moment from 'moment/moment';

import LastUpdatedSchedule from 'components/common/LastUpdatedSchedule';
import CollapsibleContainer from 'components/display/CollapsibleContainer';
import TabContainer from 'components/display/TabContainer';
import Table from 'components/display/Table';
import { SECTION_ORDER } from 'constants/CourseSection';
import { termCodeToDate } from 'utils/Misc';

import {
  CourseScheduleWrapper,
  ScheduleTableWrapper,
} from './styles/CourseSchedule';
import { courseScheduleTableColumns } from './CourseScheduleTableColumns';

const getStartingTab = (termsOffered: number[]) => {
  for (let i = 0; i < termsOffered.length; i += 1) {
    const term = termsOffered[i];
    const month = term % 10;
    const year = 1900 + Math.floor(term / 10);
    const currentTime = moment();
    const termStart = moment(`${month}-${year}`, 'MM-YYYY');
    const displayStart = termStart.clone().subtract(2, 'months');
    const displayEnd = termStart.clone().add(2, 'months');
    if (currentTime.isBetween(displayStart, displayEnd)) {
      return i;
    }
  }
  return 0;
};

type CourseScheduleProps = {
  courseCode: string;
  courseId: number;
  offerings: CourseScheduleFragment['courseOfferings'];
};

const CourseSchedule = ({
  courseCode,
  courseId,
  offerings = [],
}: CourseScheduleProps) => {
  let termsOffered = offerings!.reduce((acc: number[], curr) => {
    if (curr && curr.termId) {
      if (!acc.includes(curr.termId)) {
        acc.push(curr.termId);
      }
    }
    return acc;
  }, []);

  termsOffered = termsOffered.sort().reverse();

  const startingTab = getStartingTab(termsOffered);
  const [selectedTerm, setSelectedTerm] = useState(startingTab);

  if (offerings!.length === 0) {
    return null;
  }

  const sections = offerings!.reduce((acc: Section[], curr) => {
    if (curr && curr.sections !== undefined && curr.sections != null) {
      for (const section of curr.sections) {
        if (section) {
          acc.push(section as Section);
        }
      }
    }
    return acc;
  }, []);

  const sectionsCleanedData = sections
    .map((s) => ({
      section: `${s.componentType} ${s.number}`,
      dates: s.timingDetails?.map((t) => t!.daysOfWeekBitmap) || [],
      times: s.timingDetails?.map((t) => t!.time) || [],
      locations: s.timingDetails?.map((t) => t!.location) || [],
      profs: s.professors ?? [],
      class: s.classNumber,
      term: s.courseOffering.termId,
      enrolled: {
        course_id: courseId,
        course_code: courseCode,
        section_id: s.id,
        filled: false,
        capacity: 0,
        hasBell: false,
        selected: false,
      },
      // Every grouping contains a single time of day, location, and instructor
      // and the classes that occur with those parameters.
    }))
    .sort((a, b) => {
      const sectionTypeA = a.section.split(' ')[0];
      const sectionTypeB = b.section.split(' ')[0];
      if (SECTION_ORDER[sectionTypeA] === SECTION_ORDER[sectionTypeB]) {
        return a.section.localeCompare(b.section);
      }
      return SECTION_ORDER[sectionTypeA] - SECTION_ORDER[sectionTypeB];
    });

  const tabList = termsOffered.map((term) => {
    return {
      title: termCodeToDate(term),
      render: () => (
        <>
          <ScheduleTableWrapper>
            <Table
              cellPadding="4px 0"
              columns={courseScheduleTableColumns}
              data={sectionsCleanedData.filter((c) => c.term === term)}
              getRowProps={(row: any) =>
                row ? { disabled: row.original.cancelled } : {}
              }
            />
          </ScheduleTableWrapper>
        </>
      ),
      onClick: () => null,
    };
  });

  return (
    <CourseScheduleWrapper>
      <CollapsibleContainer
        title="Course Schedule"
        centerHeader={false}
        margin="0"
        headerBorder
        bigTitle
      >
        <TabContainer
          initialSelectedTab={startingTab}
          tabList={tabList}
          contentPadding={'0'}
          borderRadius={false}
          onChange={setSelectedTerm}
        />
      </CollapsibleContainer>
      {tabList.length > 0 && (
        <LastUpdatedSchedule
          margin={'8px 0 32px 0'}
          courseCode={courseCode}
          term={termsOffered[selectedTerm]}
          updatedAt={moment(Date.now())}
        />
      )}
    </CourseScheduleWrapper>
  );
};

export default CourseSchedule;
