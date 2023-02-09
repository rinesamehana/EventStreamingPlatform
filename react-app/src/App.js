import "./App.css"
import HomePage from "./home/HomePage"
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"

import Header from "./components/header/Header"
import Footer from "./components/footer/Footer"
import Movies from "./components/moviespage/Movies"
import Series from "./components/seriespage/Series"



function App() {
  return (
    <>
      <Router>
        <Header />
        <Switch>
          <Route exact path='/' component={HomePage} />
        
          <Route path='/Movies' component={Movies} />
          <Route path='/Series' component={Series} />
          
        </Switch>
        <Footer />
      </Router>
    </>
  )
}

export default App
