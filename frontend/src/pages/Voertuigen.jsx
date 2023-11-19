import { useEffect, useState } from "react";
import Table from "../components/Table";
import { getVoertuigen } from "../constants/Api";
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
import AddItem from "../components/AddItem";

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
    "v.merkEnModel.split(' ')[0]",
    "v.merkEnModel.split(' ')[1]",
    "v.chassisnummer",
    "v.nummerplaat",
    "v.brandstoftype",
  ];

  useEffect(() => {
    const fetchData = () => {
      getVoertuigen()
        .then((voertuigenData) => {
          setData(voertuigenData);
        })
        .catch((error) => {
          console.error("Error fetching data:", error);
        });
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

      <Table {...{tableHeaderContent, data, inputData}} />
    </>
  );
};

export default VoertuigenPage;
