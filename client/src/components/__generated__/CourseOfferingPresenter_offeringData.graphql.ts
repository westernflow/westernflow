/**
 * @generated SignedSource<<741b2bf7bb07d2bd98dc1f6b73efb9ef>>
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

import { Fragment, ReaderFragment } from 'relay-runtime';
export type Campus = "ALL" | "BRESCIA" | "HURON" | "IVEY" | "KING" | "MAIN" | "UNKNOWN" | "%future added value";
export type ComponentType = "ALL" | "LAB" | "LECTURE" | "TUTORIAL" | "UNDEFINED" | "%future added value";
export type DeliveryType = "HYBRID" | "IN_PERSON" | "ONLINE" | "OTHER" | "UNDEFINED" | "%future added value";
export type StatusType = "FULL" | "NOT_FULL" | "UNDEFINED" | "%future added value";
import { FragmentRefs } from "relay-runtime";
export type CourseOfferingPresenter_offeringData$data = {
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
  readonly " $fragmentType": "CourseOfferingPresenter_offeringData";
};
export type CourseOfferingPresenter_offeringData$key = {
  readonly " $data"?: CourseOfferingPresenter_offeringData$data;
  readonly " $fragmentSpreads": FragmentRefs<"CourseOfferingPresenter_offeringData">;
};

const node: ReaderFragment = {
  "argumentDefinitions": [],
  "kind": "Fragment",
  "metadata": null,
  "name": "CourseOfferingPresenter_offeringData",
  "selections": [
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
  "type": "Course",
  "abstractKey": null
};

(node as any).hash = "64cfd29fd13f9f375744bbd71a93add5";

export default node;
