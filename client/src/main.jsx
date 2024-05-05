import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import CreatePoll from './components/CreatePoll.jsx'
import Home from './pages/Home.jsx'
import Poll from './components/Poll.jsx'
import './index.css'
import { createRoot } from 'react-dom/client';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {/* <App /> */}
    {/* <h1>home</h1> */}
    {/* <CreatePoll /> */}
    {/* <Home /> */}
    <Poll />
  </React.StrictMode>,
)
