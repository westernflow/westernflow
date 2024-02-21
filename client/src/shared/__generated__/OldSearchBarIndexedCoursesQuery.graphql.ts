/**
 * @generated SignedSource<<d026763ce49418cab71f959d3d7d233b>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type OldSearchBarIndexedCoursesQuery$variables = Record<PropertyKey, never>;
export type OldSearchBarIndexedCoursesQuery$data = {
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
export type OldSearchBarIndexedCoursesQuery = {
  response: OldSearchBarIndexedCoursesQuery$data;
  variables: OldSearchBarIndexedCoursesQuery$variables;
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
    "name": "OldSearchBarIndexedCoursesQuery",
    "selections": (v0/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "OldSearchBarIndexedCoursesQuery",
    "selections": (v0/*: any*/)
  },
  "params": {
    "cacheID": "265dc1a8478e4de53fb0ebc990a72594",
    "id": null,
    "metadata": {},
    "name": "OldSearchBarIndexedCoursesQuery",
    "operationKind": "query",
    "text": "query OldSearchBarIndexedCoursesQuery {\n  courses(first: 5000) {\n    nodes {\n      name\n      facultyId\n      faculty {\n        abbreviation\n      }\n      number\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "518feb0120fae9e5e89e54b34d8927f8";

export default node;
