import graphql from "babel-plugin-relay/macro";
import {CourseOfferingPresenter_offeringData$key} from "./__generated__/CourseOfferingPresenter_offeringData.graphql";
import {useFragment} from "react-relay";
import {useEffect, useState} from "react";
import {StatusType} from "../models/enums";

const CourseOfferingPresenterFragment = graphql`
    fragment CourseOfferingPresenter_offeringData on Course {
        courseOfferings {
            sections {
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
		<div className="">
			<div className="mt-8 flow-root">
				<div className="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
					<div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
						<div className="overflow-hidden shadow ring-1 ring-black ring-opacity-5 sm:rounded-lg">
							<table className="min-w-full divide-y divide-gray-300">
								<thead className="bg-gray-50">
								<tr>
									<th scope="col"
									    className="py-3.5 pl-4 text-left text-sm font-semibold text-gray-900 sm:pl-6">
										Component
									</th>
									<th scope="col"
									    className="py-3.5 text-left text-sm font-semibold text-gray-900">
										Section
									</th>
									<th scope="col"
									    className="py-3.5 text-left text-sm font-semibold text-gray-900">
										Class Number
									</th>
									<th scope="col"
									    className="pr-3 py-3.5 text-left text-sm font-semibold text-gray-900">
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
								<tbody className="divide-y divide-gray-200 bg-white">
								{selectedOffering?.sections?.map((section) => (
									<tr key={section?.number} className="hover:bg-gray-50">
										<td className="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-6">
											{section?.componentType}
										</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{section?.number}</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{section?.classNumber}</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
											{section?.professorNames?.map((name) => (
												<div key={name}>{name}</div>
											))}
										</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{section?.timingDetails?.map(
											(detail) => (
												<div key={detail?.location} className="py-2">
													<p className="font-semibold">{detail?.location}</p>
													<p>{detail?.time}</p>
													<p><span
														className={(detail?.daysOfWeekBitmap[0] === "1" ? "font-semibold" : "text-gray-500")+" pr-1"}>M</span>
														<span
															className={(detail?.daysOfWeekBitmap[1] === "1" ? "font-semibold" : "text-gray-500")+" pr-1"}>Tu</span>
														<span
															className={(detail?.daysOfWeekBitmap[2] === "1" ? "font-semibold" : "text-gray-500")+" pr-1"}>W</span>
														<span
															className={(detail?.daysOfWeekBitmap[3] === "1" ? "font-semibold" : "text-gray-500")+" pr-1"}>Th</span>
														<span
															className={(detail?.daysOfWeekBitmap[4] === "1" ? "font-semibold" : "text-gray-500")+" pr-1"}>F</span>
													</p>
												</div>
											)
										)}</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
											{section?.status === StatusType.Full ? "FULL" : "OPEN"}
										</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{section?.campus}</td>
										<td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{section?.delivery}</td>
									</tr>
								))}
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
		</div>
	)
}