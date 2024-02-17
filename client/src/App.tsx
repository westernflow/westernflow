import { Route, Routes, BrowserRouter } from "react-router-dom";
import HomePresenter from "./presenters/HomePresenter";
import { CoursePresenter } from "./presenters/CoursePresenter";
import { ProfessorPresenter } from "./presenters/ProfessorPresenter";

function App() {

  return (
    <div className="roboto">
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePresenter} />
          <Route path="/course/:courseCode" Component={CoursePresenter} />
          <Route path="/professor/:professorName" Component={ProfessorPresenter} />
        </Routes>
      </BrowserRouter>  
    </div>
    
  );
}

export default App;