import React from "react";
import FleetWeergave from "../components/FleetWeergave";
import homePage from "../constants/homePageContent";

const Homepage = () => {
  return (
    <>
      <div className="grid sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 pl-12 pr-12 mt-20 h-[600px]">
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
