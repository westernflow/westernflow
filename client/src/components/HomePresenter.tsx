import SearchBar from "./SearchBar";
import { SearchContext, searchContext } from "../contexts/SearchContext";
import { useContext } from "react";

function HomePresenter() {
  const { courseData, profData } = useContext(searchContext)

  return (
    <div className="flex flex-col rounded-lg bg-gradient-to-r from-purple-400 via-pink-500 to-yellow-500 h-screen justify-center">\
      <div className="flex flex-col  mx-auto p-8 m-10 w-2/3">
        <p className="text-5xl text-white font-bold mb-3">Western Review</p>
        <p className="text-gray-50 text-2xl mb-3">
          Course and professor reviewing site for University of Western Ontario
          students
        </p>
        <SearchBar />
      </div>
    </div>
  );
}
export default HomePresenter;
