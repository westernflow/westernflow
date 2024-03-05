import React, {createContext, useContext, useState, ReactNode, useEffect} from 'react';
import {getGoogleUserInfo, refreshTokens} from "../auth/helpers";
import {User} from "../models/user";


// Define the context type
interface UserContextType {
	user: User | null;
	login: (userData: User) => void;
	logout: () => void;
}

// Create a context with a default undefined value
const UserContext = createContext<UserContextType | undefined>(undefined);

// Create a provider component with typed children
export const UserProvider: React.FC<{ children: ReactNode }> = ({children}) => {

	useEffect(() => {
		const fetchUserData = async () => {
			const refreshToken = localStorage.getItem('refreshToken');
			if (!refreshToken) return;
			
			const newAccessToken = await refreshTokens();
			if (!newAccessToken) return;
			
			const userData = await getGoogleUserInfo(newAccessToken);
			if (!userData) return;
			login(userData);
		}

		fetchUserData();
	}, []);


	const [user, setUser] = useState<User | null>(null);

	const login = (userData: User) => {
		setUser(userData);
	};

	const logout = () => {
		setUser(null);
		localStorage.removeItem('accessToken');
		localStorage.removeItem('refreshToken');
	};

	return (
		<UserContext.Provider value={{user, login, logout}}>
			{children}
		</UserContext.Provider>
	);
};

// Hook to use the user context
export const useUser = (): UserContextType => {
	const context = useContext(UserContext);
	if (context === undefined) {
		throw new Error('useUser must be used within a UserProvider');
	}
	return context;
};
