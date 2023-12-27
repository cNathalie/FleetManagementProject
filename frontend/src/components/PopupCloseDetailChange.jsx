// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";
import {
  BUTTON_STYLES,
  CARD_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";
import { useDarkMode } from "../hooks/useDarkMode";
/* eslint-disable react/prop-types*/

const PopupCloseDetailChange = ({
  popup,
  setPopupVisibility,
  refOverlay,
  refDetailChange,
  refGoBack,
}) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;
  const { isDarkMode, toggleDarkMode } = useDarkMode();

  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div className={`${CARD_STYLES.POPUP_CARD} w-[30%] h-[85%]`}>
      <div className="h-[50%] pl-4 pt-4">
        <h1 className={`${TEXT_STYLES.POPUP_TITLE}`}>{title}</h1>
        <p className={`${TEXT_STYLES.POPUP_TEXT}`}>{text}</p>
      </div>
      <div className="h-[50%] pl-4 mt-8 pb-4">
        <Button
          className={`${BUTTON_STYLES.POPUP_LEFT}`}
          onClick={() => {
            setPopupVisibility(refGoBack, false);
            setPopupVisibility(refOverlay, false);
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className={`${BUTTON_STYLES.POPUP_RIGHT}`}
          onClick={() => {
            setPopupVisibility(refGoBack, false);
            setPopupVisibility(refDetailChange, true);
          }}
        >
          {textBtnRight}
        </Button>
      </div>
    </div>
    </div>
    
  );
};

export default PopupCloseDetailChange;
