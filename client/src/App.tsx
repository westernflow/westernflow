import {Route, Routes, BrowserRouter} from "react-router-dom";
import {RelayEnvironmentProvider} from "react-relay";
import {RelayEnvironment} from "./RelayEnvironment";
import Home from "./presenters/HomePresenter";
import {CoursePresenter} from "./presenters/CoursePresenter";
import {Suspense} from "react";

const SuspendedCoursePresenter = () => {
	return (
		<Suspense fallback={<div>Loading...</div>}>
			<CoursePresenter/>
		</Suspense>
	)
}

function App() {
	return (
		<RelayEnvironmentProvider environment={RelayEnvironment}>
			<div className="roboto">
				<BrowserRouter>
					<Routes>
						<Route path="/" Component={Home}/>
						<Route path="/course/:courseCode"
						       Component={SuspendedCoursePresenter}/>
					</Routes>
				</BrowserRouter>
			</div>
		</RelayEnvironmentProvider>
	)
}

export default App;