import { Route, Routes, BrowserRouter } from "react-router-dom";
import HomePresenter from "./components/HomePresenter";
import { SearchContext } from "./contexts/SearchContext";
import { useContext } from "react";
import { CoursePresenter } from "./components/CoursePresenter";

function App() {

  return (
    <div className="roboto">
      <SearchContext>
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePresenter} />
          <Route path="/course/:courseCode" Component={CoursePresenter} />
        </Routes>
      </BrowserRouter>  
    </SearchContext>
    </div>
    
  );
}

export default App;