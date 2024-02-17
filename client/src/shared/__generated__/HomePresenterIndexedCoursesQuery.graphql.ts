/**
 * @generated SignedSource<<00aa0048c90647bb603dbe5d1bff83a4>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type HomePresenterIndexedCoursesQuery$variables = Record<PropertyKey, never>;
export type HomePresenterIndexedCoursesQuery$data = {
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
export type HomePresenterIndexedCoursesQuery = {
  response: HomePresenterIndexedCoursesQuery$data;
  variables: HomePresenterIndexedCoursesQuery$variables;
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
    "name": "HomePresenterIndexedCoursesQuery",
    "selections": (v0/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "HomePresenterIndexedCoursesQuery",
    "selections": (v0/*: any*/)
  },
  "params": {
    "cacheID": "32e8c010f1e7f183f3968e38cef3b643",
    "id": null,
    "metadata": {},
    "name": "HomePresenterIndexedCoursesQuery",
    "operationKind": "query",
    "text": "query HomePresenterIndexedCoursesQuery {\n  courses(first: 5000) {\n    nodes {\n      name\n      facultyId\n      faculty {\n        abbreviation\n      }\n      number\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "5e2cd1b7ca2b28ff6510623a6980e7c7";

export default node;
