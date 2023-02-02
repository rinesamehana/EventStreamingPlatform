import axios from "axios";
import React from "react";
import { useState, useEffect } from "react";
import "./movies.css";
const Movies = () => {
    const[films, setFilms]=useState([])
    useEffect(()=>{


      
       axios.get('https://localhost:44337/api/films').then(res=>{
               console.log(res)
               setFilms(res.data.data)
       }).catch(err=>{
        console.log(err)
    })

       

       
},[])

console.log(films);
return (
    
    <>
 
   
 <div className="title">
        <h1>Our Towns</h1>
      </div>
      <div className="neighborhood">
        <div className="featuredd">
          {films.map((qyteti) => {
            return (
              <div className="featureddItem" key={qyteti.id}>
                {/* <img
                  src={qyteti.photo}
                  alt=""
                  className="featuredImg"
                /> */}
                <div className="featureddTitles">
                  <h1>{qyteti.title}</h1>
                  <h1>{qyteti.company.companyName}</h1>
                  <h1>{qyteti.language.name}</h1>
            
                </div>
              </div>
            );
          })}
        </div>
      </div>
    </>
  );
  }

    
    


export default Movies; 

