import axios from "axios";
import React from "react";
import { data } from "../../dummyData";
import { useState, useEffect } from "react";
import "./series.css";
import { Link } from "react-router-dom";
import PopupPage from "./PopupPage";
import "./popupPage.css";
import { BsChevronDown } from "react-icons/bs";
const Movies = () => {
  const [selected, setSelected] = useState(null);

  const [isOpen, setOpen] = useState(false);
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

  const toggler = (id) => {
    if (selected === id) {
      return setSelected(null);
    }
    setSelected(id);
  };

  return (
    <>
      <>
        <section class="top-rated">
          <div class="container">
            <br></br>

            <h2 class="h2 section-title">Our Series</h2>
            <ul class="movies-list">
              {loading && (
                <div className="loading">
                  <img className="rotate" />
                  <h2>Loading...</h2>
                </div>
              )}
              {series.map((film) => {
                return (
                  <>
                    <li key={film.serieId}>
                      <div class="movie-card">
                        <a href={film.videoLink}>
                          <figure class="card-banner">
                            <img src={film.photoLink} alt="Picture Here" />
                          </figure>

                          <div class="title-wrapper">
                            <h3 class="card-title">{film.title}</h3>
                            <p>
                              {film.serieGenres.map((d) => {
                                return <p>{d.genre.name}</p>;
                              })}
                            </p>
                            {/* <time datetime="2021">{film.realiseDate}</time> */}
                          </div>

                          <div class="movie-over">
                            <div class="duration">
                              <div className="container">
                                <div className="accordion">
                                  {film.seasons.map((item, index) => (
                                    <div key={item.seasonId} className="item">
                                      <div
                                        className="accordion-header"
                                        onClick={() => toggler(index)}
                                      >
                                        <h3>{item.name}</h3>

                                        <div>
                                          {selected === index ? (
                                            <BsChevronDown className="rotate-down" />
                                          ) : (
                                            <BsChevronDown className="rotate-up" />
                                          )}
                                        </div>
                                      </div>

                                      <div
                                        className={
                                          selected === index
                                            ? "content show"
                                            : "contentt"
                                        }
                                      >
                                        {" "}
                                        <p>Episodes: </p>
                                        {item.episode.map((ite, index) => (
                                          <a href={ite.videoLink}>
                                            <div
                                              key={item.seasonId}
                                              className="item"
                                            >
                                              <div
                                                className="accordion-header"
                                                onClick={() => toggler(index)}
                                              >
                                                <h3>{ite.name}</h3>

                                                <p>Watch Now</p>

                                                {/* <div>
                                              {selected === index ? (
                                                <BsChevronDown className="rotate-down" />
                                              ) : (
                                                <BsChevronDown className="rotate-up" />
                                              )}
                                            </div> */}
                                              </div>
                                              <div
                                                className={
                                                  selected === index
                                                    ? "content show"
                                                    : "contentt"
                                                }
                                              >
                                                {item.episode.name}
                                              </div>
                                            </div>{" "}
                                          </a>
                                        ))}
                                      </div>
                                    </div>
                                  ))}
                                </div>
                              </div>

                              <ion-icon name="time-outline"></ion-icon>
                            </div>
                          </div>
                        </a>
                      </div>
                    </li>
                  </>
                );
              })}
            </ul>
          </div>
        </section>
      </>
    </>
  );
};

export default Movies;
