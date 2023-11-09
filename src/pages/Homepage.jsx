
import React from "react";
import FleetWeergave from "../components/FleetWeergave";
import homePage from "../constants/homePageContent";
import Nav from "../components/Nav";

const Homepage = () => {
  return (
    <>
      <Nav />
      <div className="mx-auto max-w-screen-xl">
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
      </div>
    </>
  );
};

export default Homepage;

