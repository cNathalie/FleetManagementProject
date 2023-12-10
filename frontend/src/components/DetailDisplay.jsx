// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";
import { TEXT_STYLES } from "../constants/tailwindStyles";

const DetailDisplay = ({ setPopupVisibility, tempObject }) => {
  console.log(tempObject);

  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB] ">
      <div>
        <div className="ml-9 mt-6 py-6 flex justify-between">
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

        <div className="items-center flex flex-wrap pb-8">
          {tempObject ? (
            Object.entries(tempObject).map(([key, value]) => (
              <>
                <div className="w-1/4 my-2 text-right">
                  <div key={key}>
                    {/* hier ook styling voor de text mag je zelf kiezen */}
                    <label htmlFor={key} className={``}>
                      {key}:
                    </label>
                  </div>
                </div>
                <div className="w-2/3 ml-12">
                  <div>
                    <p
                      id={key}
                      name={key}
                      // hier was de ${TEXT_STYLES.ADMIN_OR} misschien de kleuren veranderen da moet je zelf ik beijken hoe je die wilt heb nu dit snel copy paste gedaan
                      className={`font-mainFont font-semibold text-darkBlue text-[18px] text-left tracking-[0] leading-[normal] whitespace-nowrap w-[75%] ml-12`}
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
  );
};

export default DetailDisplay;
