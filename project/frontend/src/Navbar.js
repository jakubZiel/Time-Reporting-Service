import { useContext } from 'react'
import {Navbar, Nav} from 'react-bootstrap'
import { Link } from 'react-router-dom'
import AuthContext from './Components/Identity/AuthContext'

export const Navigation = () => {

    const {user} = useContext(AuthContext)

    return <div>
        <Navbar bg="light" expand="lg">
            <Navbar.Brand>TRS Service</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                    <Nav.Link as ={Link} to={"/login"}>Login</Nav.Link>
                    <Nav.Link as ={Link} to={"/logout"}>Logout</Nav.Link>
                    {user &&<Nav.Link as ={Link} to={"/projects"}>Projects</Nav.Link>}
                    {user &&<Nav.Link as ={Link} to={"/activities"}>Activities</Nav.Link>}
                    {user &&<Nav.Link as ={Link} to={"/reports"}>Reports</Nav.Link>}
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    </div>
}