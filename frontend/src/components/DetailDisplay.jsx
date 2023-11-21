// eslint-disable-next-line no-unused-vars
import React from "react";
import Button from "./Button";

const DetailDisplay = ({ setPopupVisibility,  tempObject}) => {

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
        {/*
        <div className="flex flex-wrap">
           <div className="ml-9 mt-14 pb-6">
            {listHeaders.map((h) => {
              return (
                <List
                  className="pb-2 text-lg font-mainFont font-titleFontWeigt"
                  key={h}
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
            {tempContent && tempContent.map((d) => {
              return (
                <List
                  className="pb-2 text-lg font-mainFont font-titleFontWeigt"
                  key={d}
                  merk={d.merkEnModel.split(" ")[0]}
                  model={d.merkEnModel.split(" ")[1]}
                  chassisnummer={d.chassisnummer}
                  nummerplaat={d.nummerplaat}
                  brandstoftype={d.brandstoftype}
                  typeWagen={d.typewagen}
                  kleur={d.kleur}
                  aantalDeuren={d.aantalDeuren}
                  listHeader={d}
                />
              );
            })}
          </div>
        </div> 
        */}
        <div className="flex flex-wrap">
              {tempObject ? (
                Object.entries(tempObject).map(([key, value]) => (
                  <>
                    <div key={key} className="ml-9 mt-14 pb-6">
                      <label htmlFor={key} className="pb-2 text-lg font-mainFont font-titleFontWeigt">
                        {key}
                      </label>
                    </div>
                    <div className="ml-12 mt-14">
                        <p
                          type="text"
                          id={key}
                          name={key}
                          className="pb-2 text-lg font-mainFont font-titleFontWeigt"
                        >
                        {typeof value === 'boolean' ? (value ? 'vrij' : 'bezet') : value}
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
