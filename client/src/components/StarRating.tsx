import { StarIcon } from "@heroicons/react/20/solid";

function classNames(...classes: any) {
  return classes.filter(Boolean).join(" ");
}

export interface StarRatingProps {
  numStars: number;
  label: string;
}

export const StarRating = (props: { StarRatingProps: StarRatingProps }) => {
  const { numStars, label } = props.StarRatingProps;

  return (
    <div className="flex flex-col">
      <div className="text-sm">{label}</div>
      <div className="flex">
        <div className="flex items-start">
          {[0, 1, 2, 3, 4].map((rating) => (
            <StarIcon
              key={rating}
              className={classNames(
                numStars > rating ? "text-yellow-400" : "text-gray-200",
                "h-5 w-5 flex-shrink-0"
              )}
              aria-hidden="true"
            />
          ))}
        </div>
      </div>

    </div>
  );
};
