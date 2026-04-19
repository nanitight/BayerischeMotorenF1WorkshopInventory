import { useForm } from 'react-hook-form'
import { Link } from 'react-router-dom'
import { userSchema, type LoginUser } from '../Interfaces/UserSchema'
import { zodResolver } from '@hookform/resolvers/zod'
import { useAppContext } from './Reusable/AppContext';
import axios from 'axios';

export default function Login() {
    const {url,setLoggedInUser} = useAppContext() ;
    const {register,handleSubmit,formState : {errors}, } = useForm<LoginUser>({resolver: zodResolver(userSchema),});
    const onSubmit = async (user: LoginUser) => {
        console.log("user: ",user) ;
        console.log("fetching...") ;
        try{
            const resp = await axios.post(`${url}/api/auth/login`,user) ;
            console.log(resp) ;
        }
        catch(err){
            if (err.response){
                console.log("error occ: ", err.response.data);
                console.log("error occ: ", err.response.status);

            }
            else{

                console.log("error occ: ", err.message);
            }
        }
    } 

    

  return (
    <div className="flex items-center justify-center ">
        <form onSubmit={handleSubmit(onSubmit)} className='space-y-4 max-w-md'>
            <fieldset className="fieldset bg-base-200 border-base-300 rounded-box w-xs border p-4">
            <legend className="fieldset-legend">Login</legend>

            <label className="label">Username</label>
            <input type="text" className="input " placeholder="Username" {...register("Username")} />
            {errors.Username && <p className='text-red-500'>{errors.Username.message}</p>}

            <label className="label">Password</label>
            <input type="password" className="input" placeholder="Password" {...register("Password")}/>
            {errors.Password && <p className='text-red-500'>{errors.Password.message}</p>}

            <button className="btn btn-neutral mt-4" type='submit'>Login</button>
            <Link to='/register' className="btn btn-neutral mt-4">Register</Link>
            </fieldset>
        </form>
    </div>
  )
}
