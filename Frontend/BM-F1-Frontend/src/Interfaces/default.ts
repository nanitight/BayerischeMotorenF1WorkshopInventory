export interface User{
    name: string ,
    role : string ,
    id : string
}
export interface AppContext{
    url : string ,
    loggedInUser : User,
    setLoggedInUser : (user: User) => void
}
