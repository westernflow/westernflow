import React, {createContext, useContext, useState, ReactNode, useEffect} from 'react';

// Define the type for your user data
interface User {
	id: string;
	name: string;
	email: string;
}

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
	const fetchUserData = async (accessToken: string): Promise<User | undefined> => {
		const userDataResponse = await fetch("https://www.googleapis.com/oauth2/v3/userinfo", {
			headers: {
				Authorization: `Bearer ${accessToken}`,
			},
		});
		if (!userDataResponse.ok) {
			return undefined;
		}
		const userData = await userDataResponse.json();
		
		return userData;
	}
	
	const getRefreshedTokens = async (refreshToken: string): Promise<{accessToken: string, refreshToken: string} | undefined> => {
		const refreshedTokensResponse = await fetch("http://localhost:5095/api/google/refresh", {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
			},
			body: JSON.stringify({
				refreshToken
			})
		});
		if (!refreshedTokensResponse.ok) {
			return undefined;
		}
		const refreshedTokens = await refreshedTokensResponse.json();
		
		return refreshedTokens;
	}
	
	

	useEffect(() => {
		const fetchUserData = async () => {
			const accessToken = localStorage.getItem('accessToken');
			if (!accessToken) return;

			const userDataResponse = await fetch("https://www.googleapis.com/oauth2/v3/userinfo", {
				headers: {
					Authorization: `Bearer ${accessToken}`,
				},
			});

			if (!userDataResponse.ok) {
				// query /api/google/refresh to refresh the token
				localStorage.getItem('refreshToken')

				const refreshedTokensResponse = await fetch("http://localhost:5095/api/google/refresh", {
					method: 'POST',
					headers: {
						'Content-Type': 'application/json',
					},
					body: JSON.stringify({
						refreshToken: localStorage.getItem('refreshToken')
					})
				});

				if (!refreshedTokensResponse.ok) {
					localStorage.removeItem('accessToken');
					localStorage.removeItem('refreshToken');
					return;
				}

				const refreshedTokens = await refreshedTokensResponse.json();

				localStorage.setItem('accessToken', refreshedTokens.accessToken);
				localStorage.setItem('refreshToken', refreshedTokens.refreshToken);

				const userDataResponse = await fetch("https://www.googleapis.com/oauth2/v3/userinfo", {
					headers: {
						Authorization: `Bearer ${accessToken}`,
					},
				});

				if (!userDataResponse.ok) {
					localStorage.removeItem('accessToken');
					localStorage.removeItem('refreshToken');
					return;
				}

				const userData = await userDataResponse.json();
				login(userData);
			} else {
				const userData = await userDataResponse.json();
				login(userData);
			}
		};

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
