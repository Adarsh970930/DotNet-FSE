import React from 'react';
import './OfficeList.css'; // CSS will be created below

function OfficeList() {
  const offices = [
    {
      id: 1,
      name: "DBS",
      rent: 50000,
      address: "Chennai",
      image: "https://via.placeholder.com/300x200.png?text=Office+1",
    },
    {
      id: 2,
      name: "WeWork",
      rent: 65000,
      address: "Bangalore",
      image: "https://via.placeholder.com/300x200.png?text=Office+2",
    },
    {
      id: 3,
      name: "Regus",
      rent: 58000,
      address: "Hyderabad",
      image: "https://via.placeholder.com/300x200.png?text=Office+3",
    }
  ];

  return (
    <div style={{ padding: '20px' }}>
      <h1>Office Space , at Affordable Range</h1>

      {offices.map((office) => (
        <div key={office.id} className="office-card">
          <img src={office.image} alt="office" />
          <h2>Name: {office.name}</h2>
          <p
            className={`rent ${office.rent < 60000 ? 'low' : 'high'}`}
          >
            Rent: Rs. {office.rent}
          </p>
          <p><strong>Address:</strong> {office.address}</p>
          <hr />
        </div>
      ))}
    </div>
  );
}

export default OfficeList;