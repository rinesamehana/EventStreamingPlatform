import axios from "axios";
import React from "react";
import { data } from "../../dummyData";
import { useState, useEffect } from "react";
import "./series.css";
import { Link } from "react-router-dom";

import "./popupPage.css";
import { BsChevronDown } from "react-icons/bs";
const PopupPage = ({ onClick }) => {
  const [selected, setSelected] = useState(null);

  const [modal, setModal] = useState(false);
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
  const togglePopup = () => {
    setModal(!modal);
  };
  if (modal) {
    document.body.classList.add("active");
  } else {
    document.body.classList.remove("active");
  }
  const toggler = (id) => {
    if (selected === id) {
      return setSelected(null);
    }
    setSelected(id);
  };
  return (
    <>
      {series.map((film) => {
        return (
          <>
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
                        selected === index ? "content show" : "contentt"
                      }
                    >
                      {" "}
                      <p>Episodes: </p>
                      {item.episode.map((ite, index) => (
                        <a href={ite.videoLink}>
                          <div key={item.seasonId} className="item">
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
                                selected === index ? "content show" : "contentt"
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
          </>
        );
      })}
    </>
  );
};

export default PopupPage;
