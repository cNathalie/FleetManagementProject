import React from "react";
import Button from "./Button";
/* eslint-disable react/prop-types*/

const PopupCloseDetailChange = ({ popup, setPopupVisibility }) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;

  return (
    <div className="w-[30%] h-[85%] bg-[#DBDBDB] rounded-xl">
      <div className="h-[50%] pl-4 pt-4">
        <h1 className="font-mainFont font-titleFontWeigt text-titleFontSize">
          {title}
        </h1>
        <p className="font-mainFont font-titleFontWeigt text-popupTextSize pt-2 text-[#858585]">
          {text}
        </p>
      </div>
      <div className="h-[50%] pl-4 mt-8">
        <Button
          className="bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn"
          onClick={() => {
            setPopupVisibility("popupGoBack", false);
            setPopupVisibility("detailChange", true);
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className="bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn ml-6"
          onClick={() => {
            setPopupVisibility("popupGoBack", false);
            setPopupVisibility("overlay", false);
          }}
        >
          {textBtnRight}
        </Button>
      </div>
    </div>
  );
};

export default PopupCloseDetailChange;
