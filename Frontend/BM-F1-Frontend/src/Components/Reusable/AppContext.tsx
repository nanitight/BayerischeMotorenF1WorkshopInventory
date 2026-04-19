import React, { createContext, useContext } from "react";

export interface AppContext{
    url : string 
}

const AppContext = createContext<AppContext | undefined>(undefined) ;

export const AppContextProvider : React.FC<{ children: React.ReactNode }> = ({children} ) =>{
    const url = "http://localhost:5014" ;
    return (
        <AppContext.Provider value={{
            url
        }} >
            {children}
        </AppContext.Provider>
    );
}

export const useAppContext = ()=>  useContext(AppContext) ;

