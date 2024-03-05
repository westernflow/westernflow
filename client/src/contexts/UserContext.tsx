import React, { createContext, useContext, useState, ReactNode } from 'react';

// Define the type for your user data
interface User {
	id: string;
	name: string;
	email: string;
	// Add more fields as necessary
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
export const UserProvider: React.FC<{children: ReactNode}> = ({ children }) => {
	const [user, setUser] = useState<User | null>(null);

	const login = (userData: User) => {
		setUser(userData);
	};

	const logout = () => {
		setUser(null);
		localStorage.removeItem('token'); // Clear token on logout
	};

	return (
		<UserContext.Provider value={{ user, login, logout }}>
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
