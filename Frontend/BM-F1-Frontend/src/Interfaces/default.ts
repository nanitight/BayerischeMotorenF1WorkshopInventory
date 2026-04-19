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
    setLoggedInUser : (user: LoggedInUser) => void
}
