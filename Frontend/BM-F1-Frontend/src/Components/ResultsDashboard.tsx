import React, { useEffect, useState } from 'react'
import type { GrandPrixResult } from '../Interfaces/GrandPrixResult'
import axios from 'axios'
import { useAppContext } from './Reusable/AppContext'

export default function ResultsDashboard() {

    const {url} = useAppContext() ;
    const [results, setResults] = useState<GrandPrixResult>({} as GrandPrixResult)
    
    useEffect(()=>{
        const fetchData = async ()=>{
            console.log("fetching...") ;
            try{
                const resp = await axios.get(`${url}/api/GrandPrixResults`) ;
                console.log(resp) ;
            }
            catch(err){
                console.log("error occ: ", err);
            }
        }
        fetchData() ;
    },[])
  return (
    <div>ResultsDashboard</div>
  )
}
