// ComplaintRegister.js
import React, { useState } from 'react';

function ComplaintRegister() {
  const [name, setName] = useState('');
  const [complaint, setComplaint] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    const transactionId = Math.floor(Math.random() * 1000);
    alert(`Thanks ${name}\nYour Complaint was Submitted.\nTransaction ID is: ${transactionId}`);
    setName('');
    setComplaint('');
  };

  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <h2 style={{ color: 'red' }}>Register your complaints here!!!</h2>
      <form onSubmit={handleSubmit}>
        <div style={{ margin: '10px' }}>
          <label>Name:&nbsp;</label>
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
        <div style={{ margin: '10px' }}>
          <label>Complaint:&nbsp;</label>
          <textarea
            value={complaint}
            onChange={(e) => setComplaint(e.target.value)}
            required
          />
        </div>
        <button type="submit">Submit</button>
      </form>
    </div>
  );
}

export default ComplaintRegister;