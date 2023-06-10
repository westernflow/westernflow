import { ContentContainer, YellowPurpleGradiant } from "../constants/styleNames";
import { SearchBar } from "../shared/SearchBar";

function HomePresenter() {
  return (
    <div className={`flex flex-col h-screen justify-center ${YellowPurpleGradiant}`}>\
      <div className={`flex flex-col  mx-auto p-8 m-10 w-2/3 ${ContentContainer}`}>
        <p className="text-5xl text-white font-bold mb-3">Western Review</p>
        <p className="text-gray-50 text-2xl mb-3">
          Course and professor reviewing site for University of Western Ontario
          students
        </p>
        <SearchBar textColor="text-gray-700" bgColor="bg-slate-50" rounded={false} />
      </div>
    </div>
  );
}
export default HomePresenter;
