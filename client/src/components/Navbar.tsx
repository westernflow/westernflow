import SearchBar from "./SearchBar";
import {useUser} from "../contexts/UserContext"
import Avatar from "./Avatar";

export default function Navbar() {
	const {user} = useUser()

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
				<div className="w-3/4">
					<SearchBar/>
				</div>
				<div className="flex flex-1 items-center justify-end gap-x-6">
					{
						!user ?
							<a
								href="#"
								className="rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
							>
								Sign in
							</a>
							:
							<Avatar />
					}
				</div>
			</nav>
		</header>
	)
}