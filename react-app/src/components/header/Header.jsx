import React, { useState } from "react";
import "./header.css";

const Header = () => {
  const [Mobile, setMobile] = useState(false);
  return (
    <>
      <header>
        <div className="container flexSB">
          <nav className="flexSB">
            <div className="logo">
              <img src="" alt="" />
            </div>
            {/*<ul className='flexSB'>*/}
            <ul
              className={Mobile ? "navMenu-list" : "flexSB"}
              onClick={() => setMobile(false)}
            >
              <li>
                <a href="/">Home</a>
              </li>
              <li>
                <a href="/series">Series</a>
              </li>
              <li>
                <a href="/movies">Movies</a>
              </li>
            </ul>
            <button className="toggle" onClick={() => setMobile(!Mobile)}>
              {Mobile ? (
                <i className="fa fa-times"></i>
              ) : (
                <i className="fa fa-bars"></i>
              )}
            </button>
          </nav>
          {/* <div className='account flexSB'>
           
           
           <a href={`https://localhost:44337/Identity/Account/Login`}>
            <button>Log in</button>
            </a>


            <a href={`https://localhost:44337/Identity/Account/Register`}>
            <button>Sign in</button>
            </a>
          </div> */}
        </div>
      </header>
    </>
  );
};

export default Header;
