/**
 * @generated SignedSource<<99aafa8bcb5e4787df5b28794bc03584>>
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
    "name": "OldSearchBarIndexedCoursesQuery",
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
    "name": "OldSearchBarIndexedCoursesQuery",
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
    "cacheID": "92b949575595622fb90f5ba9a1055f2a",
    "id": null,
    "metadata": {},
    "name": "OldSearchBarIndexedCoursesQuery",
    "operationKind": "query",
    "text": "query OldSearchBarIndexedCoursesQuery {\n  courses(first: 5000) {\n    nodes {\n      name\n      facultyId\n      faculty {\n        abbreviation\n        id\n      }\n      number\n      id\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "518feb0120fae9e5e89e54b34d8927f8";

export default node;
