import { Button, Form} from "react-bootstrap"
import axios from 'axios';
import { useContext, useEffect, useState } from "react";
import {useFetch} from 'use-http'
import AuthContext from "./AuthContext";
import {Row, Col, Container} from 'react-bootstrap'
import {Link} from 'react-router-dom'

export const Login = () => {

    const {setUser} = useContext(AuthContext)

    return <div>
        <h1>Login</h1>
        <EmployeeList/>
        <Form>
            <Form.Group>
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="password"/>       
            </Form.Group>
        </Form>
        <Link to='/activities'>
            <Button onClick={() => {setUser({name : "jakub", surname : "zielinski", id : 1})}}>Login</Button>    
        </Link>
    </div>
}

const EmployeeList = () => {
    const options = {}
    const {loading, error, data = []} = useFetch("https://localhost:5001/Identity", options, [])

    if (loading)
        return <div>
        </div>
    else
        if (error)
            return <Container>
                <h3>Server is down!</h3>
            </Container> 
        else
            return <Container>
                {data.map(emp => <Employee key={emp.id} emp={emp}/>)}
            </Container>
}

const Employee = (emp) => {
    const {name, surname, id} = emp.emp
    console.log(emp)
    return <Row>
        <Col>{id}</Col>
        <Col>{name}</Col>
        <Col>{surname}</Col>
    </Row>
}