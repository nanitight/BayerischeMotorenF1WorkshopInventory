import './App.css'
import Navbar from './Components/Navbar'
import { Route, Routes, BrowserRouter } from 'react-router-dom'
import Home from './Components/Home'
import ResultsDashboard from './Components/ResultsDashboard'
import Login from './Components/Login'  
import NotFoundPage from './Components/Reusable/NotFoundPage'

function App() {

  return (
   <>
      <BrowserRouter>
      <Navbar/>
        <Routes>

          <Route path="/" element={<Home />}/>
          <Route path="/login" element={<Login />}/>
          <Route path="/dashboard" element={<ResultsDashboard />}/>
          
          <Route path="*" element={<NotFoundPage />}/>

        </Routes>
      </BrowserRouter>
   </>
  )
}

export default App
