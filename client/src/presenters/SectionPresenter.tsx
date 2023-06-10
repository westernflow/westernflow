import { useNavigate } from "react-router-dom";
import { sectionCellStyle } from "../constants/styleNames";
import { SectionComponent } from "../models/common";
import { removeWhiteSpace } from "../utils/common";

function SectionPresenter(props: { section: SectionComponent }) {
  const history = useNavigate();
  const { section } = props;
  const days = ["M", "Tu", "W", "Th", "F"];
  const timeDays =
    section?.times != null ? section.times.map((time) => time.days) : null;
  return (
    <>
      {section != null ? (
        <>
          <div className={`${sectionCellStyle} font-semibold`}>
            {" "}
            {/*Section -- e.g. LEC 001*/}
            {section?.component + " " + section?.number}
          </div>
          <div className={`${sectionCellStyle}`}>{section?.classNumber}</div>
          <div className={`${sectionCellStyle}`}>
            {" "}
            {/*Filled or not filled*/}
            {section?.status}
          </div>
          <div className={`${sectionCellStyle} text-gray-500`}>
            {days.map((day, index) => (
              <span
                key={index}
                style={{
                  color:
                    timeDays != null && timeDays.includes(day)
                      ? "black"
                      : "normal",
                  fontWeight:
                    timeDays != null && timeDays.includes(day)
                      ? "bold"
                      : "normal",
                }}
              >
                {day}
                {index < days.length - 1 && " "}
              </span>
            ))}
          </div>
          <div className={`${sectionCellStyle} text-sm`}>
            {section.times?.length > 0
              ? section.times[0].startTime + " - " + section.times[0].endTime
              : "N/A"}
          </div>
          <div className={`${sectionCellStyle}`}>
            {section?.location == null || section?.location === ""
              ? "ONLINE"
              : section.location}
          </div>
          <div className={`${sectionCellStyle}`}>
            {section?.instructors != null ? section.instructors.map((instructor, idx) => {
              return (
                <div key={idx}>
                  <span className="underline text-green-500 hover:text-green-700 hover:cursor-pointer" onClick={() => history(`/professor/${removeWhiteSpace(instructor)}`)}>{instructor}</span>
                </div>
              );
            }) : null}
          </div>
          <div className={`${sectionCellStyle}`}>{section?.campus}</div>
        </>
      ) : (
        <div>hi</div>
      )}
    </>
  );
}
export default SectionPresenter;
