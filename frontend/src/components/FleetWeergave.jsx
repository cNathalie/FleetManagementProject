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
      <div className="h-[50%]">
        <img
          className="rounded-md object-cover h-full w-full"
          src={imgSrc}
          alt={alt}
        />
      </div>
      <div>
        <h1 className="font-titleFontWeigt text-[32px] font-Helvetica text-center mt-4">
          {title}
        </h1>
        <p className="mt-[5%] font-semibold text-[20px] font-Helvetica text-center font-titleFontWeigt">
          {text}
        </p>
        <Button
          className="w-1/2 h-[53px] rounded-[10px] mt-[10%] ml-[25%] font-btnFontWeigt font-Helvetica text-btnFontSize text-[#FFFFFF] bg-blueBtn hover:bg-hoverBtn cursor-pointer"
          onClick={() => {
            navigate(`/items/${id}`);
          }}
        >
          {btnValue}
        </Button>
      </div>
    </>
  );

  return <div className="shadow-lg rounded-md h-[100%]">{divs}</div>;
};

export default FleetWeergave;
