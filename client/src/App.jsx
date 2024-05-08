import React, { useState } from 'react';
import './css/style.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Poll from './components/Poll';
import Home from './pages/Home';
import Login from './pages/Login';
import Review from './pages/Review';
import Entity from './pages/Entity';

function App() {
  return (  
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/poll" element={<Poll />} />
        <Route path="/login" element={<Login />} />
        <Route path="/review" element={<Review />} />
        <Route path="/entity" element={<Entity />} />
        
      </Routes>
    </Router>
  )
}

export default App
