/**
 * @generated SignedSource<<a2ec909b0ee7e20eebbec19864ad66c1>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
export type Campus = "ALL" | "BRESCIA" | "HURON" | "IVEY" | "KING" | "MAIN" | "UNKNOWN" | "%future added value";
export type ComponentType = "ALL" | "LAB" | "LECTURE" | "TUTORIAL" | "UNDEFINED" | "%future added value";
export type DeliveryType = "HYBRID" | "IN_PERSON" | "ONLINE" | "OTHER" | "UNDEFINED" | "%future added value";
export type StatusType = "FULL" | "NOT_FULL" | "UNDEFINED" | "%future added value";
export type CoursePresenterCourseQuery$variables = {
  code: number;
  facultyAbbreviation: string;
};
export type CoursePresenterCourseQuery$data = {
  readonly courseByCode: {
    readonly courseOfferings: ReadonlyArray<{
      readonly sections: ReadonlyArray<{
        readonly campus: Campus;
        readonly classNumber: number;
        readonly componentType: ComponentType;
        readonly delivery: DeliveryType;
        readonly number: number;
        readonly professorNames: ReadonlyArray<string>;
        readonly status: StatusType;
        readonly timingDetails: ReadonlyArray<{
          readonly daysOfWeekBitmap: string;
          readonly location: string;
          readonly time: string;
        } | null | undefined> | null | undefined;
        readonly waitListSize: number;
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
v2 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "number",
  "storageKey": null
},
v3 = [
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
      (v2/*: any*/),
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
                "kind": "ScalarField",
                "name": "componentType",
                "storageKey": null
              },
              (v2/*: any*/),
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "classNumber",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "professorNames",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "status",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "waitListSize",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "campus",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "delivery",
                "storageKey": null
              },
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
    "selections": (v3/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Operation",
    "name": "CoursePresenterCourseQuery",
    "selections": (v3/*: any*/)
  },
  "params": {
    "cacheID": "ad64811220fe8e1bc4a61be979d64298",
    "id": null,
    "metadata": {},
    "name": "CoursePresenterCourseQuery",
    "operationKind": "query",
    "text": "query CoursePresenterCourseQuery(\n  $code: Int!\n  $facultyAbbreviation: String!\n) {\n  courseByCode(code: $code, facultyAbbreviation: $facultyAbbreviation) {\n    id\n    name\n    number\n    description\n    faculty {\n      id\n      abbreviation\n    }\n    courseOfferings {\n      sections {\n        componentType\n        number\n        classNumber\n        professorNames\n        status\n        waitListSize\n        campus\n        delivery\n        timingDetails {\n          daysOfWeekBitmap\n          time\n          location\n        }\n      }\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "937bcb4d5a1c6d7a719a923a6a172a08";

export default node;
