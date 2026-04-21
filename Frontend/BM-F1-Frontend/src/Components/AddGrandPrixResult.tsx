import { type GrandPrixResultZod } from '../Interfaces/GrandPrixResultSchema'
import axios, { AxiosError } from 'axios';
import { useAppContext } from './Reusable/AppContext';
import { useNavigate } from 'react-router-dom';
import type { GrandPrixResult } from '../Interfaces/GrandPrixResult';
import GrandPrixResultForm from './Reusable/GrandPrixResultForm';


export default function AddGrandPrixResult() {
   
    const {url,loggedInUser,loading,setLoading,setErrorMsg,} = useAppContext() ;
    const navigate = useNavigate() ;

    const onSubmitFunc = async (res: GrandPrixResultZod) =>{
        const dataToPost : GrandPrixResult = {
            raceDay: res.RaceDay.toISOString().split('T')[0],
            location: res.Location,
            pointsScored: res.PointsScored,
            positionInTeamGrid: res.PositionInTeamGrid
        };
        

        console.log("posting...",res,dataToPost ) ;
        setLoading(true);
        try{
            const resp = await axios.post(`${url}/api/GrandPrixResults/Create`,dataToPost,{
                headers : { Authorization : `Bearer ${loggedInUser.token}`, "Content-Type":"application/json"},

            }) ;
            console.log(resp) ;
            if (resp.data){
                setErrorMsg("") ;
                navigate("/dashboard");
                
            }
        }
        catch(err){
            if (err instanceof AxiosError && err.response){
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
    
      if (loading) return (<>
            <span className="loading loading-infinity loading-xl"></span>
        </>)

  return (
    
    <div className='flex items-center justify-center mt-5'>
        <div className="card card-border bg-base-100 w-96">
            <div className="card-body">
                <h2 className="card-title">Add Race Result</h2>
                <GrandPrixResultForm defaultResult={null} submitFunc={onSubmitFunc} />
            </div>
        </div>
    </div>
  )
}
