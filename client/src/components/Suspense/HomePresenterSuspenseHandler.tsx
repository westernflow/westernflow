import {Suspense} from "react";
import Home from "../../presenters/HomePresenter";

export function HomePresenterSuspenseHandler() {
	return (
		<Suspense fallback={<div>Loading...</div>}>
			<Home />
		</Suspense>
	);
}