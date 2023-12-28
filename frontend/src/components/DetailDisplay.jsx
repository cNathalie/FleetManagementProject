// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";
import { BG_STYLES, BUTTON_STYLES, TEXT_STYLES } from "../constants/tailwindStyles";
import { useDarkMode } from "../hooks/useDarkMode";

const DetailDisplay = ({
  setPopupVisibility,
  tempObject,
  refOverlay,
  refDetailDisplay,
}) => {
  console.log(tempObject);

  const { isDarkMode, toggleDarkMode } = useDarkMode();
  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div className={BG_STYLES.OVERVIEW_BG}>
      <div>
        <div className="ml-9 mt-6 py-6 flex justify-between">
          <h1 className={TEXT_STYLES.OVERVIEW_TITLE}>Detailweergave</h1>
          <div className="mr-8">
            <Button
              className={BUTTON_STYLES.OVERVIEW_EXITBUTTON}
              onClick={() => {
                setPopupVisibility(refOverlay, false);
                setPopupVisibility(refDetailDisplay, false);
              }}
            >
              <img src= {`${isDarkMode ? "../src/assets/Media/dark_closeBtn.png" : "../src/assets/Media/closeButton.jpg"}`} />
            </Button>
          </div>
        </div>

        <div className="items-center flex flex-wrap pb-8">
          {tempObject ? (
            Object.entries(tempObject).map(([key, value]) => (
              <>
                <div className="w-1/4 my-2 text-left ml-9">
                  <div key={key}>
                    <label
                      htmlFor={key}
                      className={`${TEXT_STYLES.OVERVIEW_DATAHEADER}`}
                    >
                      {key}
                    </label>
                  </div>
                </div>
                <div className="w-2/4">
                  <div>
                    <p
                      id={key}
                      name={key}
                      className={`${TEXT_STYLES.OVERVIEW_DATAVALUE}`}
                    >
                      {typeof value === "boolean"
                        ? value
                          ? "vrij"
                          : "bezet"
                        : value}
                    </p>
                  </div>
                </div>
              </>
            ))
          ) : (
            <p>Error: tempVoertuigContent is null or undefined</p>
          )}
        </div>
      </div>
    </div>
    </div>
    
  );
};

export default DetailDisplay;
