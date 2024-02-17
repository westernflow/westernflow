/**
 * @generated SignedSource<<59eec50f9688e405de7064a21eb64ead>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type SearchBarIndexedCoursesQuery$variables = Record<PropertyKey, never>;
export type SearchBarIndexedCoursesQuery$data = {
  readonly courses: {
    readonly nodes: ReadonlyArray<{
      readonly faculty: {
        readonly abbreviation: string;
      } | null | undefined;
      readonly facultyId: number;
      readonly name: string;
      readonly number: number;
    }> | null | undefined;
  } | null | undefined;
};
export type SearchBarIndexedCoursesQuery = {
  response: SearchBarIndexedCoursesQuery$data;
  variables: SearchBarIndexedCoursesQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = [
  {
    "alias": null,
    "args": [
      {
        "kind": "Literal",
        "name": "first",
        "value": 5000
      }
    ],
    "concreteType": "CoursesConnection",
    "kind": "LinkedField",
    "name": "courses",
    "plural": false,
    "selections": [
      {
        "alias": null,
        "args": null,
        "concreteType": "Course",
        "kind": "LinkedField",
        "name": "nodes",
        "plural": true,
        "selections": [
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "name",
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "facultyId",
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "concreteType": "Faculty",
            "kind": "LinkedField",
            "name": "faculty",
            "plural": false,
            "selections": [
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "abbreviation",
                "storageKey": null
              }
            ],
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "number",
            "storageKey": null
          }
        ],
        "storageKey": null
      }
    ],
    "storageKey": "courses(first:5000)"
  }
];
return {
  "fragment": {
    "argumentDefinitions": [],
    "kind": "Fragment",
    "metadata": null,
    "name": "SearchBarIndexedCoursesQuery",
    "selections": (v0/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "SearchBarIndexedCoursesQuery",
    "selections": (v0/*: any*/)
  },
  "params": {
    "cacheID": "5ee17649ac414a05e26beaa2c13e90aa",
    "id": null,
    "metadata": {},
    "name": "SearchBarIndexedCoursesQuery",
    "operationKind": "query",
    "text": "query SearchBarIndexedCoursesQuery {\n  courses(first: 5000) {\n    nodes {\n      name\n      facultyId\n      faculty {\n        abbreviation\n      }\n      number\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "10c2c25ff2a3996374fe8f8cdaee06f7";

export default node;
