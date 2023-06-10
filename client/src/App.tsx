import { Route, Routes, BrowserRouter } from "react-router-dom";
import HomePresenter from "./presenters/HomePresenter";
import { SearchContext } from "./contexts/SearchContext";
import { CoursePresenter } from "./presenters/CoursePresenter";
import { ProfessorPresenter } from "./presenters/ProfessorPresenter";

function App() {

  return (
    <div className="roboto">
      <SearchContext>
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePresenter} />
          <Route path="/course/:courseCode" Component={CoursePresenter} />
          <Route path="/professor/:professorName" Component={ProfessorPresenter} />
        </Routes>
      </BrowserRouter>  
    </SearchContext>
    </div>
    
  );
}

export default App;