import SearchBar from "../components/SearchBar"
import ContentContainer from "../components/ContentContainer";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import {Spacer, VStack} from "@chakra-ui/react";
import {Suspense, useEffect} from "react";
import {useAuth0} from "@auth0/auth0-react";

export default function Home() {
	return (
		<div className="bg-slate-50">
			<ContentContainer additionalClasses="border bg-white">
				<VStack className="min-h-screen">
					<Navbar showSearchBar={false}/>
					<div className="flex justify-center flex-col xl:flex-row">
						<div className="flex flex-col">
							<HeaderSection/>
							<Suspense fallback={<div>loading</div>}>
								<SearchBar />
							</Suspense>
						</div>
					</div>
					<Spacer />
					<Footer/>
				</VStack>
			</ContentContainer>
		</div>
	);
}

export function HeaderSection() {
	const { getAccessTokenSilently } = useAuth0();
	
	useEffect(() => {
		getAccessTokenSilently().then(token => {
			console.log("token", token)
		})
	}, [getAccessTokenSilently])
	
	return (
		<div className="bg-white py-4 sm:py-10">
			<div className="mx-auto px-3">
				<div className="mx-auto">
					<a className="text-base font-semibold leading-7 text-indigo-600" href="https://uwflow.com">Inspired
						by UW Flow</a>
					<h2 className="mt-2 text-4xl font-bold tracking-tight text-gray-900 sm:text-6xl">Westernflow </h2>
					<p className="mt-6 text-lg leading-8 text-gray-600 w-3/4">
						A platform to centralize timetable information, course reviews, professor information, and more
						for University of Western Ontario students.
					</p>
				</div>
			</div>
		</div>
	)
}