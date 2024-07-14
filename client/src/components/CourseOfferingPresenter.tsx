import graphql from "babel-plugin-relay/macro";
import {CourseOfferingPresenter_offeringData$key} from "./__generated__/CourseOfferingPresenter_offeringData.graphql";
import {useFragment} from "react-relay";
import {useEffect, useState} from "react";
import {StatusType} from "../models/enums";

const CourseOfferingPresenterFragment = graphql`
    fragment CourseOfferingPresenter_offeringData on Course {
        courseOfferings {
            sections {
	            id
                componentType
                number
                classNumber
                professorNames
                status
                waitListSize
                campus
                delivery
                timingDetails {
                    daysOfWeekBitmap
                    time
                    location
                }
            }
        }
    }
`;

type CourseOfferingPresenterProps = {
	offeringData: CourseOfferingPresenter_offeringData$key,
}

export function CourseOfferingPresenter(props: CourseOfferingPresenterProps) {
	const offeringData = useFragment(CourseOfferingPresenterFragment, props.offeringData);

	const [selectedOffering, setSelectedOffering] = useState(offeringData.courseOfferings ? offeringData.courseOfferings[0] : null);

	useEffect(() => {
		if (offeringData.courseOfferings) {
			setSelectedOffering(offeringData.courseOfferings[0]);
		}
	}, [offeringData]);

	return (
		<div className="mt-8 w-full">
			<div className="-my-2 overflow-x-auto lg:-mx-8">
				<div className="py-2 align-middle lg:px-8">
					<div className="shadow ring-1 ring-black ring-opacity-5 sm:rounded-lg">
						<table className="w-full divide-y divide-gray-300">
							<thead className="bg-gray-50">
							<tr>
								<th scope="col"
								    className="py-3.5 px-4 text-left text-sm font-semibold text-gray-900 sm:pz-6">
									Component
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Section
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Class Number
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Instructors
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Times and Locations
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Status
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Campus
								</th>
								<th scope="col"
								    className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">
									Delivery
								</th>
							</tr>
							</thead>
							<tbody className="divide-y divide-gray-200 bg-slate-500">
							{selectedOffering?.sections?.map((section, sectionIndex) => (
								<tr key={sectionIndex} className="hover:bg-gray-50">
									<td className="py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-6">
										{section?.componentType}
									</td>
									<td className="px-3 py-4 text-sm text-gray-500">{section?.number}</td>
									<td className="px-3 py-4 text-sm text-gray-500">{section?.classNumber}</td>
									<td className="px-3 py-4 text-sm text-gray-500">
										{section?.professorNames?.map((name, professorIndex) => (
											<div key={professorIndex}>{name}</div>
										))}
									</td>
									<td className="px-3 py-4 text-sm text-gray-500">{section?.timingDetails?.map(
										(detail, timingDetailsIndex) => (
											<div key={timingDetailsIndex} className="py-2">
												<p className="font-semibold">{detail?.location}</p>
												<p>{detail?.time}</p>
												<p><span
													className={(detail?.daysOfWeekBitmap[0] === "1" ? "font-semibold" : "text-gray-500") + " pr-1"}>M</span>
													<span
														className={(detail?.daysOfWeekBitmap[1] === "1" ? "font-semibold" : "text-gray-500") + " pr-1"}>Tu</span>
													<span
														className={(detail?.daysOfWeekBitmap[2] === "1" ? "font-semibold" : "text-gray-500") + " pr-1"}>W</span>
													<span
														className={(detail?.daysOfWeekBitmap[3] === "1" ? "font-semibold" : "text-gray-500") + " pr-1"}>Th</span>
													<span
														className={(detail?.daysOfWeekBitmap[4] === "1" ? "font-semibold" : "text-gray-500") + " pr-1"}>F</span>
												</p>
											</div>
										)
									)}</td>
									<td className="px-3 py-4 text-sm text-gray-500">
										{section?.status === StatusType.Full ? "FULL" : "OPEN"}
									</td>
									<td className="px-3 py-4 text-sm text-gray-500">{section?.campus}</td>
									<td className="px-3 py-4 text-sm text-gray-500">{section?.delivery}</td>
								</tr>
							))}
							</tbody>
						</table>
					</div>
				</div>
			</div>
		</div>
	)
}
