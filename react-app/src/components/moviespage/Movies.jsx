import axios from "axios";
import React from "react";
import { useState, useEffect } from "react";
import "./movies.css";
import { Link } from "react-router-dom";
const Movies = () => {
  const [films, setFilms] = useState([]);
  const [loading, setLoading]=useState(true);
  useEffect(() => {
    axios
      .get("https://localhost:44337/api/films")
      .then((res) => {
        console.log(res);
        setFilms(res.data.data);
      }).then((res)=>setLoading(false))
      .catch((err) => {
        console.log(err);
      });
  }, []);

  console.log(films);
  
  return (
    <>
      <section class="top-rated">
        <div class="container">
          <br></br>
         
          <h2 class="h2 section-title">Our Movies</h2>
          <ul class="movies-list">
          {loading && <div className="loading"><img className="rotate"/>
             <h2>Loading...</h2></div>}
            {films.map((film) => {
              return (
                
              <li key={film.id}>
                <div class="movie-card">
                <Link to={`/singlepage/${film.id}`}>
                    <figure class="card-banner">
                      <img
                        src={film.photoLink}
                        alt="Picture Here"
                      />
                    </figure>
               
                  </Link>
                  <div class="title-wrapper">
                    <a href="./singlepage/1">
                      <h3 class="card-title">{film.title}</h3>
                     
                    </a>

                    <time datetime="2021">{film.realiseDate}</time>
                  </div>
                   
                  <div class="card-meta">
                    <div class="badge badge-outline">HD 
                    
                    </div>
                    <p>{film.filmGenres.map((d)=>{
                      return (
                        <p>{d.genre.name}</p>
            )})}</p>
                    <div class="duration">
                      <ion-icon name="time-outline"></ion-icon>

                      <time datetime="PT131M">🎬{film.duration}</time>
                    </div>

                    <div class="rating">
                      <ion-icon name="star"></ion-icon>

                      <data>🌟{film.rating}</data>
                    </div>
                  </div>
                </div>
              </li>);
            })}
          </ul>
        </div>
      </section>
    </>
  );
};

export default Movies;
