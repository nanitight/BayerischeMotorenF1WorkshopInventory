import type { DbTrackedGrandPrixResult } from "./GrandPrixResult";

export interface User{
    username: string ,
    role : string ,
    id : string
}

export interface LoggedInUser extends User {
    token: string ;
}
export interface AppContext{
    url : string ,
    loggedInUser : LoggedInUser,
    setLoggedInUser : (user: LoggedInUser) => void,
    loading:boolean,
    setLoading: (x:boolean)=>void,
    errorMsg:string,
    setErrorMsg:(m:string)=>void,
    resultToEdit: DbTrackedGrandPrixResult,
    setResultToEdit: (resultToEdit: DbTrackedGrandPrixResult)=>void
}
