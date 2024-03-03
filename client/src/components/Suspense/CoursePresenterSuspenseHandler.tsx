import {Suspense} from "react";
import {CoursePresenter} from "../../presenters/CoursePresenter";

export function CoursePresenterSuspenseHandler() {
	return (
		<Suspense fallback={<div>Loading...</div>}>
			<CoursePresenter/>
		</Suspense>
	);
}