import {Button, Image, Menu, MenuButton, MenuItem, MenuList} from "@chakra-ui/react";
import {useAuth0} from "@auth0/auth0-react";

export default function Avatar() {
	const {user, isAuthenticated, isLoading, logout} = useAuth0();

	if (isLoading || !isAuthenticated || !user) {
		return <div>Loading...</div>;
	}

	return (
	
			<Menu>
				<MenuButton as={Button} rounded={'full'} variant={'link'} cursor={'pointer'} minW={0}>
					<Image
						className="h-10 w-10 rounded-full"
						src={user.picture}
					/>
				</MenuButton>
				<MenuList>
					<MenuItem>Profile</MenuItem>
					<MenuItem onClick={() => logout()}>Sign Out</MenuItem>
				</MenuList>
			</Menu>
	);
}