// eslint-disable-next-line no-unused-vars
import React from "react";
import FleetWeergave from "../components/FleetWeergave";
import homePage from "../constants/homePageContent";
import { useOutletContext } from "react-router-dom";

const Homepage = () => {
  const navBtnRef = useOutletContext();
  //console.log(navBtnRef.current);
  return (
    <>
      <div className="h-screen flex flex-wrap justify-around gap-y-7 px-12 pt-24 pb-8">
        {homePage.map((h) => {
          return (
            <FleetWeergave
              key={h.id}
              fleetweergave={{
                id: h.id,
                title: h.title,
                text: h.text,
                btnValue: h.btnValue,
                imgSrc: h.imgSrc,
              }}
              navBtnRef={navBtnRef}
            />
          );
        })}
      </div>
    </>
  );
};

export default Homepage;
