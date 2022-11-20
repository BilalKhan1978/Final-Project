import './App.css';
import {BrowserRouter,Route,Routes} from 'react-router-dom'
import ContactListing from './components/ContactListing';
import CreateContacts from './components/CreateContacts';
import EditContacts from './components/EditContacts';

function App() {
  return (
    <div className="App">    
    <BrowserRouter>
    <Routes>
      <Route path='/' element={<ContactListing/>}></Route>
      <Route path='/contacts/create' element={<CreateContacts/>}></Route>
      <Route path='/contacts/edit/:contactid' element={<EditContacts/>}></Route>
      
    </Routes>
  </BrowserRouter>
    </div>
  );
 //json-server --watch db.json --port 8000 
}


export default App;
