import React, { createContext, useContext, useState } from "react";
import { type AppContext, type LoggedInUser} from "../../Interfaces/default";
import type { DbTrackedGrandPrixResult } from "../../Interfaces/GrandPrixResult";


const AppContext = createContext<AppContext | undefined>(undefined) ;

export const AppContextProvider : React.FC<{ children: React.ReactNode }> = ({children} ) =>{
    // const url = "http://localhost:5014" ;
    const url = "https://bmf-9cvpu62s.b4a.run" ;
    
    const [loggedInUser,setLoggedInUser] = useState<LoggedInUser>({} as LoggedInUser) ;
    const [loading, setLoading] = useState(false) ;
    const [errorMsg, setErrorMsg] = useState<string>("")
    const [resultToEdit, setResultToEdit] = useState<DbTrackedGrandPrixResult>({} as DbTrackedGrandPrixResult  )
    
    return (
        <AppContext.Provider value={{
            url, loggedInUser, setLoggedInUser,loading,setLoading,errorMsg,setErrorMsg,resultToEdit,setResultToEdit
        }} >
            {children}
        </AppContext.Provider>
    );
}

export const useAppContext = ()=>  {
    const context = useContext(AppContext) ;
    if (context == null){
        throw new Error("AppContext must be used within an AppContextProvider")
    }
    return context ;
}
    