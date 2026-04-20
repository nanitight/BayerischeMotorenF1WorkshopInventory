import React from 'react'
import { useForm } from 'react-hook-form'
import { grandPrixResultSchema,type GrandPrixResultZod } from '../Interfaces/GrandPrixResultSchema'
import { zodResolver } from '@hookform/resolvers/zod'
import axios from 'axios';
import { useAppContext } from './Reusable/AppContext';


export default function AddGrandPrixResult() {

    const {register,handleSubmit, formState : {errors}} = useForm<GrandPrixResultZod>({resolver  : zodResolver(grandPrixResultSchema)}) ;
    const {url,loggedInUser,loading,setLoading,setErrorMsg,} = useAppContext() ;
    const onSubmitFunc = async (res: GrandPrixResultZod) =>{
         
        console.log("fetching...") ;
        setLoading(true);
        try{
            const resp = await axios.post(`${url}/api/GrandPrixResults`,res,{
                headers : { Authorization : `Bearer ${loggedInUser.token}`}
            }) ;
            console.log(resp) ;
            if (resp.data){
                setErrorMsg("") ;
            }
        }
        catch(err){
            if (err.response){
                if (err.response.status >= 400)
                setErrorMsg("something went wrong... "+err.response.data) ;
                console.log("error occ: ", err.response);
            }
            else{
                console.log("error occ: ", err);

            }
        }
        finally{
            setLoading(false)
        }
    }
    
  return (
    
    <div className='flex items-center justify-center mt-5'>
        <div className="card card-border bg-base-100 w-96">
            <div className="card-body">
                <h2 className="card-title">Add Race Result</h2>
                <form onSubmit={handleSubmit(onSubmitFunc)}>
                    <label className='input '>
                        <svg width="24px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M12 21C15.5 17.4 19 14.1764 19 10.2C19 6.22355 15.866 3 12 3C8.13401 3 5 6.22355 5 10.2C5 14.1764 8.5 17.4 12 21Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>
                        <input {...register("Location")} type="text" className="input " placeholder="Location"/>
                    </label>
                    {errors.Location && <p className='text-red-500'>{errors.Location.message}</p>}
                    <div className="card-actions justify-end">
                    <button type='submit' className="btn btn-primary">Add</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
  )
}
