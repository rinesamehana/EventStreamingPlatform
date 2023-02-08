import React from "react";

const PopupPage =({onClick}) => {

    return (
        <>
        <p>Sezonat e filmave</p>
        <div className="newContainer  modal-content">   
        <button className="close-modal" onClick={onClick}>&times;</button> 
        <div className="new">
          <div className="newContainer">
            <div className="top">
              <h1>Tekst tekst</h1>
              <h1>Tekst tekst</h1>
              <h1>Tekst tekst</h1>
              <h1>Tekst tekst</h1>
              <h1>Tekst tekst</h1>
              <h1>Tekst tekst</h1>
            </div>
            </div>
            </div>
            </div>    
        </>
    )
}

export default PopupPage;