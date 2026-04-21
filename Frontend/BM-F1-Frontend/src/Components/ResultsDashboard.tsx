import React, { useEffect, useState } from 'react'
import type { DbTrackedGrandPrixResult, GrandPrixResult } from '../Interfaces/GrandPrixResult'
import axios from 'axios'
import { useAppContext } from './Reusable/AppContext'
import ResultsTable from './Render/ResultsTable';
import { Link } from 'react-router-dom';

export default function ResultsDashboard() {

    const {url,loggedInUser,setLoading,loading,errorMsg,setErrorMsg} = useAppContext() ;
    const [results, setResults] = useState<DbTrackedGrandPrixResult[]>( [] as DbTrackedGrandPrixResult[])
    
    const fetchData = async ()=>{
        console.log("fetching...") ;
        setLoading(true);
        try{
            const resp = await axios.get(`${url}/api/GrandPrixResults`,{
                headers : { Authorization : `Bearer ${loggedInUser.token}`}
            }) ;
            if (resp.data){
                setResults(resp.data) ;
                setErrorMsg("") ;
            }
            console.log(resp) ;
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


    const deleteResult = async(id:string)=>{
        var delAction = confirm("Are you sure you want to delete record? ") ; 
        if (id == "" || id.length< 3 || !delAction) return 

         setLoading(true);
        try{
            const resp = await axios.delete(`${url}/api/GrandPrixResults/Delete/${id}`,{
                headers : { Authorization : `Bearer ${loggedInUser.token}`}
            }) ;
            console.log(resp) ;
            if (resp.data){
                setErrorMsg("") ;
                await fetchData();
            }
        }
        catch(err){
            if (err.response){
                if (err.response.status == 403)
                    setErrorMsg("You are not Authorized for that action") ;
                else if (err.response.status >= 400)
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

    useEffect(()=>{
        fetchData() ;
    },[])


    if (loading) return (<>
            <span className="loading loading-infinity loading-xl"></span>
        </>)

  return (
    <div>
        <h1>
            ResultsDashboard
        </h1>

        {results.length <= 0 ? 
            <>There's nothing do you want to add a result? 
                <Link to="/addresult    " className="btn btn-success bg-white text-black border-[#e5e5e5]">
                   <svg fill="#000000" height="16px" width="16px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 330 330" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <path d="M281.672,48.328C250.508,17.163,209.073,0,164.999,0C120.927,0,79.492,17.163,48.328,48.328 c-64.333,64.334-64.333,169.011,0,233.345C79.492,312.837,120.927,330,165,330c44.073,0,85.508-17.163,116.672-48.328 C346.005,217.339,346.005,112.661,281.672,48.328z M260.46,260.46C234.961,285.957,201.06,300,165,300 c-36.06,0-69.961-14.043-95.46-39.54c-52.636-52.637-52.636-138.282,0-190.919C95.039,44.042,128.94,30,164.999,30 c36.06,0,69.961,14.042,95.46,39.54C313.095,122.177,313.095,207.823,260.46,260.46z"></path> <path d="M254.999,150H180V75c0-8.284-6.716-15-15-15s-15,6.716-15,15v75H75c-8.284,0-15,6.716-15,15s6.716,15,15,15h75v75 c0,8.284,6.716,15,15,15s15-6.716,15-15v-75h74.999c8.284,0,15-6.716,15-15S263.284,150,254.999,150z"></path> </g> </g></svg>
                    Add Result
                </Link>
            </>
            : 
            <div className="overflow-x-auto">
                <Link to="/addresult    " className="btn bg-white text-black border-[#e5e5e5]">
                   <svg fill="#000000" height="16px" width="16px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 330 330" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <path d="M281.672,48.328C250.508,17.163,209.073,0,164.999,0C120.927,0,79.492,17.163,48.328,48.328 c-64.333,64.334-64.333,169.011,0,233.345C79.492,312.837,120.927,330,165,330c44.073,0,85.508-17.163,116.672-48.328 C346.005,217.339,346.005,112.661,281.672,48.328z M260.46,260.46C234.961,285.957,201.06,300,165,300 c-36.06,0-69.961-14.043-95.46-39.54c-52.636-52.637-52.636-138.282,0-190.919C95.039,44.042,128.94,30,164.999,30 c36.06,0,69.961,14.042,95.46,39.54C313.095,122.177,313.095,207.823,260.46,260.46z"></path> <path d="M254.999,150H180V75c0-8.284-6.716-15-15-15s-15,6.716-15,15v75H75c-8.284,0-15,6.716-15,15s6.716,15,15,15h75v75 c0,8.284,6.716,15,15,15s15-6.716,15-15v-75h74.999c8.284,0,15-6.716,15-15S263.284,150,254.999,150z"></path> </g> </g></svg>
                    Add Result
                </Link>
                <table className="table">
                    {/* head */}
                    <thead>
                    <tr>
                        <th></th>
                        <th>Race Day</th>
                        <th>Location</th>
                        <th>Points Scored</th>
                        <th>Position In Teams Grid</th>
                        
                    </tr>
                    </thead>
                    <tbody>            
                        {results.map((r : DbTrackedGrandPrixResult, ind : number)=> (
                            
                            <ResultsTable key={ind} deleteFunc={deleteResult} result={r}/>
                        ))}
                    </tbody>
                </table>
            </div>
         
            
        }
        

        {errorMsg.length>0 ? <p className='text-red-500'>
        {errorMsg}
        </p>
        : ""}
    </div>
  )
}
