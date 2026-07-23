import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Home from './Home';
import TrainerList from './TrainerList';
import TrainerDetails from './TrainerDetails';
import trainers from './TrainersMock';

const App = () => (
  <Router>
    <nav>
      <Link to="/">Home</Link> | <Link to="/trainers">Trainers</Link>
    </nav>
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/trainers" element={<TrainerList trainers={trainers} />} />
      <Route path="/trainer/:id" element={<TrainerDetails />} />
    </Routes>
  </Router>
);

export default App;