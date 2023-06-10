import { useLocation, useNavigate, useParams } from "react-router-dom";
import { Professor } from "../models/professor";
import { useEffect, useState } from "react"
import {
  ContentContainer,
  YellowPurpleGradiant,
} from "../constants/styleNames";
import { stripSuffix } from "../utils/common";
import { Navbar } from "../shared/Navbar";

export const ProfessorPresenter = () => {
  const history = useNavigate();
  const location = useLocation();
  const { professorName } = useParams();
  const [professor, setProfessor] = useState<Professor>();

  useEffect(() => {
    if (professorName == null) return;

    const getProfessor = async () => {
      const response = await fetch(
        `http://localhost:8080/api/v1/professors/${professorName}`
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
      <div style={{ position: "sticky", top: 0 }}>
        <Navbar />
      </div>
      <div className="flex flex-col min-h-screen">
        <div className={`${YellowPurpleGradiant} text-yellow-50 pt-10 pb-4`}>
          <h1 className={`${ContentContainer} text-3xl font-bold`}>
            {professor.rmpName !== "" || professor.rmpName != null
              ? professor.rmpName
              : professor.name}
          </h1>
        </div>
        <div className="bg-white">
          <h3
            className={`${ContentContainer} text-m pt-10 pb-10 text-gray-700`}
          >
            {professor.currentCourses.length > 0 ? (
              <div>
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
        </div>
        <div className="grow bg-slate-100"></div>
      </div>
    </div>
  ) : null;
};
