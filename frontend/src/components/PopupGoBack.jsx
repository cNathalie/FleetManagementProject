import React from "react";
import Button from "./Button";
import { useNavigate } from "react-router-dom";
import {
  BG_STYLES,
  BUTTON_STYLES,
  CARD_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";
/* eslint-disable react/prop-types*/

const PopupGoBack = ({ popup, refGoBack, refOverlay }) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;

  const navigate = useNavigate();

  return (
<<<<<<< Updated upstream
    <div className={`${CARD_STYLES.POPUP_CARD} w-[30%] h-[85%]`}>
      <div className="h-[50%] pl-4 pt-4">
        <h1 className={`${TEXT_STYLES.POPUP_TITLE}`}>{title}</h1>
        <p className={`${TEXT_STYLES.POPUP_TEXT}`}>{text}</p>
      </div>
      <div className="h-[50%] pl-4 mt-8">
        <Button
          className={`${BUTTON_STYLES.POPUP_LEFT}`}
          onClick={() => {
            navigate("/home");
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className={`${BUTTON_STYLES.POPUP_RIGHT}`}
          onClick={() => {
            const popup = document.getElementById("popupGoBack");
            const overlay = document.getElementById("overlay");
            popup.style.display = "none";
            overlay.style.display = "none";
          }}
        >
          {textBtnRight}
        </Button>
=======
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
              navigate("/home");
            }}
          >
            {textBtnLeft}
          </Button>
          <Button
            className={`${BUTTON_STYLES.POPUP_RIGHT}`}
            onClick={() => {
              const popup = refGoBack.current;
              const overlay = refOverlay.current;
              popup.style.display = "none";
              overlay.style.display = "none";
            }}
          >
            {textBtnRight}
          </Button>
        </div>
>>>>>>> Stashed changes
      </div>
    </div>
  );
};

export default PopupGoBack;
