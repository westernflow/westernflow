import {User} from "../models/user";

interface Tokens {
	accessToken: string;
	refreshToken: string;
}

export const getAccessAndRefreshToken = async (authCode: string): Promise<Tokens> => {
	const response = await fetch("http://localhost:5095/api/auth/exchange", {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify({
			code: authCode,
			redirectUri: "http://localhost:3000",
		}),
	});
	const data = await response.json();
	localStorage.setItem('accessToken', data.accessToken);
	localStorage.setItem('refreshToken', data.refreshToken);
	
	return data;
}

export const getGoogleUserInfo = async (accessToken: string): Promise<User | undefined> => {
	const userDataResponse = await fetch("https://www.googleapis.com/oauth2/v3/userinfo", {
		headers: {
			Authorization: `Bearer ${accessToken}`,
		},
	});

	return userDataResponse.ok ? await userDataResponse.json() : undefined;
}

export const refreshTokens = async () => {
	const refreshToken = localStorage.getItem('refreshToken');
	if (!refreshToken) return null;

	const response = await fetch("http://localhost:5095/api/auth/refresh", {
		method: 'POST',
		headers: {'Content-Type': 'application/json'},
		body: JSON.stringify({refreshToken}),
	});

	if (!response.ok) {
		localStorage.removeItem('accessToken');
		localStorage.removeItem('refreshToken');
		return null;
	}

	const tokens = await response.json();
	localStorage.setItem('accessToken', tokens.accessToken);
	return tokens.accessToken;
};
