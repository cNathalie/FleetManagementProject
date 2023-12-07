// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";
import { TEXT_STYLES } from "../constants/tailwindStyles";

const DetailDisplay = ({ setPopupVisibility, tempObject }) => {
  console.log(tempObject);

  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB] ">
      <div>
        <div className="ml-9 mt-6 pt-6 flex justify-between">
          <h1 className="font-mainFont font-titleFontWeigt text-4xl">
            Detailweergave
          </h1>
          <div className="mr-8">
            <Button
              className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt"
              onClick={() => {
                setPopupVisibility("overlay", false);
                setPopupVisibility("detailDisplay", false);
              }}
            >
              <img src="../src/assets/Media/closeButton.jpg" />
            </Button>
          </div>
        </div>

        <div className="items-center grid grid-cols-3">
          {tempObject ? (
            Object.entries(tempObject).map(([key, value]) => (
              <>
                <div key={key} className="col-span-1">
                  <label
                    htmlFor={key}
                    className={`${TEXT_STYLES.ADMIN_OR}`}
                  >
                    {key}:
                  </label>
                </div>
                <div className="col-span-2">
                  <p
                    id={key}
                    name={key}
                    className={`${TEXT_STYLES.ADMIN_OR} w-[75%]`}
                  >
                    {typeof value === "boolean"
                      ? value
                        ? "vrij"
                        : "bezet"
                      : value}
                  </p>
                </div>
              </>
            ))
          ) : (
            <p>Error: tempVoertuigContent is null or undefined</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default DetailDisplay;
