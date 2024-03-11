import {RelayEnvironmentProvider} from "react-relay";
import {RelayEnvironment} from "./RelayEnvironment";
import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./index.css";
import {ChakraProvider} from "@chakra-ui/react";
import {Auth0Provider} from "@auth0/auth0-react";

const root = ReactDOM.createRoot(
	document.getElementById("root") as HTMLElement
);

const domain = "dev-p4dlhz8fqi356ze6.us.auth0.com";
root.render(
	<RelayEnvironmentProvider environment={RelayEnvironment}>
		<React.StrictMode>
			<ChakraProvider>
				<Auth0Provider domain={domain}
				               clientId={"V3iA5MlVIx9i7LTJWkxJad0TvOruJV6b"}
				               authorizationParams={{
					               redirect_uri: window.location.origin,
					               audience: `https://${domain}/api/v2/`,
					               scope: 'openid profile email',
				               }}>
					<App/>
				</Auth0Provider>
			</ChakraProvider>
		</React.StrictMode>
	</RelayEnvironmentProvider>
);
