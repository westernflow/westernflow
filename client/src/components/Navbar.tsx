import SearchBar from "./SearchBar";
import {useAuth0} from "@auth0/auth0-react";
import Avatar from "./Avatar";

interface NavbarProps {
	showSearchBar?: boolean;
}

export default function Navbar(props: NavbarProps) {
	const {loginWithPopup, isLoading, isAuthenticated} = useAuth0();
	const showSearchBar = props.showSearchBar ?? true

	return (
		<header className="bg-white min-w-full">
			<nav className="mx-auto flex items-center p-6" aria-label="Global">
				<div className="flex flex-1">
					<a href="/" className="-m-1.5 p-1.5">
						<span className="sr-only">Westernflow</span>
						<img className="h-8 w-auto"
						     src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600" alt=""/>
					</a>
				</div>
				<div className={`w-3/4 ${showSearchBar ? "" : "hidden"}`}>
					<SearchBar/>
				</div>
				<div className="flex flex-1 items-center justify-end gap-x-6">
					{
						!isLoading ?
							(!isAuthenticated ? <button
									className="rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
									onClick={() => loginWithPopup()}>
									Sign in
								</button>
								:
								<Avatar/>)
							: null
					}
				</div>
			</nav>
		</header>
	)
}