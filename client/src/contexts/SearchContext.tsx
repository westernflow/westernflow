import { ReactNode, createContext, useEffect, useState } from "react";
import {
	IndexedCourse,
	IndexedProfessor,
	IndexedSearchData,
} from "../models/search";
import { Trie } from "../trie/trie";

export const searchContext = createContext<{
	courseData?: IndexedCourse[];
	profData?: IndexedProfessor[];
	courseTrie?: Trie;
	professorTrie?: Trie;
}>({});

export const SearchContext = ({ children }: { children: ReactNode }) => {
	// use state which holds the results from the query and passes it into the provider as the value for the consumer
	const [data, setData] = useState<IndexedSearchData | undefined>();
	const [courseData, setCourseData] = useState<IndexedCourse[] | undefined>();
	const [profData, setProfData] = useState<IndexedProfessor[] | undefined>();

	const [courseTrie, setCourseTrie] = useState<Trie>(new Trie());
	const [professorTrie, setProfessorTrie] = useState<Trie>(new Trie());
	// use effect which fetches the data from our local endpoint
	// grab url from env
	// const apiUrl = process.env.REACT_APP_API_URL || "http://backend-service:8080";
	const apiUrl = "http://backend-service:8080";
	useEffect(() => {
		fetch(apiUrl + "/api/v1/indexed_search_data")
			.then((result) => result.json())
			.then((resultJson) => {
				setData(resultJson as IndexedSearchData);
			});
	}, []);

	useEffect(() => {
		setCourseData(data?.indexed_courses);
		setProfData(data?.indexed_profs);
	}, [data]);

	useEffect(() => {
		// create a new trie from the course data
		const newCourseTrie = new Trie();
		courseData?.forEach((course) => {
			newCourseTrie.insert(
				(course.faculty + course.number).toLowerCase()
			);
		});
		setCourseTrie(newCourseTrie);
	}, [courseData]);

	useEffect(() => {
		// create a new trie from the professor data
		const newProfessorTrie = new Trie();
		profData?.forEach((prof) => {
			newProfessorTrie.insert(prof.name.toLowerCase());
		});
		setProfessorTrie(newProfessorTrie);
	}, [profData]);

	return (
		<searchContext.Provider
			value={{
				courseData: courseData,
				profData: profData,
				courseTrie: courseTrie,
				professorTrie: professorTrie,
			}}
		>
			{children}
		</searchContext.Provider>
	);
};
