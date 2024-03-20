const baseURL = process.env.REACT_APP_BASE_URL;

function fetchWithBaseURL(endpoint: string, options: RequestInit) {
	return fetch(`${baseURL}${endpoint}`, options);
}

export default fetchWithBaseURL;
