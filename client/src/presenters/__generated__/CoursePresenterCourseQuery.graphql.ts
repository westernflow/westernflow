/**
 * @generated SignedSource<<283796d45ae31eeff036da5b406b19a9>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type CoursePresenterCourseQuery$variables = Record<PropertyKey, never>;
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
    readonly faculty: {
      readonly abbreviation: string;
    } | null | undefined;
    readonly facultyId: number;
    readonly id: number;
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
    "alias": null,
    "args": [
      {
        "kind": "Literal",
        "name": "code",
        "value": 2553
      },
      {
        "kind": "Literal",
        "name": "facultyName",
        "value": "ACTURSCI"
      }
    ],
    "concreteType": "Course",
    "kind": "LinkedField",
    "name": "courseByCode",
    "plural": false,
    "selections": [
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "id",
        "storageKey": null
      },
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
    "storageKey": "courseByCode(code:2553,facultyName:\"ACTURSCI\")"
  }
];
return {
  "fragment": {
    "argumentDefinitions": [],
    "kind": "Fragment",
    "metadata": null,
    "name": "CoursePresenterCourseQuery",
    "selections": (v0/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [],
    "kind": "Operation",
    "name": "CoursePresenterCourseQuery",
    "selections": (v0/*: any*/)
  },
  "params": {
    "cacheID": "d980ba37181c536727539ed9c7fef414",
    "id": null,
    "metadata": {},
    "name": "CoursePresenterCourseQuery",
    "operationKind": "query",
    "text": "query CoursePresenterCourseQuery {\n  courseByCode(code: 2553, facultyName: \"ACTURSCI\") {\n    id\n    name\n    facultyId\n    faculty {\n      abbreviation\n    }\n    number\n    courseOfferings {\n      sections {\n        timingDetails {\n          daysOfWeekBitmap\n          time\n          location\n        }\n      }\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "ed55e8a1434aadfc63bbe8e1a90dd9c1";

export default node;
