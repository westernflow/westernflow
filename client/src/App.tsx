import { useAuth0 } from "@auth0/auth0-react";
import React, {Component, ErrorInfo, ReactNode, Suspense, useEffect, useState} from "react";
import { RelayEnvironmentProvider } from "react-relay";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import RelayModernEnvironment from "relay-runtime/lib/store/RelayModernEnvironment";
import { createRelayEnvironment } from "./RelayEnvironment";
import { CoursePresenter } from "./presenters/CoursePresenter";
import Home from "./presenters/HomePresenter";
import SuspenseScreen from "./components/Spinner";
import ErrorPage from "./components/ErrorPage";

interface Props {
    children?: ReactNode;
    fallback?: ReactNode;
}

interface State {
    hasError: boolean;
}

class ErrorBoundary extends Component<Props, State> {
    public state: State = {
        hasError: false
    };

    public static getDerivedStateFromError(_: Error): State {
        // Update state so the next render will show the fallback UI.
        return { hasError: true };
    }

    public componentDidCatch(error: Error, errorInfo: ErrorInfo) {
        console.error("Uncaught error:", error, errorInfo);
    }

    public render() {
        if (this.state.hasError) {
            return this.props.fallback || <ErrorPage />;
        }

        return this.props.children;
    }
}


const SuspendedCoursePresenter = () => {
    return (
        <Suspense fallback={<SuspenseScreen />}>
            <CoursePresenter />
        </Suspense>
    );
};

function App() {
    const { getAccessTokenSilently } = useAuth0();
    const [relayEnvironment, setRelayEnvironment] =
        useState<RelayModernEnvironment>();

    useEffect(() => {
        console.log("Relay environment has changed..");
        const tokenProvider = {
            getToken: () => getAccessTokenSilently().catch(() => ""),
        };

        setRelayEnvironment(createRelayEnvironment(tokenProvider));
    }, [getAccessTokenSilently]);

    if (!relayEnvironment) {
        return <SuspenseScreen />;
    }

    return (
        <RelayEnvironmentProvider environment={relayEnvironment}>
            <div className="roboto">
                <ErrorBoundary>
                    <BrowserRouter>
                        <Routes>
                            <Route path="/" Component={Home} />
                            <Route
                                path="/course/:courseCode"
                                Component={SuspendedCoursePresenter}
                            />
                            <Route path="*" Component={ErrorPage} />
                        </Routes>
                    </BrowserRouter>
                </ErrorBoundary>
            </div>
        </RelayEnvironmentProvider>
    );
}

export default App;
