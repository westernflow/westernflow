import React, { useState, useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import graphql from 'babel-plugin-relay/macro'
import type { SearchBarIndexedCoursesQuery as SearchBarIndexedCoursesQueryType } from "./__generated__/SearchBarIndexedCoursesQuery.graphql";
import { useLazyLoadQuery } from "react-relay";
import { Trie } from "../trie/trie";

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

type SearchResult = {
  course?: string;
  professor?: string;
  callback: () => void;
};

export const SearchBar = (props: { bgColor: string; textColor: string; rounded: boolean }) => {
  const [courseTrie, setCourseTrie] = useState<Trie>(new Trie());
  const [professorTrie, setProfessorTrie] = useState<Trie>(new Trie());
  const indexedCourses = useLazyLoadQuery<SearchBarIndexedCoursesQueryType>(
    SearchBarIndexedCoursesQuery,
    {}
  );

  useEffect(() => {
    const professorTrie = new Trie(); // none yet
    const courseTrie = new Trie();
    indexedCourses?.courses?.nodes?.forEach(courseNode => 
    {
      courseTrie.insert((courseNode?.faculty?.abbreviation + " " + courseNode?.number.toString()).toLowerCase())
    })

    setCourseTrie(courseTrie)
    setProfessorTrie(professorTrie)
  }, [indexedCourses])

  const { bgColor, textColor, rounded } = props;

  const history = useNavigate();

  const searchRef = useRef<HTMLInputElement>(null);
  const [showResults, setShowResults] = useState(false);
  const [searchResults, setSearchResults] = useState<SearchResult[]>([]);

  useEffect(() => {
    function handleClickOutside(event: any) {
      if (searchRef.current && !searchRef.current.contains(event.target)) {
        // check if user clicked on outside (not contained in event.target)
        setShowResults(false);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [searchRef]);

  const goToCourse = (result: string) => {
    // remove spaceas from result
    result = result.replace(/\s/g, "");
    history(`/course/${result}`);
    setShowResults(false);
    // reset the search bar text
    if (searchRef.current) {
      searchRef.current.value = "";
    }
  };

  const goToProfessor = (result: string) => {
    // remove spaceas from result
    result = result.replace(/\s/g, "");
    history(`/professor/${result}`);
    setShowResults(false);
    if (searchRef.current) {
      searchRef.current.value = "";
    }
  };

  const transformCourseTrieData = (data: string[]): string[] => {
    // split the left and right side of the data into course name and course number by looking for the first numeric character
    data = data.map((result) => {
      const index = result.search(/\d/);
      return result.slice(0, index) + " " + result.slice(index);
    });

    // capitalize all letters
    data = data.map((result) => result.toUpperCase());

    return data;
  };

  const transformProfessorTrieData = (data: string[]): string[] => {
    // split the string and capitalize the first letter of each word
    data = data.map((result) => {
      const words = result.split(" ");
      return words
        .map((word) => word.charAt(0).toUpperCase() + word.slice(1))
        .join(" ");
    });

    return data;
  };

  const getResultsFromTrie = (input: string) => {
    if (courseTrie == null || professorTrie == null) return;
    // assume it is a course for now
    const courseResults = transformCourseTrieData(
      courseTrie?.getKWords(input, 4)
    );
    // courseResultsObject = courseResults.map((result) => {course: result, callback: goToCourse(result)})
    const courseResultsObject = courseResults.map((result) => ({
      course: result,
      callback: () => goToCourse(result),
    }));

    const professorResults = transformProfessorTrieData(
      professorTrie?.getKWords(input, 2)
    );

    const professorResultsObject = professorResults.map((result) => ({
      professor: result,
      callback: () => goToProfessor(result),
    }));

    const results = [...courseResultsObject, ...professorResultsObject];
    if (results) {
      setSearchResults(results);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.value.length > 0) {
      getResultsFromTrie(e.target.value);
      setShowResults(true);
    } else {
      setShowResults(false);
    }
  };

  return (
    <div ref={searchRef} className={`relative flex ${bgColor} ${rounded ? "rounded-xl" : null}`}>
      <div className="flex flex-col justify-center item-center pl-2 pr-2">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
          className="w-5 h-5"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
          />
        </svg>
      </div>

      <input
        onFocus={() => setShowResults(true)}
        className={`truncate flex-grow py-3 px-4 ${textColor} leading-tight text-md outline-none ${bgColor} rounded-xl`}
        id="search"
        type="text"
        autoComplete="off"
        placeholder="Search for a professor or course..."
        onChange={(e) => handleInputChange(e)}
      />
      {showResults ? (
        <div className="absolute z-50 w-full mt-11">
          {searchResults.map((searchResult, i) => (
            <div
              key={i}
              onClick={searchResult.callback}
              className="font-bold border-b-2 bg-white flex-grow py-2 px-4 text-gray-700 leading-tight text-lg focus:outline-1 hover:bg-blue-500 hover:text-white cursor-pointer"
            >
              {searchResult.course
                ? searchResult.course
                : searchResult.professor}
            </div>
          ))}
        </div>
      ) : null}
    </div>
  );
};
