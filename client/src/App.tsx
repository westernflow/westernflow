import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Course } from './Models/definitions';
import { useEffect } from 'react';

function App() {
  // fetch data from backend API endpoint http://localhost:8080/api/v1/courses?course-faculty=exact:PSYCHOL&section-class-number=exact:8045 
  // turn it into json data then display it in the browser
  
  // useState to store json data
  const [courses, setCourses] = React.useState<Course[]>([]);

  // useEffect to fetch data from backend API endpoint
  useEffect(() => {
    fetch('http://localhost:8080/api/v1/courses?course-faculty=exact:PSYCHOL')
      .then(response => response.json())
      .then(data => setCourses(data as Course[]))
      .then(() => console.log("RERENDER"))
  }, []);

  useEffect(() => {
    console.log(courses);
  }, [courses]);
   

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <ul>
          {courses.map((course,idx) => (
              <React.Fragment key={idx}><br/>PSYCHOL {course.courseData.Number}: {course.courseData.Name}</React.Fragment>
          ))
          }              
        </ul>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
