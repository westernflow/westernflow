import {Route, Routes, BrowserRouter} from "react-router-dom";
import {RelayEnvironmentProvider} from "react-relay";
import {RelayEnvironment} from "./RelayEnvironment";
import Home from "./presenters/HomePresenter";
import {CoursePresenter} from "./presenters/CoursePresenter";

function App() {
	return (
		<RelayEnvironmentProvider environment={RelayEnvironment}>
			<div className="roboto">
				<BrowserRouter>
					<Routes>
						<Route path="/" Component={Home}/>
						<Route path="/course/:courseCode"
						       Component={CoursePresenter}/>
					</Routes>
				</BrowserRouter>
			</div>
		</RelayEnvironmentProvider>
	)
}

export default App;