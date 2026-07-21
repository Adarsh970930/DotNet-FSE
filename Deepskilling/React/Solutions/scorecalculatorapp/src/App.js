import React from 'react';
import './Stylesheets/mystyle.css';
import CalculateScore from './Components/CalculateScore';

function App() {
  return (
    <div className="App">
      <h1>scorecalculatorapp Application</h1>
      <CalculateScore />
    </div>
  );
}

export default App;