import {
  Environment,
  Network,
  RecordSource,
  Store,
} from "relay-runtime";
import fetchWrapper from "./api/fetchWrapper";

const CACHE_KEY_PREFIX = 'relayCache_';

const generateCacheKey = (query: any, variables:any) => {
	const queryID = btoa(query.text);
	const variablesID = btoa(JSON.stringify(variables));
	return `${CACHE_KEY_PREFIX}${queryID}_${variablesID}`;
};

const readFromCache = (key:any) => {
	const { data, timestamp } = JSON.parse(localStorage.getItem(key) || '{}');
	const now = new Date().getTime();
	const oneHour = 60 * 60 * 1000;
	
	if (timestamp && now - timestamp < oneHour) {
		return JSON.parse(data);
	}
};

const writeToCache = (key:any, data:any) => {
	const item = {
		data: JSON.stringify(data),
		timestamp: new Date().getTime(),
	};
	localStorage.setItem(key, JSON.stringify(item));
};


interface TokenProvider {
    getToken: () => Promise<string>;
}

const createFetchFn = (tokenProvider: TokenProvider) => {
	return async (request:any, variables:any) => {
		if (request.name === "SearchBarIndexedCoursesQuery") {
			const cacheKey = generateCacheKey(request, variables);
			const cachedData = readFromCache(cacheKey);

			if (cachedData) {
				return cachedData;
			}
		}

		const resp = await fetchWrapper('graphql', {
			method: "POST",
			mode: "cors",
			headers: {
				"Content-Type": "application/json",
				"Authorization": `Bearer ${await tokenProvider.getToken()}`,
			},
			body: JSON.stringify({
				query: request.text,
				variables,
			}),
		});

		const jsonData = await resp.json();
		
		if (request.name === "SearchBarIndexedCoursesQuery") {
			const cacheKey = generateCacheKey(request, variables);
			writeToCache(cacheKey, jsonData);
		}

		return jsonData;
	};
};


export function createRelayEnvironment(tokenProvider: TokenProvider) {
  return new Environment({
    network: Network.create(createFetchFn(tokenProvider)),
    store: new Store(new RecordSource()),
  });
}
