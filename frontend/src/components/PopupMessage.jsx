import React from "react";
import Button from "./Button";
import { BUTTON_STYLES } from "../constants/tailwindStyles";

const PopupMessage = (props, { msgRef }) => {
  const hidePopups = (ref) => {
    const popup = ref.current;
    popup.style.display = "none";
  };

  return (
    <div className="absolute right-0 flex items-center bg-[#DBDBDB] rounded-md py-2 px-4 mr-4 mt-6">
      <div className="mr-10">
        <p>{props.children}</p>
      </div>
      <Button
        className={`${BUTTON_STYLES.OVERVIEW_EXITBUTTON}`}
        type="button"
        onClick={() => {
          hidePopups(msgRef);
        }}
      >
        <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
      </Button>
    </div>
  );
};

export default PopupMessage;
