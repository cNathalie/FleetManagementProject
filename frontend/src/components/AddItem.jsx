// eslint-disable-next-line no-unused-vars
import React, { useState } from "react";
import Button from "./Button";
import CheckNoBg from "../assets/Media/CheckNoBg.png";
import isEqual from "lodash/isEqual";

const AddItem = ({ setPopupVisibility, apiCmd, initialFormData }) => {
  const [data, setData] = useState(initialFormData);
  const [showCheckMark, setShowSetMark] = useState(false);

  const handleDataChange = (field, value) => {
    setData((prevData) => ({
      ...prevData,
      [field]: value,
    }));
  };

  const handleSaveChanges = async () => {
    // Logic to save data to DB
    try {
      await apiCmd(data);
      console.log("Data to be saved:", data);

      // Check if the format matches initialVoertuigFormData[0]
      const isFormatValid = isEqual(data, initialFormData);
      console.log("Is format valid:", isFormatValid);
    } catch (error) {
      console.error("Error during PUT request:", error.message);
    }

    // Reset data after saving
    setData({});

    // Checkmark to show saving was successful
    setShowSetMark(true);
    setTimeout(() => {
      setShowSetMark(false);
    }, 3000);
  };

  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB]">
      <div>
        <div className="ml-9 mt-6 pt-6 flex justify-between">
          <h1 className="font-mainFont font-titleFontWeigt text-4xl">
            Item toevoegen
          </h1>
          <div className="mr-8">
            <Button
              className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt"
              onClick={() => {
                setPopupVisibility("overlay", false);
                setPopupVisibility("addItem", false);
              }}
            >
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </div>
        </div>
        <div className="flex flex-wrap">
          <div className="ml-9 mt-14 pb-6">
            {initialFormData ? (
              Object.entries(initialFormData).map(([key]) => (
                <div key={key} className="flex items-center mb-4">
                  <label htmlFor={key} className="block text-blueText mr-2">
                    {key}
                  </label>
                  <input
                    id={key}
                    type="text"
                    onChange={(e) => handleDataChange(key, e.target.value)}
                    className="w-[100%] h-9 p-2 rounded-md border border-b border-blueText text-blueText bg-transparent focus:ring-blueText focus:border-blueText"
                  />
                </div>
              ))
            ) : (
              <p>Error: tempVoertuigContent is null or undefined</p>
            )}
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
              onClick={handleSaveChanges}
            >
              Opslaan
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AddItem;
