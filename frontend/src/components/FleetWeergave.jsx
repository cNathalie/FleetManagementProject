/* eslint-disable no-unused-vars */
import React from "react";
import Button from "./Button";
import { useNavigate } from "react-router-dom";
import { BUTTON_STYLES, TEXT_STYLES } from "../constants/tailwindStyles";
/* eslint-disable react/prop-types*/

const FleetWeergave = (props) => {
  const {
    fleetweergave: { id, title, text, btnValue, imgSrc, alt },
  } = props;

  const navigate = useNavigate();

  let divs;

  divs = (
    <>
      <div className="w-full">
        <img

          className="rounded-1 object-cover h-full w-full"
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

  return <div className="shadow-lg rounded-md flex w-[45%]">{divs}</div>;
};

export default FleetWeergave;
