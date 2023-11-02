/* eslint-disable no-unused-vars */
import React from "react";

const FleetWeergave = (props) => {
  
  /* eslint-disable react/prop-types*/ 
  const { fleetweergave: {id, title, text, btnValue, imgSrc, alt} } = props;
  
  let div

  if (id % 2 !== 0 ) {
    div = <>
    <div className="w-1/2 mt-[68px]">
      <div>
        <h1 className="ml-[25%] font-semibold text-[64px] font-Helvetica">{title}</h1>
      </div>
      <div>
        <p className="mt-[10%] ml-[25%] font-semibold text-[21px] font-Helvetica">
          {text}
        </p>
      </div>
      <div>
        <button className="w-[235px] h-[53px] rounded-[10px] mt-[10%] ml-[25%] font-semibold font-Helvetica text-[#FFFFFF] bg-blueBtn">
          {btnValue}</button>
      </div>
    </div>
    <div className="w-[458px] h-[569px] ml-[12%]">
      <img src={imgSrc} alt={alt} className="object-cover h-full w-full rounded-[10px]" />
    </div>
    </>;
  } else {
    div = <>
    <div className="w-[458px] h-[569px] ml-[12%]">
      <img src={imgSrc} alt={alt} className="object-cover h-full w-full rounded-[10px]" />
    </div>
    <div className="w-1/2 mt-[68px]">
      <div>
        <h1 className="ml-[25%] font-semibold text-[64px] font-Helvetica">{title}</h1>
      </div>
      <div>
        <p className="mt-[10%] ml-[25%] font-semibold text-[21px] font-Helvetica">
          {text}
        </p>
      </div>
      <div>
        <button className="w-[235px] h-[53px] rounded-[10px] mt-[10%] ml-[25%] font-semibold font-Helvetica text-[#FFFFFF] bg-blueBtn">
          {btnValue}</button>
      </div>
    </div>
    </>;
  }

  return (
    <div>
      <div className="flex flex-wrap p-5">
      {div}
    </div>
    </div>
  );
};

export default FleetWeergave;
