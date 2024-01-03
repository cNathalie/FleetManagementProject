import React from "react";
import Button from "./Button";
import { BG_STYLES, BUTTON_STYLES, CARD_STYLES } from "../constants/tailwindStyles";
import { useDarkMode } from "../hooks/useDarkMode";

const PopupMessage = ({ msgRef, msg }) => {
  const hidePopups = (ref) => {
    const popup = ref.current;
    popup.style.display = "none";
  };
  const { isDarkMode, toggleDarkMode } = useDarkMode();

  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div className={`absolute right-0 flex items-center ${CARD_STYLES.POPUP_CARD} py-2 px-4 mr-4 mt-6`}>
      <div className="mr-10">
        <p>{msg}</p>
      </div>
      <Button
        className={`${BUTTON_STYLES.OVERVIEW_EXITBUTTON}`}
        type="button"
        onClick={() => {
          hidePopups(msgRef);
        }}
      >
        <img src={isDarkMode ? "../src/assets/Media/dark_closeBtn.png" : "../src/assets/Media/closeButton.jpg"} alt="Close" />
      </Button>
    </div>
    </div>
    
  );
};

export default PopupMessage;
