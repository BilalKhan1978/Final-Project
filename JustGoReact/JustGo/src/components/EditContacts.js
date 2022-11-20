import { useEffect, useState } from "react";
import { Link, useParams,useNavigate } from "react-router-dom";

// call two methods one is Get by Id
// second method is for updating a contact. PUT method

const EditContacts = () =>{
    const { contactid } = useParams();

    useEffect(() => {
      //api get by id endpoint url   
      fetch("https://localhost:7073/api/Contacts/" + contactid).then((res) => {
            return res.json();
        }).then((resp) => {          
          fullNameChange(resp.fullName)
          emailChange(resp.email)
          phoneChange(resp.phone)
          addressChange(resp.address)

        }).catch((err) => {
            console.log(err.message);
        })
    }, [contactid]);


    const [fullName,fullNameChange]= useState("");
    const [email,emailChange]= useState("");
    const [phone,phoneChange]= useState("");
    const [address,addressChange]= useState("");

    const navigate=useNavigate();

    const handelsubmit = (e) =>{
        e.preventDefault();
      const contactdata={fullName,email,phone,address};
      // Api Put endpoint url 
      fetch("https://localhost:7073/api/Contacts/"+contactid,{
        method:"PUT",
        headers:{"content-type":"application/json"},
        body:JSON.stringify(contactdata)
      }).then((res)=>{
        alert('Saved successfully.')
        navigate('/');
      }).catch((err)=>{
        console.log(err.message)
      })
    }


    return(
        <div>
        
                <div>
                    <div>
                        <h2>Edit Contact</h2>
                    </div>
                    <form onSubmit={handelsubmit}>
                  
                        <div>
                        <label>Full Name</label>
                        </div>
                        <div>
                        <input value={fullName} onChange={e=>fullNameChange(e.target.value)}></input>
                        </div>
                        <div>
                        <label>Email</label>
                        </div>
                        <div>
                        <input value={email} onChange={e=>emailChange(e.target.value)}></input>
                        </div>
                        <div>
                        <label>Phone</label>
                        </div>
                        <div>
                        <input value={phone} onChange={e=>phoneChange(e.target.value)}></input>
                        </div>
                        <div>
                        <label>Address</label>
                        </div>
                        <div>
                        <input value={address} onChange={e=>addressChange(e.target.value)}></input>
                        </div>
                        <br/>
                        <div>
                            <button type="submit">Save</button>
                            <Link to="/">Back</Link>
                        </div>                        
                    </form>
                    <div>

                    </div>
                </div>
            

    </div>
    )
    }
    
    export default EditContacts;