// eslint-disable-next-line no-unused-vars
import React from "react";
import { BUTTON_STYLES, TEXT_STYLES } from "../constants/tailwindStyles";
import { useDarkMode } from "../hooks/useDarkMode";
import { Icon, IconButton } from "@mui/material";
import { LightModeOutlined, DarkModeOutlined } from "@mui/icons-material";

const AdminHeader = () => {
  const { isDarkMode, toggleDarkMode } = useDarkMode();
  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div className="w-[100%] h-[100px] flex justify-center">
      {/* Header */}
      <div className="flex justify-between my-14 w-[786px] h-[43px]">
        <button
          className={`${BUTTON_STYLES.ADMIN_GOBACK} relative w-[201px] h-[43px]`}
          onClick={() => {
            const popup = document.getElementById("popupGoBack");
            const overlay = document.getElementById("overlay");
            popup.style.display = "block";
            overlay.style.display = "block";
          }}
        >
          <p>Terug naar homepagina</p>
        </button>
        <IconButton
                onClick={() => {
                  toggleDarkMode();
                }}
              >
                {isDarkMode ? (
                  <LightModeOutlined style={{ fill: "white"}} />
                ) : (
                  <DarkModeOutlined style={{ fill: "#0B5A64"}} />
                )}
        </IconButton>
        <div className={TEXT_STYLES.ADMIN_TITLE}>Administratie</div>
      </div>
    </div>
    </div>
    
  );
};

export default AdminHeader;
