import axios from "axios";
import React from "react";
import { useState, useEffect } from "react";
import "../moviespage/movies.css";
import { Link } from "react-router-dom";
const Movies = () => {
   const [modal , setModal] = useState(false);
  const [series, setFilms] = useState([]);
  const [loading, setLoading] = useState(true);
  useEffect(() => {
    axios
      .get("https://localhost:44337/api/series")
      .then((res) => {
        console.log(res);
        setFilms(res.data.data);
      })
      .then((res) => setLoading(false))
      .catch((err) => {
        console.log(err);
      });
  }, []);
  const togglePopup = () =>{
    setModal(!modal)
}
  if(modal){
  document.body.classList.add("active")
  }else{
  document.body.classList.remove("active")
  }
  console.log(series);

  return (
    <>
      <section class="top-rated">
        <div class="container">
          <br></br>

          <h2 class="h2 section-title">Our Movies</h2>
          <ul class="movies-list">
            {loading && (
              <div className="loading">
                <img className="rotate" />
                <h2>Loading...</h2>
              </div>
            )}
            {series.map((film) => {
              return (
                <li key={film.serieId}>
                  <div class="movie-card">
                    <a href={film.videoLink}>
                      <figure class="card-banner">
                        <img src={film.photoLink} alt="Picture Here" />
                      </figure>

                      <div class="title-wrapper">
                        <h3 class="card-title">{film.title}</h3>
                       
                        {/* <time datetime="2021">{film.realiseDate}</time> */}
                        
                      </div>

                      <div class="card-meta">
                        <p>
                          {film.serieGenres.map((d) => {
                            return <p>{d.genre.name}</p>;
                          })}
                        </p>
                        <div class="duration">
                          <ion-icon name="time-outline"></ion-icon>
                <div class="card-meta">
                  {/* <button  onClick={togglePopup} type={"submit"} class="badge badge-outline button-popup">See more</button> */}
                  {/* <div class="badge badge-outline">See more</div> */}
                          {/* <time datetime="PT131M">🎬{film.duration}</time> */}
                        </div>
                        </div>
                        <div class="rating">
                          <ion-icon name="star"></ion-icon>

                          {/* <data>🌟{film.rating}</data> */}
                        </div>
                      </div>
                      <div className="movie-over">
                        <h2>Overview</h2>
                        <p>{film.description}</p>
                        <p>
                          {film.serieActors.map((d) => {
                            return <p>🎭Main Actors: {d.actor.name}</p>;
                          })}
                        </p>
                        <p>
                          {film.serieActors.map((d) => {
                            return <p>🎭Other Actors: {d.actor.name}</p>;
                          })}
                        </p>
                        {/* <p>🏭{film.company.companyName}</p> */}
                        <p>🏭{film.director}</p>
                      </div>
                    </a>
                  </div>
                </li>
              );
            })}
          </ul>
        </div>
      </section>
    </>
  );
};

export default Movies;
