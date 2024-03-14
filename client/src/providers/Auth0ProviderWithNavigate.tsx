import React from "react";
import {Auth0Provider} from "@auth0/auth0-react";

interface Auth0ProviderProps {
	children: React.ReactNode;
}

export const Auth0ProviderWithVariables = ({children}: Auth0ProviderProps) => {
	const domain = process.env.REACT_APP_AUTH0_DOMAIN;
	const clientId = process.env.REACT_APP_AUTH0_CLIENT_ID;
	const audience = process.env.REACT_APP_AUTH0_AUDIENCE;
	
	if (!(domain && clientId && audience)) {
		return <div>Missing Auth0 environment variables</div>;
	}

	return (
		<Auth0Provider
			domain={domain}
			clientId={clientId}
			authorizationParams={{
				audience: audience,
				redirect_uri: window.location.origin,
				scope: 'openid profile email read:current_user update:current_user_metadata'
			}}
		>
			{children}
		</Auth0Provider>
	);
};