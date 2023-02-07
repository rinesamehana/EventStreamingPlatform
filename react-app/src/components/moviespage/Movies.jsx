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
 
   
 <section class="top-rated">
        <div class="container">

         <br></br>

          <h2 class="h2 section-title">Our Movies</h2>

          <ul class="movies-list">

           
            <li>
              <div class="movie-card">

                <a href="./singlepage/1">
                  <figure class="card-banner">
                    <img src="https://assets-in.bmscdn.com/iedb/movies/images/mobile/thumbnail/xlarge/titanic-et00008457-03-12-2017-06-34-18.jpg" alt="The King's Man movie poster"/>
                  </figure>
                </a>

                <div class="title-wrapper">
                  <a href="./singlepage/1">
                    <h3 class="card-title">The King's Man</h3>
                  </a>

                  <time datetime="2021">2021</time>
                </div>

                <div class="card-meta">
                  <div class="badge badge-outline">HD</div>

                  <div class="duration">
                    <ion-icon name="time-outline"></ion-icon>

                    <time datetime="PT131M">131 min</time>
                  </div>

                  <div class="rating">
                    <ion-icon name="star"></ion-icon>

                    <data>7.0</data>
                  </div>
                </div>

              </div>
            </li>

          </ul>

        </div>
      </section>
    </>
  );
  }

    
    


export default Movies; 

