import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import {  type LoginUser } from '../Interfaces/UserSchema'
import { zodResolver } from '@hookform/resolvers/zod'
import { useAppContext } from './Reusable/AppContext';
import axios from 'axios';
import { useState } from 'react';
import { userRegisterSchema, type RegisterUser } from '../Interfaces/UserRegisterSchema';

export default function Register() {
    const {url} = useAppContext() ;
    const navigate = useNavigate()
    const {register,handleSubmit,formState : {errors}, } = useForm<RegisterUser>({resolver: zodResolver(userRegisterSchema),});
    const [errorMsg, setErrorMsg] = useState("") ;
    const onSubmit = async (user: LoginUser) => {
        console.log("user: ",user) ;
        console.log("fetching...") ;
        try{
            const resp = await axios.post(`${url}/api/auth/register`,user) ;
            console.log(resp) ;
            if (resp.data){
                // console.log("extract", logged) ;

                // const logged : LoggedInUser = resp.data as LoggedInUser ;
                navigate("/login");

            }
        }
        catch(err){
            if (err.response){
                setErrorMsg(err.response.data) ;
                console.log("error occ: ", err.response.data);
                console.log("error occ: ", err.response.status);

            }
            else{

                console.log("error occ: ", err.message);
            }
        }
    } 

    

  return (
    <div className='h-screen' style={{backgroundImage:"url(https://primotipo.com/wp-content/uploads/2015/04/image19.jpg)"}}>
        <div className="grid place-items-center " >
            <h1 className='p-20'>Register User Account</h1>
            <form onSubmit={handleSubmit(onSubmit)} className='space-y-4 max-w-md'>
                <fieldset className="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4">

                <label className="label">Username</label>
                <input type="text" className="input " placeholder="Username" {...register("Username")} />
                {errors.Username && <p className='text-red-500'>{errors.Username.message}</p>}

                <label className="label">Password</label>
                <input type="password" className="input" placeholder="Password" {...register("Password")}/>
                {errors.Password && <p className='text-red-500'>{errors.Password.message}</p>}

                <label className="label">Confirm Password</label>
                <input type="password" className="input" placeholder="Condfirm Password" {...register("ConfirmPassword")}/>
                {errors.ConfirmPassword && <p className='text-red-500'>{errors.ConfirmPassword.message}</p>}

                <button className="btn btn-neutral mt-4" type='submit'>Register</button> 
                {errorMsg.length > 0 ? <p className='text-red-300'>{errorMsg}</p> : ""}
                <Link to='/login' className="btn btn-neutral mt-4">Login</Link>
                </fieldset>
            </form>
        </div>
    </div>
  )
}
