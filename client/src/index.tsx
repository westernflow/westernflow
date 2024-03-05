import {RelayEnvironmentProvider} from "react-relay";
import {RelayEnvironment} from "./RelayEnvironment";
import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./index.css";
import {ChakraProvider} from "@chakra-ui/react";

const root = ReactDOM.createRoot(
	document.getElementById("root") as HTMLElement
);
root.render(
	<RelayEnvironmentProvider environment={RelayEnvironment}>
		<React.StrictMode>
			<ChakraProvider>
				<App/>
			</ChakraProvider>
		</React.StrictMode>
	</RelayEnvironmentProvider>
);
