import React, { useState } from 'react';
import './App.css';

function GuestPage({ onLogin }) {
  return (
    <div style={{ textAlign: 'center', marginTop: '100px' }}>
      <h2>Please sign up.</h2>
      <button onClick={onLogin}>Login</button>
    </div>
  );
}

function UserPage({ onLogout }) {
  return (
    <div style={{ textAlign: 'center', marginTop: '100px' }}>
      <h2>Welcome back</h2>
      <button onClick={onLogout}>Logout</button>
    </div>
  );
}

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogin = () => {
    setIsLoggedIn(true);
  }

  const handleLogout = () => {
    setIsLoggedIn(false);
  }

  return (
    <div className="App">
      {
        isLoggedIn ? 
          <UserPage onLogout={handleLogout} /> : 
          <GuestPage onLogin={handleLogin} />
      }
    </div>
  );
}

export default App;