import React from 'react';

function IndianPlayers() {
  const oddTeam = ["Sachin1", "Virat3", "Yuvraj5"];
  const evenTeam = ["Dhoni2", "Rohit4", "Raina6"];

  const [first, , third, , fifth] = oddTeam;
  const [, second, , fourth, , sixth] = evenTeam;

  const T20Players = ["Mr. First Player", "Mr. Second Player", "Mr. Third Player"];
  const RanjiPlayers = ["Mr. Fourth Player", "Mr. Fifth Player", "Mr. Sixth Player"];

  const allPlayers = [...T20Players, ...RanjiPlayers];

  return (
    <div>
      <h2>Odd Players</h2>
      <ul>
        <li>First : {first}</li>
        <li>Third : {third}</li>
        <li>Fifth : {fifth}</li>
      </ul>

      <hr />

      <h2>Even Players</h2>
      <ul>
        <li>Second : {second}</li>
        <li>Fourth : {fourth}</li>
        <li>Sixth : {sixth}</li>
      </ul>

      <hr />

      <h2>List of Indian Players Merged:</h2>
      <ul>
        {allPlayers.map((p, i) => (
          <li key={i}>{p}</li>
        ))}
      </ul>
    </div>
  );
}

export default IndianPlayers;