import { useNavigate } from "react-router-dom";
import { ContentContainer } from "../constants/styleNames";
import { SearchBar } from "./SearchBar";

export const Navbar = () => {
  const history = useNavigate();

  return (
    <div className="bg-white flex">
      <div className={`${ContentContainer} mt-3 mb-3`}>
        <div className="flex">
          <div
            className="pr-5 text-xl items-center justify-start hover:cursor-pointer sm:flex hidden font-bold"
            onClick={() => history("/")}
          >
            <span className="pr-2">UWO</span>
            <span className="text-blue-500">Review</span>
          </div>
          <div className="grow">
            <SearchBar
              textColor="text-gray-700"
              bgColor="bg-gray-300"
              rounded={true}
            />
          </div>
          <div className="flex pl-4 text-lg items-center justify-center">
            <span>Log in</span>
          </div>
        </div>
      </div>
    </div>
  );
};
