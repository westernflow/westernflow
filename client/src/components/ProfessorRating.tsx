import { RatingBar } from "../components/RatingBar";

export interface RatingComponent {
  rating1: number;
  rating2: number;
  label1: string;
  label2: string;
  likedPercentage: number;
}

export const ProfessorRating = (props: { ratingData: RatingComponent }) => {
  const { rating1, rating2, label1, label2, likedPercentage } =
    props.ratingData;

  return (
    <div className="bg-white rounded-xl">
      <div className="mx-auto max-w-2xl pb-4">
        <div className="grid grid-rows-3 gap-4 lg:flex lg:justify-around">
          <RatingBar
            ratingBarData={{ rating: likedPercentage, label: "Liked" }}
          />
          <RatingBar
            ratingBarData={{ rating: rating1, label: label1 }}
          />
          <RatingBar
            ratingBarData={{ rating: rating2, label: label2 }}
          />
        </div>
      </div>
    </div>
  );
};
