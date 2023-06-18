import { StarIcon } from "@heroicons/react/20/solid";

export interface RatingBarData {
  rating: number;
  label: string;
}

export const RatingBar = (props: { ratingBarData: RatingBarData }) => {
  const { rating, label } = props.ratingBarData;

  return (
    <div className="px-4">
      <h2
        className="text-2xl font-bold tracking-tight text-gray-900"
        style={{ width: "150px" }}
      >
        {label}
      </h2>
      <div className="mt-3">
        <div className="flex items-center text-sm">
          <dt className="flex flex-1 items-center">
            <div aria-hidden="true" className="ml-1 flex flex-1 items-center">
              <StarIcon
                className="text-yellow-400 h-5 w-5 flex-shrink-0"
                aria-hidden="true"
              />
              <div className="relative ml-3 flex-1">
                <div className="h-3 rounded-full border border-gray-200 bg-gray-100" />
                <div
                  className="absolute inset-y-0 rounded-full border border-yellow-400 bg-yellow-400"
                  style={{
                    width: `calc(${rating} * 100%)`,
                  }}
                />
              </div>
            </div>
          </dt>
          <dd className="ml-3 w-10 text-right text-sm tabular-nums text-gray-900">
            {Math.round(rating * 100)}%
          </dd>
        </div>
      </div>
    </div>
  );
};
