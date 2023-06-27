import { useLocation, useParams } from "react-router-dom";
import { Course } from "../models/common";
import { useEffect, useState } from "react";
import SectionPresenter from "./SectionPresenter";
import {
	ContentContainer,
	YellowPurpleGradiant,
	sectionHeaderStyle,
} from "../constants/styleNames";
import { Navbar } from "../shared/Navbar";

export const CoursePresenter = () => {
	const location = useLocation();
	const { courseCode } = useParams();
	const [course, setCourse] = useState<Course>();
	const [isCollapsed, setIsCollapsed] = useState<boolean>(false);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		setLoading(true);
		if (courseCode != null) {
			// we should expect the url to be in the format /[CourseName][CourseNumber], where the two are split via the first numeric character's appearance
			var courseFaculty = "";
			var courseNumber = "";

			for (var i = 0; i < courseCode.length; i++) {
				if (!isNaN(Number(courseCode[i]))) {
					courseNumber = courseCode.slice(i);
					break;
				}
				courseFaculty += courseCode[i];
			}
			const queryString = `http://localhost:8080/api/v1/courses?course-faculty=exact:${courseFaculty.toUpperCase()}&course-number=gte:${courseNumber}&course-number=lte:${courseNumber}`;
			fetch(queryString)
				.then((res) => res.json())
				.then((res) => {
					//console.log("Done Loading");
					if (res && res.length > 0) setCourse(res[0] as Course);
					setLoading(false);
				});
		}
	}, [location.search, courseCode]);

	if (loading)
		return (
			<div>
				<Navbar />
				<div className="flex h-screen items-center justify-center">
					<span className="loading loading-spinner loading-lg"></span>
				</div>
			</div>
		);

	return (
		<div
			className="text-gray-700"
			style={{ position: "relative", minHeight: "100vh" }}
		>
			<div style={{ position: "sticky", top: 0 }}>
				<Navbar />
			</div>
			<div className="flex flex-col min-h-screen">
				<div className={`${YellowPurpleGradiant} text-yellow-50 pt-10 pb-4`}>
					<h1 className={`${ContentContainer} text-3xl font-bold`}>
						{course?.courseData?.faculty +
							" " +
							course?.courseData?.number +
							course?.courseData?.suffix}
					</h1>
					<h2 className={`${ContentContainer} text-2xl`}>
						{course?.courseData?.name}
					</h2>
				</div>

				<div className="bg-slate-50">
					<h3 className={`${ContentContainer} text-m pt-10 pb-10`}>
						{course?.courseData?.description}
					</h3>
				</div>
				<div className="pt-5 pb-10 flex-grow bg-slate-100">
					<div
						className={`${ContentContainer} border-blue-500 bg-slate-50 shadow-2xl`}
					>
						<div className={`text-3xl font-bold text-gray-700 border-b-2 flex`}>
							<div className="p-3">Course Schedule</div>
							<div className="flex-grow"></div>
							<div className="flex bg-slate-300">
								<button
									className="pl-3 pr-3"
									onClick={() => setIsCollapsed(!isCollapsed)}
								>
									{isCollapsed ? (
										<svg
											xmlns="http://www.w3.org/2000/svg"
											fill="none"
											viewBox="0 0 24 24"
											strokeWidth={1.5}
											stroke="currentColor"
											className="w-6 h-6 justify-center align-middle"
										>
											<path
												strokeLinecap="round"
												strokeLinejoin="round"
												d="M4.5 15.75l7.5-7.5 7.5 7.5"
											/>
										</svg>
									) : (
										<svg
											xmlns="http://www.w3.org/2000/svg"
											fill="none"
											viewBox="0 0 24 24"
											strokeWidth={1.5}
											stroke="currentColor"
											className="w-6 h-6 justify-center align-middle"
										>
											<path
												strokeLinecap="round"
												strokeLinejoin="round"
												d="M19.5 8.25l-7.5 7.5-7.5-7.5"
											/>
										</svg>
									)}
								</button>
							</div>
						</div>
						<div className="overflow-x-auto">
							{!isCollapsed && (
								<div
									className="p-4 grid grid-cols-8 text-base"
									style={gridStyle}
								>
									<div className={`${sectionHeaderStyle}`}>Section</div>
									<div className={`${sectionHeaderStyle}`}>Class</div>
									<div className={`${sectionHeaderStyle}`}>Status</div>
									<div className={`${sectionHeaderStyle}`}>Days</div>
									<div className={`${sectionHeaderStyle}`}>Time</div>
									<div className={`${sectionHeaderStyle}`}>Location</div>
									<div className={`${sectionHeaderStyle}`}>Instructor</div>
									<div className={`${sectionHeaderStyle}`}>Campus</div>
									<>
										{course?.courseData != null
											? course?.sectionData.map((section, i) => {
													return <SectionPresenter key={i} section={section} />;
											  })
											: null}
									</>
								</div>
							)}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

const gridStyle = {
	minWidth: "500px",
};
