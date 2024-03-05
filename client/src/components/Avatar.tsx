import {Button, Image, Menu, MenuButton, MenuItem, MenuList} from "@chakra-ui/react";
import {useUser} from "../contexts/UserContext";

export default function Avatar() {
	const { user, logout} = useUser();
	if (!user) return null;
	
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
				<MenuItem onClick={()=>logout()}>Sign Out</MenuItem>
			</MenuList>
		</Menu>
	);
}