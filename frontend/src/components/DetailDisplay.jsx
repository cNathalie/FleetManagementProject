import React from "react";
import listHeaders from "../constants/listHeader";
import List from "./List";
import Button from "./Button";

const DetailDisplay = () => {
  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB] ">
      <div>
        <div className="ml-9 mt-6 pt-6 flex justify-between">
          <h1 className="font-mainFont font-titleFontWeigt text-4xl">
            Detailweergave
          </h1>
          <div className="mr-8">
            <Button className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt">
              <img src="../src/assets/Media/closeButton.jpg" />
            </Button>
          </div>
        </div>
        <div className="flex flex-wrap">
          <div className="ml-9 mt-14 pb-6">
            {listHeaders.map((h) => {
              return (
                <List
                  className="pb-2 text-lg font-mainFont font-titleFontWeigt"
                  key={h}
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
            {/* zelfde als hierboven maar met database data en andere styling */}
          </div>
        </div>
      </div>
    </div>
  );
};

export default DetailDisplay;
