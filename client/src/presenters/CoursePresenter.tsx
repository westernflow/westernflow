import Navbar from "../components/Navbar";
import ContentContainer from "../components/ContentContainer";
import graphql from "babel-plugin-relay/macro";
import {useLazyLoadQuery} from "react-relay";
import type {CoursePresenterCourseQuery} from "./__generated__/CoursePresenterCourseQuery.graphql";
import {useParams} from "react-router-dom";
import {ContentCard} from "../components/ContentCard";
import {SectionsPresenter} from "../components/SectionsPresenter";

const GetCourseByCodeQuery = graphql`
    query CoursePresenterCourseQuery($code: Int!, $facultyAbbreviation: String!) {
        courseByCode(code: $code, facultyAbbreviation: $facultyAbbreviation){
            id
            name
            number
	        description
            faculty {
                id
                abbreviation
            }
            courseOfferings {
                sections {
                    componentType
                    number
                    classNumber
                    professorNames
                    status
                    waitListSize
                    campus
                    delivery
                    timingDetails {
                        daysOfWeekBitmap
                        time
                        location
                    }
                }
            } 
        }
    }
`;

export function CoursePresenter() {
	const {courseCode} = useParams();
	const [facultyAbbreviation, codeString] = courseCode ? courseCode.split("-") : [null, null];
	const code = parseInt(codeString ? codeString : "");

	const courseData = useLazyLoadQuery<CoursePresenterCourseQuery>(
		GetCourseByCodeQuery,
		{
			code: isNaN(code) ? -1 : code,
			facultyAbbreviation: facultyAbbreviation ? facultyAbbreviation.toUpperCase() : ""
		},
	).courseByCode;
	
	console.log(courseData);
	
	return (
		<ContentContainer>
			<Navbar/>
			<ContentCard classNames="flex-row my-6">
				<h2 className="text-indigo-600 text-2xl">{courseData.name}</h2>
				<p className="text-gray-600">{courseData.faculty?.abbreviation + " " + courseData.number}</p>
				
				<p className="text-gray-600 mt-3">{courseData.description}</p>
			</ContentCard>
			
			<ContentCard classNames="flex-row my-6">
				<h2 className="text-indigo-600 text-xl">Course Offerings</h2>
				<div className={'my-6'}>
					<SectionsPresenter/>
				</div>
			</ContentCard>
		</ContentContainer>
	)
}