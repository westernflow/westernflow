import { Spacer, VStack } from "@chakra-ui/react";
import graphql from "babel-plugin-relay/macro";
import { useLazyLoadQuery } from "react-relay";
import { useParams } from "react-router-dom";
import { ContentCard } from "../components/ContentCard";
import ContentContainer from "../components/ContentContainer";
import { CourseOfferingPresenter } from "../components/CourseOfferingPresenter";
import Footer from "../components/Footer";
import Navbar from "../components/Navbar";
import type { CoursePresenterCourseQuery } from "./__generated__/CoursePresenterCourseQuery.graphql";

const GetCourseByCodeQuery = graphql`
    query CoursePresenterCourseQuery(
        $code: Int!
        $facultyAbbreviation: String!
    ) {
        courseByCode(code: $code, facultyAbbreviation: $facultyAbbreviation) {
            id
            name
            number
            description
            faculty {
                id
                abbreviation
            }
            ...CourseOfferingPresenter_offeringData
        }
    }
`;

export function CoursePresenter() {
    const { courseCode } = useParams();
    const [facultyAbbreviation, codeString] = courseCode
        ? courseCode.split("-")
        : [null, null];
    const code = parseInt(codeString ? codeString : "");

    const courseData = useLazyLoadQuery<CoursePresenterCourseQuery>(
        GetCourseByCodeQuery,
        {
            code: isNaN(code) ? -1 : code,
            facultyAbbreviation: facultyAbbreviation
                ? facultyAbbreviation.toUpperCase()
                : "",
        },
    ).courseByCode;

    return (
        <div className="bg-wf">
            <ContentContainer additionalClasses="border bg-slate-100">
                <VStack className="min-h-screen">
                    <Navbar />
                    <ContentCard classNames="w-full">
                        <h2 className="text-indigo-600 text-2xl">
                            {courseData.name}
                        </h2>
                        <p className="text-gray-600">
                            {courseData.faculty?.abbreviation +
                                " " +
                                courseData.number}
                        </p>
                    </ContentCard>
                    <ContentCard classNames="my-6 w-full">
                        <div>
                            <p className="text-gray-600 mt-3">
                                {courseData.description}
                            </p>
                        </div>
                    </ContentCard>

                    <div className={"my-3 w-screen md:w-full"}>
                        <CourseOfferingPresenter offeringData={courseData} />
                    </div>
                    <Spacer />
                    <Footer />
                </VStack>
            </ContentContainer>
        </div>
    );
}
