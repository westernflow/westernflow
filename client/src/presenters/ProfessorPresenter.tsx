import { useLocation, useNavigate, useParams } from "react-router-dom";
import { Professor } from "../models/professor";
import { useEffect, useState } from "react";
import {
  ContentContainer,
  YellowPurpleGradiant,
} from "../constants/styleNames";
import { stripSuffix } from "../utils/common";
import { Navbar } from "../shared/Navbar";
import { ProfessorRating } from "../components/ProfessorRating";
import { ProfessorReviewComponent } from "../components/Review";

export const ProfessorPresenter = () => {
  const history = useNavigate();
  const location = useLocation();
  const { professorName } = useParams();
  const [professor, setProfessor] = useState<Professor>();

  const clarityAverage =
    professor != null && professor.reviews.length !== 0
      ? professor.reviews
          .map((review) => review.clarity)
          .reduce((acc, clarity) => acc + clarity) / professor.reviews.length
      : 0;

  useEffect(() => {
    if (professorName == null) return;

    const getProfessor = async () => {
      const response = await fetch(
			`https://westernflow-backend.fly.dev/api/v1/professors/${professorName}`
		);

      if (!response.ok) {
        console.error("The request was not successful", response);
        return;
      }

      const data = await response.json();
      setProfessor(data);
    };

    getProfessor();
  }, [location.search, professorName]);

  useEffect(() => console.log(professor), [professor]);

  const goToCourse = (result: string) => {
    // remove spaceas from result
    result = result.replace(/\s/g, "");
    history(`/course/${result}`);
  };

  return professor ? (
    <div style={{ position: "relative", minHeight: "100vh" }}>
      <div style={{ position: "sticky", top: 0, zIndex: 10 }}>
        <Navbar />
      </div>
      <div className="flex flex-col min-h-screen">
        <div className={`${YellowPurpleGradiant} text-yellow-50 pt-10 pb-4`}>
          <div className={`${ContentContainer}`}>
            <div className="flex flex-col w-full lg:flex-row">
              <div className="flex flex-col">
                <div className={`text-5xl font-bold pb-6`}>
                  {professor.rmpName !== "" || professor.rmpName != null
                    ? professor.rmpName
                    : professor.name}
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className="bg-white">
          <h3
            className={`${ContentContainer} text-lg pt-10 pb-10 text-gray-700`}
          >
            {professor.currentCourses.length > 0 ? (
              <div className="justify-start">
                Currently teaches:{" "}
                {professor.currentCourses.map((course, idx) => (
                  <div className="inline">
                    <span
                      onClick={() => goToCourse(stripSuffix(course))}
                      key={idx}
                      className="underline text-orange-400 hover:text-orange-500 hover:cursor-pointer font-bold"
                    >
                      {course}
                    </span>
                    {idx !== professor.currentCourses.length - 1 ? "," : null}{" "}
                  </div>
                ))}
              </div>
            ) : (
              "Not currently teaching any courses"
            )}
          </h3>
          <div className={`${ContentContainer}`}>
            <ProfessorRating
              ratingData={{
                rating1: professor.difficulty / 5,
                rating2: clarityAverage / 5,
                label1: "Easy",
                label2: "Clear",
                likedPercentage: professor.rating / 5,
              }}
            />
          </div>
        </div>
        <div className="grow bg-slate-100 pb-12">
          <div
            className={`${ContentContainer} mt-10 pb-10 bg-white rounded-2xl shadow-2xl`}
          >
            <div className="w-full">
              {professor.reviews.map((review, idx) => (
                <ProfessorReviewComponent
                  key={idx}
                  professorReview={{ professorReview: professor.reviews[idx] }}
                />
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  ) : null;
};
