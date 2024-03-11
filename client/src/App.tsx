import {Route, Routes, BrowserRouter} from "react-router-dom";
import {ProfessorPresenter} from "./presenters/ProfessorPresenter";
import {CoursePresenterSuspenseHandler} from "./components/Suspense/CoursePresenterSuspenseHandler";
import {RelayEnvironmentProvider} from "react-relay";
import {RelayEnvironment} from "./RelayEnvironment";
import Home from "./presenters/HomePresenter";

function App() {
	return (
		<RelayEnvironmentProvider environment={RelayEnvironment}>
			<div className="roboto">
				<BrowserRouter>
					<Routes>
						<Route path="/" Component={Home}/>
						<Route path="/course/:courseCode" Component={CoursePresenterSuspenseHandler}/>
						<Route path="/professor/:professorName" Component={ProfessorPresenter}/>
					</Routes>
				</BrowserRouter>
			</div>
		</RelayEnvironmentProvider>
	)
}

export default App;