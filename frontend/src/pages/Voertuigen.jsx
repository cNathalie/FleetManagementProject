// eslint-disable-next-line no-unused-vars
import React from "react";
import Table from "../components/Table";
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
  const tableHeaderContent = [
    "Merk",
    "Model",
    "Chasisnummer",
    "Nummerplaat",
    "Brandstoftype",
    "Acties",
  ];

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

      <Table tableHeaderContent={tableHeaderContent} />
    </>
  );
};

export default VoertuigenPage;
