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
        <h1>Our Movies</h1>
      </div>
      <div className="movies">
        <div className="movie">
          {films.map((filmi) => {
            return (
              <div className="movieItem" key={filmi.id}>
                {/* <img
                  src={filmi.photo}
                  alt=""
                  className="featuredImg"
                /> */}
                <div className="movieTitles">
                  <h1>{filmi.title}</h1>
                  <h1>{filmi.company.companyName}</h1>
                  <h1>{filmi.language.name}</h1>
            
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

