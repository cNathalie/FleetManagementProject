/* eslint-disable react/prop-types */
/* eslint-disable react/jsx-key */
/* eslint-disable no-unused-vars */
import React, { useState } from "react";
import Button from "./Button";
import CheckNoBg from "../assets/Media/CheckNoBg.png";

const DetailChange = ({ setPopupVisibility, UpdateVoertuig, tempObject }) => {
  const [data, setData] = useState(tempObject);
  const [isEditing, setIsEditing] = useState(false);
  const [showCheckMark, setShowSetMark] = useState(false);

  const handleDataChange = (field, value) => {
    setData((prevData) => ({
      ...prevData,
      [field]: value,
    }));
  };

  /**
   * The function `handleToggleEditMode` toggles between edit and display mode, saves data to the
   * database if in edit mode, and shows a checkmark to indicate success.
   */
  const handleToggleEditMode = () => {
    if (isEditing) {
      // Handle logic to save data to DB
      try {
        const updatedData = { ...tempObject, ...data };
        UpdateVoertuig(updatedData);
      } catch (error) {
        console.error("Error during PUT request:", error.message);
      }
      //Show checkmark to show success of actions
      setShowSetMark(true);
      //Timeout for checkmark so it doesn't stay on the screen endlessly
      setTimeout(() => {
        setShowSetMark(false);
      }, 3000);
    }
    setIsEditing(!isEditing); // Toggle between edit and display mode
  };

  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB]">
      <div>
        <div className="ml-9 mt-6 pt-6 flex justify-between">
          <h1 className="font-mainFont font-titleFontWeigt text-4xl">
            Detailweergave
          </h1>
          <div className="mr-8">
            <Button
              className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt"
              onClick={() => {
                setPopupVisibility("popupGoBack", true);
                setPopupVisibility("detailChange", false);
              }}
            >
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </div>
        </div>
        <div className="flex flex-wrap">
          <div className="ml-9 mt-14 pb-6">
            {tempObject &&
              Object.entries(tempObject).map(([key, value]) => (
                <div key={key} className="flex items-center mb-4">
                  <label htmlFor={key} className="block text-blueText mr-2">
                    {key}
                  </label>
                  <input
                    type="text"
                    id={key}
                    name={key}
                    value={isEditing ? data[key] : value}
                    onChange={(e) => handleDataChange(key, e.target.value)}
                    className="w-[100%] h-9 p-2 rounded-md border border-b border-blueText text-blueText bg-transparent focus:ring-blueText focus:border-blueText"
                    disabled={!isEditing}
                  />
                </div>
              ))}
          </div>
        </div>
        <div className="flex relative w-1/2 h-16 ml-[50%]">
          {showCheckMark && (
            <div className="flex ml-[25%] xl3:ml-[40%]">
              <p className="text-[#858585] font-btnFontWeigt font-Helvetica p-3">
                Succes!
              </p>
              <img
                src={CheckNoBg}
                alt="Checkmark"
                className="w-10 h-10 rounded-full"
              />
            </div>
          )}
          <div className="pl-3 absolute right-[10%]">
            <Button
              className="w-28 h-[40px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-whiteText bg-blueBtn hover:bg-hoverBtn cursor-pointer"
              onClick={handleToggleEditMode}
            >
              {isEditing ? "Opslaan" : "Bewerk"}
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DetailChange;
