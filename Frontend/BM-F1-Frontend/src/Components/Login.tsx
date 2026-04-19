import React from 'react'
import { Link } from 'react-router-dom'

export default function Login() {
  return (
    <div className="flex items-center justify-center ">
        <form>
            <fieldset className="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4">
            <legend className="fieldset-legend">Login</legend>

            <label className="label">Username</label>
            <input type="text" className="input" placeholder="Username" />

            <label className="label">Password</label>
            <input type="password" className="input" placeholder="Password" />

            <button className="btn btn-neutral mt-4">Login</button>
            <Link to='/register' className="btn btn-neutral mt-4">Register</Link>
            </fieldset>
        </form>
    </div>
  )
}
