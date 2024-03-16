// file to test random queries on render
import graphql from "babel-plugin-relay/macro";
import {useLazyLoadQuery} from "react-relay";
import type {QueryOnRenderQuery as QueryOnRenderQueryType} from "./__generated__/QueryOnRenderQuery.graphql";

const QueryOnRenderQuery = graphql`
	query QueryOnRenderQuery {
		jwt
	}
`;

export default function QueryOnRender() {
	const query = useLazyLoadQuery<QueryOnRenderQueryType>(QueryOnRenderQuery, {});
	
	console.log(query)
	
	return (
		<div>

		</div>
	)
}