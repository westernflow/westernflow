import {Route, Routes, BrowserRouter} from "react-router-dom";
import {ProfessorPresenter} from "./presenters/ProfessorPresenter";
import {CoursePresenterSuspenseHandler} from "./components/Suspense/CoursePresenterSuspenseHandler";
import {HomePresenterSuspenseHandler} from "./components/Suspense/HomePresenterSuspenseHandler";
import {GoogleOAuthProvider} from "@react-oauth/google";

function App() {
	return (
		<GoogleOAuthProvider
			clientId="1089561640895-rp6i32897q7lcmu3ktn5efmvgmnouabu.apps.googleusercontent.com">
			<div className="roboto">
				<BrowserRouter>
					<Routes>
						<Route path="/" Component={HomePresenterSuspenseHandler}/>
						<Route path="/course/:courseCode" Component={CoursePresenterSuspenseHandler}/>
						<Route path="/professor/:professorName" Component={ProfessorPresenter}/>
					</Routes>
				</BrowserRouter>
			</div>
		</GoogleOAuthProvider>
	)
}

export default App;