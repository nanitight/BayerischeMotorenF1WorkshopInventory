import React from 'react'
import { Link } from 'react-router'
import { useAppContext } from './Reusable/AppContext'
import type { LoggedInUser } from '../Interfaces/default';

export default function Navbar() {

    const {loggedInUser,setLoggedInUser} = useAppContext() ;

    const logOut = () => { 
        setLoggedInUser({} as LoggedInUser) ;
    }

  return (
    <div>
        <div className="navbar bg-base-100 shadow-sm">
        <div className="flex-1">
        <Link to='/' className="btn btn-ghost text-xl" >Bayerische Motoren F1 Workshop </Link>
        </div>
        <div className="flex-none">
        <Link to='/dashboard' className="dropdown ">
            <div  className="btn btn-ghost btn-circle">
            <div className="indicator">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z" />
                </svg>
            </div>
            </div>
            
        </Link>
        <div className="dropdown dropdown-end">
            <div tabIndex={0} role="button" className="btn btn-ghost btn-circle avatar">
            <div className="w-10 rounded-full">
                <img
                alt="Tailwind CSS Navbar component"
                src="https://img.daisyui.com/images/stock/photo-1534528741775-53994a69daeb.webp" />
            </div>
            </div>
            <ul
            tabIndex={-1}
            className="menu menu-sm dropdown-content bg-base-100 rounded-box z-1 mt-3 w-52 p-2 shadow">
                {
                    /* If logged in , show profile, settings, and logout else, show login link */
                    loggedInUser.token != null && loggedInUser.token.length > 0 ? 
                        <>
                            <li>
                                <a className="justify-between">
                                Profile
                                <span className="badge">New</span>
                                </a>
                            </li>
                            <li><a>Settings</a></li>
                            <li><a onClick={logOut}>Logout</a></li>
                        </>
                    :
                        <>
                            <li>
                                <Link to="/login"> Login</Link>
                            </li>
                        </>
                }
            </ul>
        </div>
        </div>
        </div>
    </div>
  )
}
