import {Route, Routes, BrowserRouter} from "react-router-dom";
import {RelayEnvironmentProvider} from "react-relay";
import {createRelayEnvironment} from "./RelayEnvironment";
import Home from "./presenters/HomePresenter";
import {CoursePresenter} from "./presenters/CoursePresenter";
import {Suspense, useEffect, useState} from "react";
import {useAuth0} from "@auth0/auth0-react";
import RelayModernEnvironment from "relay-runtime/lib/store/RelayModernEnvironment";

const SuspendedCoursePresenter = () => {
	return (
		<Suspense fallback={<div>Loading...</div>}>
			<CoursePresenter/>
		</Suspense>
	)
}

function App() {
	const { getAccessTokenSilently } = useAuth0();
	const [relayEnvironment, setRelayEnvironment] = useState<RelayModernEnvironment>();

	useEffect(() => {
		console.log("Relay environment has changed..")
		const tokenProvider = {
			getToken: () => getAccessTokenSilently().catch(() => ""),
		};

		setRelayEnvironment(createRelayEnvironment(tokenProvider));
	}, [getAccessTokenSilently]);
	
	if (!relayEnvironment) {
		return <div>Loading Relay environment...</div>;
	}

	return (
		<RelayEnvironmentProvider environment={relayEnvironment}>
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