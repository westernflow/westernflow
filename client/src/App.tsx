import { Route, Routes, BrowserRouter } from "react-router-dom";
import { ProfessorPresenter } from "./presenters/ProfessorPresenter";
import {CoursePresenterSuspenseHandler} from "./components/Suspense/CoursePresenterSuspenseHandler";
import {HomePresenterSuspenseHandler} from "./components/Suspense/HomePresenterSuspenseHandler";

function App() {

  return (
    <div className="roboto">
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePresenterSuspenseHandler} />
          <Route path="/course/:courseCode" Component={CoursePresenterSuspenseHandler} />
          <Route path="/professor/:professorName" Component={ProfessorPresenter} />
        </Routes>
      </BrowserRouter>  
    </div>
    
  );
}

export default App;