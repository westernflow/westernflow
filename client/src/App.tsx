import React from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
  // fetch data from backend API endpoint localhost:8080/api/v1/courses
  fetch('http://localhost:8080/api/v1/courses?course-faculty=exact:PSYCHOL&section-class-number=exact:8045')
    .then(response => response.json())
    .then(data => console.log(data));

  // ------ JSX below this point ------
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Hi Felo.
        </p>
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
