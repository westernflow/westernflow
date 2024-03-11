import React from "react";
import {Auth0Provider} from "@auth0/auth0-react";

interface Auth0ProviderWithNavigateProps {
	children: React.ReactNode;
}

export const Auth0ProviderWithNavigate = ({children}: Auth0ProviderWithNavigateProps) => {
	const domain = process.env.REACT_APP_AUTH0_DOMAIN;
	const clientId = process.env.REACT_APP_AUTH0_CLIENT_ID;
	const redirectUri = process.env.REACT_APP_AUTH0_CALLBACK_URL;
	const audience = process.env.REACT_APP_AUTH0_AUDIENCE;
	
	
	console.log(domain, clientId, redirectUri, audience);

	if (!(domain && clientId && redirectUri && audience)) {
		return null;
	}

	return (
		<Auth0Provider
			domain={domain}
			clientId={clientId}
			authorizationParams={{
				audience: audience,
				redirect_uri: redirectUri,
			}}
		>
			{children}
		</Auth0Provider>
	);
};