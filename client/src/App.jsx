import React, { useState } from 'react';
import './css/style.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Poll from './components/Poll';
import Home from './pages/Home';
import Login from './pages/Login';


function App() {
  return (  
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/poll" element={<Poll />} />
        <Route path="/login" element={<Login />} />
        
      </Routes>
    </Router>
  )
}

export default App
