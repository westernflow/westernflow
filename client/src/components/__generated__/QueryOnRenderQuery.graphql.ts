/**
 * @generated SignedSource<<84dd726ca09d73c0a411ff743328720d>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type QueryOnRenderQuery$variables = Record<PropertyKey, never>;
export type QueryOnRenderQuery$data = {
  readonly jwt: string;
};
export type QueryOnRenderQuery = {
  response: QueryOnRenderQuery$data;
  variables: QueryOnRenderQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = [
  {
    "alias": null,
    "args": null,
    "kind": "ScalarField",
    "name": "jwt",
    "storageKey": null
  }
];
return {
  "fragment": {
    "argumentDefinitions": [],
    "kind": "Fragment",
    "metadata": null,
    "name": "QueryOnRenderQuery",
    "selections": (v0/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "QueryOnRenderQuery",
    "selections": (v0/*: any*/)
  },
  "params": {
    "cacheID": "1ee0efebc60355c38f4364cd3f8eaa29",
    "id": null,
    "metadata": {},
    "name": "QueryOnRenderQuery",
    "operationKind": "query",
    "text": "query QueryOnRenderQuery {\n  jwt\n}\n"
  }
};
})();

(node as any).hash = "2dfd1f99df084d5f1d1b1cd80875c8b7";

export default node;
