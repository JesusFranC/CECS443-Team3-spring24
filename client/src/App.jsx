import React, { useState } from 'react';
import './css/style.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Poll from './components/Poll';
import Home from './pages/Home';
import Login from './pages/Login';
import Polls from './pages/Polls';


function App() {
  return (  
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/poll" element={<Poll />} />
        <Route path="/login" element={<Login />} />
        <Route path="/viewpolls" element={<Polls />} />
        
      </Routes>
    </Router>
  )
}

export default App
