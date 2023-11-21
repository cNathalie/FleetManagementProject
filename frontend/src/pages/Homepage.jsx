import React from "react";
import FleetWeergave from "../components/FleetWeergave";
import homePage from "../constants/homePageContent";

const Homepage = () => {
  return (
    <>
      <div className="grid sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 gap-y-12 gap-x-10 px-12 pt-26">
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
