import { data } from "autoprefixer";
import React, { useState, useRef, useEffect, useCallback } from "react";
import { Navigate, useNavigate } from "react-router-dom";

function SearchBar (){
  const history = useNavigate();

  const searchRef = useRef<HTMLInputElement>(null);

  const [input, setInput] = useState<string>("");
  const [showResults, setShowResults] = useState(false);
  const [searchResults, setSearchResults] = useState<string[]>([]);

  useEffect(() => {
    function handleClickOutside(event: any) {
      if (searchRef.current && !searchRef.current.contains(event.target)) { // check if user clicked on outside (not contained in event.target)
        setShowResults(false);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [searchRef]);

  const goToSearchResult = (result : string) => {
    // remove spaceas from result
    result = result.replace(/\s/g, '');
    history(`/course/${result}`);
  }

  const transformData = (data: string[]): string[] => {
    // first filter for the first 5 elements
    data = data.filter((_, i) => i < 8);

    // split the left and right side of the data into course name and course number by looking for the first numeric character
    data = data.map((result) => {
      const index = result.search(/\d/);
      return result.slice(0, index) + " " + result.slice(index);
    })
    
    return data
  }


  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInput(e.target.value);
    if (e.target.value.length > 0) {
      setShowResults(true);
      fetch(`http://localhost:8080/api/v1/search/${e.target.value}`)
        .then((res) => res.json())
        .then((data: string[]) => setSearchResults(transformData(data)));
    } else {
      setShowResults(false);
    }
  }

  return (
    <div ref={searchRef} className="flex flex-col border-2 border-gray-300">
          <input
            onFocus={() => setShowResults(true)}
            className="truncate flex-grow py-3 px-4 text-gray-700 leading-tight text-lg focus:outline-1 focus:border-blue-500"
            id="search"
            type="text"
            autoComplete="off"
            placeholder="Search for a professor or course..."
            onChange={(e) => handleInputChange(e)}
          />
          {showResults ? searchResults.map((result, i) => (<div key={i} onClick={() => goToSearchResult(result)} className="font-bold border-t-2 border-b-1 border-gray-300 bg-white flex-grow py-2 px-4 text-gray-700 leading-tight text-lg focus:outline-1 hover:bg-blue-500 hover:text-white cursor-pointer">{result}</div>)) : null}
        </div>
  )
}

export default SearchBar