import React, { useState } from 'react';
import './css/style.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Poll from './components/Poll';
import Home from './pages/Home';
import Login from './pages/Login';
import Review from './pages/Review';
import Entity from './pages/Entity';
import Choose from './pages/Choose';
import Search from './pages/Search';

function App() {
  return (  
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/poll" element={<Poll />} />
        <Route path="/login" element={<Login />} />
        <Route path="/review" element={<Review />} />
        <Route path="/entity" element={<Entity />} />
        <Route path="/choose" element={<Choose />} />
        <Route path="/search" element={<Search />} />
        
      </Routes>
    </Router>
  )
}

export default App
