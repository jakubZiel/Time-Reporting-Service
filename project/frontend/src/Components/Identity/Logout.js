import { useContext, useState } from "react"
import AuthContext from "./AuthContext"
import {Button, Modal} from 'react-bootstrap'
import {Link} from 'react-router-dom'

export const Logout = () => {
    const {user, setUser} = useContext(AuthContext)

    return<div>
        Logout
        <LogoutPrompt show={true}/>
    </div>
}

export const LogoutPrompt = () => {
    const {user, setUser} = useContext(AuthContext)    
    const [show, setShow] = useState(user != null)
    
    return <Modal show={show}>
        <Modal.Header closeButton>
            <Modal.Title>Do you want to logout?</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <p>See you soon!</p>
        </Modal.Body>
        <Modal.Footer>
            <Link to={"/logout"}>
                <Button onClick={() => {
                    setShow(false)
                    setUser(null)
                }}>Confirm</Button>
            </Link>
            <Link to={"/activities"}>
                <Button>Back</Button>
            </Link>
        </Modal.Footer>
    </Modal>
}