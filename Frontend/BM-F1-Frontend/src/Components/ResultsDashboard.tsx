import React, { useEffect, useState } from 'react'
import type { GrandPrixResult } from '../Interfaces/GrandPrixResult'
import axios from 'axios'
import { useAppContext } from './Reusable/AppContext'

export default function ResultsDashboard() {

    const {url,loggedInUser} = useAppContext() ;
    const [results, setResults] = useState<GrandPrixResult[]>( [] as GrandPrixResult[])
    const [errorMsg, setErrorMsg] = useState<string>("")
    const [loading, setLoading] = useState(false) ;
    
    useEffect(()=>{
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
        fetchData() ;
    },[])


    if (loading) return (<>
            <span className="loading loading-infinity loading-xl"></span>
        </>)

  return (
    <div>ResultsDashboard

        {results.length <= 0 ? 
            <>There's nothing do you want to add a result?</>
            : <>
                {results.map((r : GrandPrixResult)=> (<p>{r.location}</p>))}
            </>
        }

        {errorMsg.length>0 ? <p className='text-red-500'>
        {errorMsg}
        </p>
        : ""}
    </div>
  )
}
