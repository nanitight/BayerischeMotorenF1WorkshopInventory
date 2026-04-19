import React, { createContext, useContext, useState } from "react";
import { type AppContext, type LoggedInUser} from "../../Interfaces/default";


const AppContext = createContext<AppContext | undefined>(undefined) ;

export const AppContextProvider : React.FC<{ children: React.ReactNode }> = ({children} ) =>{
    const url = "http://localhost:5014" ;
    const [loggedInUser,setLoggedInUser] = useState<LoggedInUser>({} as LoggedInUser) ;
    return (
        <AppContext.Provider value={{
            url, loggedInUser, setLoggedInUser
        }} >
            {children}
        </AppContext.Provider>
    );
}

export const useAppContext = ()=>  useContext(AppContext) ;

    