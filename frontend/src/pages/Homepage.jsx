import React from "react";
import FleetWeergave from "../components/FleetWeergave";
import homePage from "../constants/homePageContent";
import Nav from "../components/Nav";

const Homepage = () => {
  return (
    <>
      <Nav />
      <div className="grid sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 gap-y-12 gap-x-10 pl-12 pr-12 mt-30">
        {homePage.map((h) => {
          return (
            <FleetWeergave
              key={h.id}
              title={h.title}
              text={h.text}
              btnValue={h.btnValue}
              imgSrc={h.imgSrc}
              fleetweergave={h}
            />
          );
        })}
      </div>
    </>
  );
};

export default Homepage;
