import { Spinner } from "@chakra-ui/react";

export default function SuspenseScreen() {
	return (
		<div className="bg-wf w-full h-screen flex justify-center items-center">
			<Spinner />
		</div>
	);
}
