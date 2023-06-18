import { StarRating } from "./StarRating";
import { ProfessorReview } from "../models/professor";

export interface ProfessorReviewProps {
  professorReview: ProfessorReview;
}

export const ProfessorReviewComponent = (props: {
  professorReview: ProfessorReviewProps;
}) => {
  const { professorReview } = props.professorReview;

  return (
    <div className="bg-white w-full">
      <div className="overflow-x-auto px-12">
        <div className="mt-6 space-y-10 divide-y divide-gray-200 border-b border-t border-gray-200 pb-10">
          <div className="pt-10 grid grid-cols-10">
            <div className="col-start-4 col-span-7">
              <div className="grid grid-rows-3 md:grid-rows-1 md:grid-cols-3 items-center text-gray-700">
                <StarRating
                  StarRatingProps={{
                    numStars: professorReview.difficulty,
                    label: "Easy",
                  }}
                />
                <StarRating
                  StarRatingProps={{
                    numStars: professorReview.clarity,
                    label: "Clarity",
                  }}
                />
                <StarRating
                  StarRatingProps={{
                    numStars: professorReview.quality,
                    label: "Liked",
                  }}
                />
              </div>

              <div className="mt-4">
                <div className="mt-3 space-y-6 text-m text-gray-600">
                  {professorReview.reviewText}
                </div>
              </div>
            </div>

            <div className="col-start-1 row-start-1 col-span-2 mt-6 flex flex-col text-sm">
              <div className="rounded-full border-2 h-12 w-12 items-center justify-center">
                <div className="flex">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke-width="1.5"
                    stroke="currentColor"
                    className="h-9 w-9 items-center justify-center m-auto"
                  >
                    <path
                      stroke-linecap="round"
                      stroke-linejoin="round"
                      d="M14.25 6.087c0-.355.186-.676.401-.959.221-.29.349-.634.349-1.003 0-1.036-1.007-1.875-2.25-1.875s-2.25.84-2.25 1.875c0 .369.128.713.349 1.003.215.283.401.604.401.959v0a.64.64 0 01-.657.643 48.39 48.39 0 01-4.163-.3c.186 1.613.293 3.25.315 4.907a.656.656 0 01-.658.663v0c-.355 0-.676-.186-.959-.401a1.647 1.647 0 00-1.003-.349c-1.036 0-1.875 1.007-1.875 2.25s.84 2.25 1.875 2.25c.369 0 .713-.128 1.003-.349.283-.215.604-.401.959-.401v0c.31 0 .555.26.532.57a48.039 48.039 0 01-.642 5.056c1.518.19 3.058.309 4.616.354a.64.64 0 00.657-.643v0c0-.355-.186-.676-.401-.959a1.647 1.647 0 01-.349-1.003c0-1.035 1.008-1.875 2.25-1.875 1.243 0 2.25.84 2.25 1.875 0 .369-.128.713-.349 1.003-.215.283-.4.604-.4.959v0c0 .333.277.599.61.58a48.1 48.1 0 005.427-.63 48.05 48.05 0 00.582-4.717.532.532 0 00-.533-.57v0c-.355 0-.676.186-.959.401-.29.221-.634.349-1.003.349-1.035 0-1.875-1.007-1.875-2.25s.84-2.25 1.875-2.25c.37 0 .713.128 1.003.349.283.215.604.401.96.401v0a.656.656 0 00.658-.663 48.422 48.422 0 00-.37-5.36c-1.886.342-3.81.574-5.766.689a.578.578 0 01-.61-.58v0z"
                    />
                  </svg>
                </div>
              </div>

              <p className="font-medium text-gray-900">Anonymous Atom</p>
              <time className="mt-2 text-gray-500">
                {professorReview.date.split(" ")[0]}
              </time>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
