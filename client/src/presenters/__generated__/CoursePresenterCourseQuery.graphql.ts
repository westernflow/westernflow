/**
 * @generated SignedSource<<d611b8f2cc464e4a8eb7e713bcd6f202>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { ConcreteRequest, Query } from 'relay-runtime';
import { FragmentRefs } from "relay-runtime";
export type CoursePresenterCourseQuery$variables = {
  code: number;
  facultyAbbreviation: string;
};
export type CoursePresenterCourseQuery$data = {
  readonly courseByCode: {
    readonly description: string | null | undefined;
    readonly faculty: {
      readonly abbreviation: string;
      readonly id: string;
    } | null | undefined;
    readonly id: string;
    readonly name: string;
    readonly number: number;
    readonly " $fragmentSpreads": FragmentRefs<"CourseOfferingPresenter_offeringData">;
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
v1 = [
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
v2 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "id",
  "storageKey": null
},
v3 = {
  "alias": null,
  "args": null,
  "kind": "ScalarField",
  "name": "name",
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
  "name": "description",
  "storageKey": null
},
v6 = {
  "alias": null,
  "args": null,
  "concreteType": "Faculty",
  "kind": "LinkedField",
  "name": "faculty",
  "plural": false,
  "selections": [
    (v2/*: any*/),
    {
      "alias": null,
      "args": null,
      "kind": "ScalarField",
      "name": "abbreviation",
      "storageKey": null
    }
  ],
  "storageKey": null
};
return {
  "fragment": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Fragment",
    "metadata": null,
    "name": "CoursePresenterCourseQuery",
    "selections": [
      {
        "alias": null,
        "args": (v1/*: any*/),
        "concreteType": "Course",
        "kind": "LinkedField",
        "name": "courseByCode",
        "plural": false,
        "selections": [
          (v2/*: any*/),
          (v3/*: any*/),
          (v4/*: any*/),
          (v5/*: any*/),
          (v6/*: any*/),
          {
            "args": null,
            "kind": "FragmentSpread",
            "name": "CourseOfferingPresenter_offeringData"
          }
        ],
        "storageKey": null
      }
    ],
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": (v0/*: any*/),
    "kind": "Operation",
    "name": "CoursePresenterCourseQuery",
    "selections": [
      {
        "alias": null,
        "args": (v1/*: any*/),
        "concreteType": "Course",
        "kind": "LinkedField",
        "name": "courseByCode",
        "plural": false,
        "selections": [
          (v2/*: any*/),
          (v3/*: any*/),
          (v4/*: any*/),
          (v5/*: any*/),
          (v6/*: any*/),
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
                  (v2/*: any*/),
                  {
                    "alias": null,
                    "args": null,
                    "kind": "ScalarField",
                    "name": "componentType",
                    "storageKey": null
                  },
                  (v4/*: any*/),
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
    ]
  },
  "params": {
    "cacheID": "8484cc764ccfecf1140d4da45d3e2f0c",
    "id": null,
    "metadata": {},
    "name": "CoursePresenterCourseQuery",
    "operationKind": "query",
    "text": "query CoursePresenterCourseQuery(\n  $code: Int!\n  $facultyAbbreviation: String!\n) {\n  courseByCode(code: $code, facultyAbbreviation: $facultyAbbreviation) {\n    id\n    name\n    number\n    description\n    faculty {\n      id\n      abbreviation\n    }\n    ...CourseOfferingPresenter_offeringData\n  }\n}\n\nfragment CourseOfferingPresenter_offeringData on Course {\n  courseOfferings {\n    sections {\n      id\n      componentType\n      number\n      classNumber\n      professorNames\n      status\n      waitListSize\n      campus\n      delivery\n      timingDetails {\n        daysOfWeekBitmap\n        time\n        location\n      }\n    }\n  }\n}\n"
  }
};
})();

(node as any).hash = "bc689c352185d4eafd3241233430404e";

export default node;
