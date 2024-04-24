import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import CreatePoll from './components/CreatePoll.jsx'
import './index.css'
import { createRoot } from 'react-dom/client';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {/* <App /> */}
    <h1>hello world</h1>
    <CreatePoll />
  </React.StrictMode>,
)
