import { useState, useContext, createContext, useEffect, ReactNode } from 'react';
import { IndexedCourse, IndexedProfessor, IndexedSearchData } from '../models/search';


export const searchContext = createContext<{ courseData?: IndexedCourse[]; profData?: IndexedProfessor[] }>({});

export const SearchContext = ({ children }:{ children: ReactNode }) => {
  // use state which holds the results from the query and passes it into the provider as the value for the consumer
  const [data, setData] = useState<IndexedSearchData | undefined>()
  const [courseData, setCourseData] = useState<IndexedCourse[] | undefined>();
  const [profData, setProfData] = useState<IndexedProfessor[] | undefined>();
  // use effect which fetches the data from our local endpoint
  useEffect(()=>
  {
    const response = fetch("http://localhost:8080/api/v1/indexed_search_data").then(result => result.json()).then(resultJson => {setData(resultJson as IndexedSearchData); console.log("sanity check",data);});
  }, [])

  useEffect(()=>{
    setCourseData(data?.indexed_courses)
    setProfData(data?.indexed_profs)
  }, [data])

  return (
    <searchContext.Provider value={{courseData: courseData, profData: profData}}>
      {children}
    </searchContext.Provider>
  )
}