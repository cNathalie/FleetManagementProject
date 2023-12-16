/* eslint-disable no-unused-vars */
import React from "react";
import Button from "./Button";
import { useNavigate } from "react-router-dom";
import { BUTTON_STYLES, TEXT_STYLES } from "../constants/tailwindStyles";
/* eslint-disable react/prop-types*/
import { useDarkMode } from "../hooks/useDarkMode";

const FleetWeergave = (props) => {
  const {
    fleetweergave: { id, title, text, btnValue, imgSrc, alt },
  } = props;

  const navigate = useNavigate();
  const { isDarkMode, toggleDarkMode } = useDarkMode();

  let divs;

  divs = (
    <>
      <div className="w-full">
        <img

          className="rounded-l-md object-cover h-full w-full"
          src={imgSrc}
          alt={alt}
        />
      </div>
      <div className="w-[150%]">

        <h1 className={TEXT_STYLES.HOMEPAGE_CARDTITLE}>
          {title}
        </h1>
        <p className={TEXT_STYLES.HOMEPAGE_CARDTEXT}>
          {text}
        </p>
        <div className="flex items-center h-[25%] justify-center">
          <Button

            className={BUTTON_STYLES.HOMEPAGE_CARDBUTTON}

            onClick={() => {
              navigate(`/items/${id}`);
            }}
          >
            {btnValue}
          </Button>
        </div>
      </div>
    </>
  );

  return <div className={`shadow-lg rounded-md flex w-[45%] ${isDarkMode ? "dark bg-darkBlue2" : "bg-white"}`}>{divs}</div>;
};

export default FleetWeergave;
