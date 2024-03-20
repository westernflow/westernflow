/**
 * @generated SignedSource<<df23c41cbdc96dcd36fa2b88a1b978fa>>
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
    "kind": "Literal",
    "name": "first",
    "value": 5000
  }
],
v1 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "name",
  "storageKey": null
},
v2 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "facultyId",
  "storageKey": null
},
v3 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "abbreviation",
  "storageKey": null
},
v4 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "number",
  "storageKey": null
},
v5 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "id",
  "storageKey": null
};
return {
  "fragment": {
    "argumentDefinitions": [],
    "kind": "Fragment",
    "metadata": null,
    "name": "SearchBarIndexedCoursesQuery",
    "selections": [
      {
        "alias": null,
        "args": (v0/*: any*/),
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
              (v1/*: any*/),
              (v2/*: any*/),
              {
                "alias": null,
                "args": null,
                "concreteType": "Faculty",
                "kind": "LinkedField",
                "name": "faculty",
                "plural": false,
                "selections": [
                  (v3/*: any*/)
                ],
                "storageKey": null
              },
              (v4/*: any*/)
            ],
            "storageKey": null
          }
        ],
        "storageKey": "courses(first:5000)"
      }
    ],
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "SearchBarIndexedCoursesQuery",
    "selections": [
      {
        "alias": null,
        "args": (v0/*: any*/),
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
              (v1/*: any*/),
              (v2/*: any*/),
              {
                "alias": null,
                "args": null,
                "concreteType": "Faculty",
                "kind": "LinkedField",
                "name": "faculty",
                "plural": false,
                "selections": [
                  (v3/*: any*/),
                  (v5/*: any*/)
                ],
                "storageKey": null
              },
              (v4/*: any*/),
              (v5/*: any*/)
            ],
            "storageKey": null
          }
        ],
        "storageKey": "courses(first:5000)"
      }
    ]
  },
  "params": {
    "cacheID": "4d9994a788c30cb21df595f3549da59f",
    "id": null,
    "metadata": {},
    "name": "SearchBarIndexedCoursesQuery",
    "operationKind": "query",
    "text": "query SearchBarIndexedCoursesQuery {\n  courses(first: 5000) {\n    nodes {\n      name\n      facultyId\n      faculty {\n        abbreviation\n        id\n      }\n      number\n      id\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "10c2c25ff2a3996374fe8f8cdaee06f7";

export default node;
