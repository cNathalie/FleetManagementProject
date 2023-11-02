// eslint-disable-next-line no-unused-vars
import React from 'react'
import FleetWeergave from "../components/FleetWeergave"
import homePage from "../constants/homePageContent"
import Nav from "../components/Nav"

    
const Homepage = () => {
  return (
    <>
    <nav>
      <Nav/>
    </nav>
      {homePage.map((h) => {
        return <FleetWeergave key={h.id} title={h.title} text={h.text} btnValue={h.btnValue} imgSrc={h.imgSrc} fleetweergave={h}/>
      })}
    </> 
  )
}
    
export default Homepage;
