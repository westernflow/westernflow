import Navbar from "../components/Navbar";
import ContentContainer from "../components/ContentContainer";
import graphql from "babel-plugin-relay/macro";
import {useLazyLoadQuery} from "react-relay";
import type {CoursePresenterCourseQuery} from "./__generated__/CoursePresenterCourseQuery.graphql";

const CoursePresenterCourseQuery = graphql`
    query CoursePresenterCourseQuery {
        courseByCode(code: 2553, facultyName: "ACTURSCI"){
            name
            number
            courseOfferings {
                sections {
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
	const indexedCourses = useLazyLoadQuery<CoursePresenterCourseQuery>(
		CoursePresenterCourseQuery,
		{}
	);
	
	console.log(indexedCourses)
	
	return (
		<ContentContainer>
			<Navbar />
			
		</ContentContainer>
	)
}