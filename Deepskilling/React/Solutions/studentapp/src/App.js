import React from 'react';
import './Stylesheets/mystyle.css';
import CalculateScore from './Components/CalculateScore';

function App() {
  return (
    <div className="App">
      <h1>studentapp Application</h1>
      <CalculateScore />
    </div>
  );
}

export default App;