import React, { useState, useEffect } from "react"
import "./style.css"
import { useParams } from "react-router-dom"
import { homeData, recommended } from "../../dummyData"
import Upcomming from "../upcoming/Upcomming"
import axios from "axios";
const SinglePage = ({match}) => {
  const { id } = useParams()
  const [film, setFilm] = useState([]);
  useEffect(() => {
    axios
      .get(`https://localhost:44337/api/films/${match.params.id}`)
      .then((res) => {
        console.log(res);
        setFilm(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  }, []);
 
  const [rec, setRec] = useState(recommended)
  console.log(film)
  return (
    <>
      {film ? (
        <>
          <section className='singlePage'>
            <div className='singleHeading'>
              <h1>{film.title} </h1> <span> | {film.duration} | </span> <span> HD </span>
            </div>
            <div className='container'>
              {/* <video src={item.video} controls></video> */}
              <div className='para'>
                {/* <h3>Date : {item.date}</h3> */}
                {/* <p>{item.desc}</p> */}
                <p>{film.description}</p>
                <p>Get access to the direct Project Overview with this report. Just by a quick glance, you’ll have an idea of the total tasks allotted to the team, number of milestones given & completed, total Links created for the project and the total time taken by all the users. Each section would elaborate the data further, if chosen.</p>
                <p>Get access to the direct Project Overview with this report. Just by a quick glance, you’ll have an idea of the total tasks allotted to the team, number of milestones given & completed, total Links created for the project and the total time taken by all the users. Each section would elaborate the data further, if chosen.</p>
              </div>
              <div className='soical'>
                <h3>Share : </h3>
                <img src='https://img.icons8.com/color/48/000000/facebook-new.png' />
                <img src='https://img.icons8.com/fluency/48/000000/twitter-circled.png' />
                <img src='https://img.icons8.com/fluency/48/000000/linkedin-circled.png' />
              </div>
            </div>
          </section>
          {/* <Upcomming items={rec} title='Recommended Movies' /> */}
        </>
      ) : (
        "no"
      )}
    </>
  )
}

export default SinglePage
