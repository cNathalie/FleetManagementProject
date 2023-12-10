// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";
import {
  BUTTON_STYLES,
  CARD_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";
/* eslint-disable react/prop-types*/

const PopupCloseDetailChange = ({ popup, setPopupVisibility }) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;

  return (
    <div className={`${CARD_STYLES.POPUP_CARD} w-[30%] h-[85%]`}>
      <div className="h-[50%] pl-4 pt-4">
        <h1 className={`${TEXT_STYLES.POPUP_TITLE}`}>{title}</h1>
        <p className={`${TEXT_STYLES.POPUP_TEXT}`}>{text}</p>
      </div>
      <div className="h-[50%] pl-4 mt-8">
        <Button
          className={`${BUTTON_STYLES.POPUP_LEFT}`}
          onClick={() => {
            setPopupVisibility("popupGoBack", false);
            setPopupVisibility("overlay", false);
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className={`${BUTTON_STYLES.POPUP_RIGHT}`}
          onClick={() => {
            setPopupVisibility("popupGoBack", false);
            setPopupVisibility("detailChange", true);
          }}
        >
          {textBtnRight}
        </Button>
      </div>
    </div>
  );
};

export default PopupCloseDetailChange;
