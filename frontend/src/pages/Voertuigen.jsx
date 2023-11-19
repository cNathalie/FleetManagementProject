// eslint-disable-next-line no-unused-vars
import React from "react";
import { useState, useEffect } from "react";
import Table from "../components/Table";
import ApiUrls from "../constants/ApiUrls";
import Overlay from "../components/Overlay";
import {
  textPopupVerwijderItem,
  textPopupGaTerug,
} from "../constants/textPopups";
import Popup from "../components/Popup";
import PopupRemoveItem from "../components/PopupRemoveItem";
import PopupCloseDetailChange from "../components/PopupCloseDetailChange";
import DetailChange from "../components/DetailChange";
import DetailDisplay from "../components/DetailDisplay";

const VoertuigenPage = () => {
  const [data, setData] = useState([]);
  const tableHeaderContent = [
    "Merk",
    "Model",
    "Chasisnummer",
    "Nummerplaat",
    "Brandstoftype",
    "Acties",
  ];
  const inputData = [
    'v.merkEnModel?.split(" ")[0] ?? "no Data"',
    'v.merkEnModel?.split(" ")[1] ?? "no Data"',
    'v.chassisnummer ?? "no Data"',
    'v.nummerplaat ?? "no Data"',
    'v.brandstoftype ?? "no Data"',
  ];

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(ApiUrls.GetVoertuigen);
        const data = await response.json();
        setData(data);
      } catch (error) {
        console.log("Error fetching data:", error);
      }
    };
  
    fetchData();
  }, []);
  

  return (
    <>
      <Overlay />
      <Popup id="popupRemoveItem">
        {textPopupVerwijderItem.map((p) => {
          return (
            <PopupRemoveItem
              key={p.id}
              title={p.title}
              text={p.text}
              textBtnLeft={p.textBtnLeft}
              textBtnRight={p.textBtnRight}
              popup={p}
            />
          );
        })}
      </Popup>
      <div
        id="detailChange"
        style={{
          zIndex: 60,
          position: "absolute",
          display: "none",
          width: "100%",
        }}
      >
        <DetailChange />
      </div>
      <div
        id="detailDisplay"
        style={{
          zIndex: 60,
          position: "absolute",
          display: "none",
          width: "100%",
        }}
      >
        <DetailDisplay />
      </div>

      <Popup id="popupGoBack">
        {textPopupGaTerug.map((p) => {
          return (
            <PopupCloseDetailChange
              key={p.id}
              title={p.title}
              text={p.text}
              textBtnLeft={p.textBtnLeft}
              textBtnRight={p.textBtnRight}
              popup={p}
            />
          );
        })}
      </Popup>
      <div
        id="addItem"
        style={{
          zIndex: 60,
          position: "absolute",
          display: "none",
          width: "100%",
        }}
      >
        <AddItem />
      </div>

      <Table tableHeaderContent={tableHeaderContent} />
    </>
  );
};

export default VoertuigenPage;
