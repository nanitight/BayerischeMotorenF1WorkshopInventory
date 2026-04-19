import { useState } from 'react'
import './App.css'
import Navbar from './Components/Navbar'
import { Route, Routes, BrowserRouter } from 'react-router-dom'
import Home from './Components/Home'
import ResultsDashboard from './Components/ResultsDashboard'

function App() {

  return (
   <>
      <BrowserRouter>
      <Navbar/>
        <Routes>

          <Route path="/" element={<Home />}/>
          <Route path="/dashboard" element={<ResultsDashboard />}/>
        </Routes>
      </BrowserRouter>
   </>
  )
}

export default App
