import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./index.css";
import {ChakraProvider} from "@chakra-ui/react";
import {Auth0ProviderWithVariables} from "./providers/Auth0ProviderWithNavigate";

const root = ReactDOM.createRoot(
	document.getElementById("root") as HTMLElement
);

root.render(
	<React.StrictMode>
		<ChakraProvider>
			<Auth0ProviderWithVariables>
				<App/>
			</Auth0ProviderWithVariables>
		</ChakraProvider>
	</React.StrictMode>
);
