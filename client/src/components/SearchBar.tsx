import { Combobox } from "@headlessui/react";
import graphql from "babel-plugin-relay/macro";
import { useEffect, useRef, useState } from "react";
import { useLazyLoadQuery } from "react-relay";
import { useNavigate } from "react-router-dom";
import type { SearchBarIndexedCoursesQuery as SearchBarIndexedCoursesQueryType } from "./__generated__/SearchBarIndexedCoursesQuery.graphql";

const SearchBarIndexedCoursesQuery = graphql`
    query SearchBarIndexedCoursesQuery {
        courses(first: 5000) {
            nodes {
                name
                facultyId
                faculty {
                    abbreviation
                }
                number
            }
        }
    }
`;

type SearchCourse = {
    name: string;
    facultyAbbreviation: string;
    number: number;
};

function classNames(...classes: string[]) {
    return classes.filter(Boolean).join(" ");
}

function filterCourses(courses: SearchCourse[], query: string) {
    query = query.replace(/\s/g, "").toLowerCase();

    if (query === "" || query.length < 1) return [];
    return courses
        .filter((course) => {
            return (
                course.name.toLowerCase().includes(query) ||
                (course.facultyAbbreviation + course.number.toString())
                    .toLowerCase()
                    .includes(query)
            );
        })
        .slice(0, 10);
}

export default function SearchBar() {
    const [query, setQuery] = useState("");
    const [selectedCourse, setSelectedCourse] = useState<SearchCourse | null>(
        null,
    );
    const [courses, setCourses] = useState<SearchCourse[]>([]);
    const inputRef = useRef<HTMLInputElement>(null);
    const indexedCourses = useLazyLoadQuery<SearchBarIndexedCoursesQueryType>(
        SearchBarIndexedCoursesQuery,
        {},
    );
    const navigate = useNavigate();

    useEffect(() => {
        if (indexedCourses?.courses?.nodes == null) {
            return;
        }

        const mappedCourses = indexedCourses.courses.nodes.map((courseNode) => {
            return {
                name: courseNode.name,
                facultyAbbreviation: courseNode.faculty!.abbreviation,
                number: courseNode.number,
            };
        });

        setCourses(mappedCourses);
    }, [indexedCourses]);

    useEffect(() => {
        if (inputRef?.current != null) {
            inputRef.current.focus();
        }
    }, []);

    useEffect(() => {
        if (selectedCourse != null) {
            navigate(
                "/course/" +
                    selectedCourse.facultyAbbreviation.toLowerCase() +
                    "-" +
                    selectedCourse.number.toString(),
            );
        }
    }, [selectedCourse, navigate]);

    const filteredCourses = filterCourses(courses, query);

    return (
        <Combobox as="div" value={selectedCourse} onChange={setSelectedCourse}>
            <div className="relative mt-2">
                <Combobox.Input
                    ref={inputRef}
                    autoComplete={"off"}
                    className="w-full rounded-md border-0 bg-white py-1.5 pl-3 pr-10 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                    onChange={(event) => setQuery(event.target.value)}
                    displayValue={(course: SearchCourse) =>
                        course
                            ? course.facultyAbbreviation +
                              " " +
                              course.number.toString() +
                              " — " +
                              course.name
                            : ""
                    }
                    placeholder={"Search for courses, professors, or subjects"}
                />

                {filteredCourses.length > 0 && (
                    <Combobox.Options className="absolute z-10 mt-1 max-h-60 w-full overflow-auto rounded-md bg-white py-1 text-base shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none sm:text-sm">
                        {filteredCourses.map((course) => (
                            <Combobox.Option
                                key={
                                    course.facultyAbbreviation +
                                    course.number.toString()
                                }
                                value={course}
                                className={({ active }) =>
                                    classNames(
                                        "relative cursor-default select-none py-2 pl-8 pr-4",
                                        active
                                            ? "bg-indigo-600 text-white"
                                            : "text-gray-900",
                                    )
                                }
                            >
                                {({ active, selected }) => (
                                    <div>
                                        <span
                                            className={classNames(
                                                "block truncate",
                                            )}
                                        >
                                            <span
                                                className={classNames(
                                                    "font-semibold",
                                                )}
                                            >
                                                {course.facultyAbbreviation +
                                                    " " +
                                                    course.number.toString()}
                                            </span>{" "}
                                            {" — " + course.name}
                                        </span>
                                        {selected && (
                                            <span
                                                className={classNames(
                                                    "absolute inset-y-0 left-0 flex items-center pl-1.5",
                                                    active
                                                        ? "text-white"
                                                        : "text-indigo-600",
                                                )}
                                            ></span>
                                        )}
                                    </div>
                                )}
                            </Combobox.Option>
                        ))}
                    </Combobox.Options>
                )}
            </div>
        </Combobox>
    );
}
