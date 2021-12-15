import './App.css';
import {Navigation} from './Navbar.js'
import {Login} from './Components/Identity/Login.js'
import 'bootstrap/dist/css/bootstrap.min.css'
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom'
import {Activities} from './Components/Activity/Activities'
import {Projects} from './Components/Project/Projects'
import {Reports} from './Components/Report/Reports'
import {Logout} from './Components/Identity/Logout'
import AuthContext from './Components/Identity/AuthContext'; 
import { useState } from 'react';

function App() {
  
  const [user, setUser] = useState(null)
    
  return (
    <div className="App">
      <AuthContext.Provider value={{user, setUser}}>
        <Router>
          <Navigation/>
          <Routes>
            <Route path="/login" element={<Login/>}/>
            <Route path="/logout" element={<Logout/>}/>
            <Route path="/projects" element={<Projects/>}/>
            <Route path="/activities" element={<Activities/>}/>
            <Route path="/reports" element={<Reports/>}/>
          </Routes>
        </Router>
      </AuthContext.Provider>
    </div>  
  );
}

export default App;
