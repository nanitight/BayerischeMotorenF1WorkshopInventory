import './App.css'
import Navbar from './Components/Navbar'
import { Route, Routes, BrowserRouter } from 'react-router-dom'
import Home from './Components/Home'
import ResultsDashboard from './Components/ResultsDashboard'
import Login from './Components/Login'  
import NotFoundPage from './Components/Reusable/NotFoundPage'
import ProtectedRoute from './Components/Reusable/ProtectedRoute'
import AddGrandPrixResult from './Components/AddGrandPrixResult'

function App() {

  return (
   <>
      <BrowserRouter>
      <Navbar/>
        <Routes>

          <Route path="/" element={<Home />}/>
          <Route path="/login" element={<Login />}/>

          <Route element={<ProtectedRoute />} >
            <Route path="/dashboard" element={<ResultsDashboard />}/>
            <Route path="/register" element={<ResultsDashboard />}/>
            <Route path="/addresult" element={<AddGrandPrixResult />}/>

          </Route>
          
          <Route path="*" element={<NotFoundPage />}/>

        </Routes>
      </BrowserRouter>
   </>
  )
}

export default App
