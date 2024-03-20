import {
  Environment,
  Network,
  RecordSource,
  Store,
} from "relay-runtime";
import fetchWrapper from "./api/fetchWrapper";

interface TokenProvider {
    getToken: () => Promise<string>;
}

const createFetchFn = (tokenProvider: TokenProvider) => {
    return async (request: any, variables: any) => {
        const resp = await fetchWrapper('graphql', {
        method: "POST",
        mode: "cors",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${await tokenProvider.getToken()}`,
        },
        body: JSON.stringify({
            query: request.text, // <-- The GraphQL document composed by Relay
            variables,
        }),
        });
    
        return await resp.json();
    };
}

export function createRelayEnvironment(tokenProvider: TokenProvider) {
  return new Environment({
    network: Network.create(createFetchFn(tokenProvider)),
    store: new Store(new RecordSource()),
  });
}
