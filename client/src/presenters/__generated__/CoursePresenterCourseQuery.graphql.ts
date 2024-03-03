/**
 * @generated SignedSource<<e1a460a602a2681182a3e59134be35ea>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type CoursePresenterCourseQuery$variables = {
  code: number;
  facultyAbbreviation: string;
};
export type CoursePresenterCourseQuery$data = {
  readonly courseByCode: {
    readonly courseOfferings: ReadonlyArray<{
      readonly sections: ReadonlyArray<{
        readonly timingDetails: ReadonlyArray<{
          readonly daysOfWeekBitmap: string;
          readonly location: string;
          readonly time: string;
        } | null | undefined> | null | undefined;
      } | null | undefined> | null | undefined;
    } | null | undefined> | null | undefined;
    readonly description: string | null | undefined;
    readonly faculty: {
      readonly abbreviation: string;
      readonly id: string;
    } | null | undefined;
    readonly id: string;
    readonly name: string;
    readonly number: number;
  };
};
export type CoursePresenterCourseQuery = {
  response: CoursePresenterCourseQuery$data;
  variables: CoursePresenterCourseQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = [
  {
    "defaultValue": null,
    "kind": "LocalArgument",
    "name": "code"
  },
  {
    "defaultValue": null,
    "kind": "LocalArgument",
    "name": "facultyAbbreviation"
  }
],
v1 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "id",
  "storageKey": null
},
v2 = [
  {
    "alias": null,
    "args": [
      {
        "kind": "Variable",
        "name": "code",
        "variableName": "code"
      },
      {
        "kind": "Variable",
        "name": "facultyAbbreviation",
        "variableName": "facultyAbbreviation"
      }
    ],
    "concreteType": "Course",
    "kind": "LinkedField",
    "name": "courseByCode",
    "plural": false,
    "selections": [
      (v1/*: any*/),
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
        "name": "number",
        "storageKey": null
      },
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "description",
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
          (v1/*: any*/),
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
        "concreteType": "CourseOffering",
        "kind": "LinkedField",
        "name": "courseOfferings",
        "plural": true,
        "selections": [
          {
            "alias": null,
            "args": null,
            "concreteType": "Section",
            "kind": "LinkedField",
            "name": "sections",
            "plural": true,
            "selections": [
              {
                "alias": null,
                "args": null,
                "concreteType": "TimingDetails",
                "kind": "LinkedField",
                "name": "timingDetails",
                "plural": true,
                "selections": [
                  {
                    "alias": null,
                    "args": null,
                    "kind": "ScalarField",
                    "name": "daysOfWeekBitmap",
                    "storageKey": null
                  },
                  {
                    "alias": null,
                    "args": null,
                    "kind": "ScalarField",
                    "name": "time",
                    "storageKey": null
                  },
                  {
                    "alias": null,
                    "args": null,
                    "kind": "ScalarField",
                    "name": "location",
                    "storageKey": null
                  }
                ],
                "storageKey": null
              }
            ],
            "storageKey": null
          }
        ],
        "storageKey": null
      }
    ],
    "storageKey": null
  }
];
return {
  "fragment": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Fragment",
    "metadata": null,
    "name": "CoursePresenterCourseQuery",
    "selections": (v2/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Operation",
    "name": "CoursePresenterCourseQuery",
    "selections": (v2/*: any*/)
  },
  "params": {
    "cacheID": "3ab40d835fe53b9a540faa1e678e2b5e",
    "id": null,
    "metadata": {},
    "name": "CoursePresenterCourseQuery",
    "operationKind": "query",
    "text": "query CoursePresenterCourseQuery(\n  $code: Int!\n  $facultyAbbreviation: String!\n) {\n  courseByCode(code: $code, facultyAbbreviation: $facultyAbbreviation) {\n    id\n    name\n    number\n    description\n    faculty {\n      id\n      abbreviation\n    }\n    courseOfferings {\n      sections {\n        timingDetails {\n          daysOfWeekBitmap\n          time\n          location\n        }\n      }\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "99b323e232d947a333a70e37f4a30f90";

export default node;
