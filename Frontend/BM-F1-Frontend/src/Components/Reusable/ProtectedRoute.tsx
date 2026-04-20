import { useAppContext } from './AppContext'
import { Navigate, Outlet } from 'react-router-dom';

export default function ProtectedRoute() {
    const {loggedInUser} = useAppContext() ;

    if (loggedInUser.token == null || loggedInUser.token == ""){
        return <Navigate to="/login" replace />
    }
    return <Outlet />

}
