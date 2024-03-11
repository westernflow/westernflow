import SearchBar from "../components/SearchBar"
import LoginCard from "../components/LoginCard"
import ContentContainer from "../components/ContentContainer";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import {Spacer, VStack} from "@chakra-ui/react";
import {Suspense, useEffect} from "react";
import {useAuth0} from "@auth0/auth0-react";

const LoginButton = () => {
	const { loginWithRedirect } = useAuth0();

	return <button onClick={() => loginWithRedirect()}>Log In</button>;
};

const LogoutButton = () => {
	const { logout } = useAuth0();

	return (
		<button onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}>
			Log Out
		</button>
	);
};

export default function Home() {
	const { isAuthenticated, loginWithRedirect, getAccessTokenSilently } = useAuth0();
	const getAccessToken = async () => {
		if (!isAuthenticated) {
			// This will redirect the user to the login page
			loginWithRedirect();
		} else {
			try {
				const accessToken = await getAccessTokenSilently();
				console.log(accessToken);
			} catch (error) {
				// If silent acquisition fails, you might need to handle it differently
				// For example, you could fall back to an interactive method
				console.error(error);
			}
		}
	};
	
	useEffect(() => {
		getAccessToken();
	}, [isAuthenticated]);
	
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
						<div className="xl:w-1/2">
							<Suspense fallback={<div>loading</div>}>
								<LoginButton/>
								<LogoutButton/>
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