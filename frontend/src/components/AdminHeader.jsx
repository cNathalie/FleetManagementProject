// eslint-disable-next-line no-unused-vars
import React from "react";
import { BUTTON_STYLES, TEXT_STYLES } from "../constants/tailwindStyles";

const AdminHeader = ({ refOverlay, refGoBack }) => {
  const showPopup = (overlayRef, popupRef) => {
    const popup = popupRef.current;
    const overlay = overlayRef.current;
    popup.style.display = "block";
    overlay.style.display = "block";
  };

  return (
    <div className="w-[100%] h-[100px] flex justify-center">
      {/* Header */}
      <div className="flex justify-between my-14 w-[786px] h-[43px]">
        <button
          className={`${BUTTON_STYLES.ADMIN_GOBACK} relative w-[201px] h-[43px]`}
          onClick={() => {
            showPopup(refOverlay, refGoBack);
          }}
        >
          <p>Terug naar homepagina</p>
        </button>
        <div className={TEXT_STYLES.ADMIN_TITLE}>Administratie</div>
      </div>
    </div>
  );
};

export default AdminHeader;
