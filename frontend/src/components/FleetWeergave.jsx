/* eslint-disable no-unused-vars */
import React from "react";
import Button from "./Button";
import { useNavigate } from "react-router-dom";
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
          className="rounded-l object-cover h-full w-full"
          src={imgSrc}
          alt={alt}
        />
      </div>
      <div className="w-[150%]">
        <h1 className="font-titleFontWeigt text-[32px] font-Helvetica text-center h-[15%]">
          {title}
        </h1>
        <p className="font-semibold text-[20px] font-Helvetica text-center font-titleFontWeigt h-[60%] flex items-center p-5">
          {text}
        </p>
        <div className="flex items-center h-[25%] justify-center">
          <Button
            className="w-3/4 h-[53px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-white bg-blueBtn hover:bg-hoverBtn cursor-pointer"
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
