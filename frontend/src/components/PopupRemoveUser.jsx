// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";
import {
  BUTTON_STYLES,
  CARD_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";
import useConfirmation from "../confirmation/useConfirmation";
import { useDarkMode } from "../hooks/useDarkMode";
/* eslint-disable react/prop-types*/

const PopupRemoveUser = ({ popup, refRemoveUser, refOverlay }) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;

  const { setAdminDecision } = useConfirmation();
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
              // code om gebruiker te verwijderen, na het verwijderen popup en overlay display: none
              setAdminDecision(true);
              const popup = refRemoveUser.current;
              const overlay = refOverlay.current;
              popup.style.display = "none";
              overlay.style.display = "none";
            }}
          >
            {textBtnLeft}
          </Button>
          <Button
            className={`${BUTTON_STYLES.POPUP_RIGHT}`}
            onClick={() => {
              setAdminDecision(false);
              const popup = refRemoveUser.current;
              const overlay = refOverlay.current;
              popup.style.display = "none";
              overlay.style.display = "none";
            }}
          >
            {textBtnRight}
          </Button>
        </div>
      </div>
    </div>
  );
};

export default PopupRemoveUser;
