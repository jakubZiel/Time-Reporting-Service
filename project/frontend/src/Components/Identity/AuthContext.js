import {createContext} from 'react'

const AuthContext = createContext({
    user : null,
    setUser : (u) => {}
})

export default AuthContext