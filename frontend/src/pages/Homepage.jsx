// eslint-disable-next-line no-unused-vars
import React from "react";
import FleetWeergave from "../components/FleetWeergave";
import homePage from "../constants/homePageContent";

const Homepage = () => {
  return (
    <>
      <div className="h-screen flex flex-wrap justify-around gap-y-7 px-12 pt-24 pb-8">
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
