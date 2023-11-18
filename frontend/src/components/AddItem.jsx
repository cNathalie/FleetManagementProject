// eslint-disable-next-line no-unused-vars
import React, { useState } from "react";
import listHeaders from "../constants/listHeader";
import List from "./List";
import Button from "./Button";
import CheckNoBg from "../assets/Media/CheckNoBg.png";

const AddItem = () => {
  const [data, setData] = useState({});
  const [showCheckMark, setShowSetMark] = useState(false);

  const handleDataChange = (field, value) => {
    setData({
      ...data,
      [field]: value,
    });
  };

  const handleSaveChanges = () => {
    // Logic to save data to DB

    // Reset data after saving
    setData({});

    //Checkmark to show saving was succesful
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
                const overlay = document.getElementById("overlay");
                const addItem = document.getElementById("addItem");
                overlay.style.display = "none";
                addItem.style.display = "none";
              }}
            >
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </div>
        </div>
        <div className="flex flex-wrap">
          <div className="ml-9 mt-14 pb-6">
            {/* Shows headers of the list on left of actual data */}
            {listHeaders.map((h) => {
              return (
                <List
                  className="pb-2 text-lg font-mainFont font-titleFontWeigt"
                  key={h}
                  field={h}
                  value={data[h]}
                  onChange={handleDataChange}
                  eigenaar={h.eigenaar}
                  merk={h.merk}
                  model={h.model}
                  chassisnummer={h.chassisnummer}
                  nummerplaat={h.nummerplaat}
                  brandstoftype={h.brandstoftype}
                  typeWagen={h.typeWagen}
                  kleur={h.kleur}
                  aantalDeuren={h.aantalDeuren}
                  listHeader={h}
                />
              );
            })}
          </div>
          <div className="ml-12 mt-14">
            {listHeaders.map((headerObj, index) =>
              Object.keys(headerObj).map((key) => (
                <div key={`${key}-${index}`} className="flex flex-col">
                  <input
                    id={key}
                    type="text"
                    value={data[key] || ""}
                    onChange={(e) => handleDataChange(key, e.target.value)}
                    placeholder={`Geef ${headerObj[key]}`}
                    className="w-[100%] h-9 p-2 rounded-md border border-b border-blueText text-blueText bg-transparent focus:ring-blueText focus:border-blueText"
                  />
                </div>
              ))
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
