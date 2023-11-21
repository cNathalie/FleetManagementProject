import { useEffect, useState } from "react";
import Table from "../components/Table";
import { getVoertuigen, DeleteVoertuig, getVoertuig, UpdateVoertuig , PostVoertuig} from "../constants/Api";
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
  

  const [data, setData] = useState([]);
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


  const setPopupVisibility = (popupId, visibility) => {
    const element = document.getElementById(popupId);
    if (element) {
      element.style.display = visibility ? "block" : "none";
    } else {
      console.error(`Element with id ${popupId} not found.`);
    }
  };


  const [temp , setTemp] = useState({
    tempVoertuigContent: [],
    tempVoertuigId: 0,
    tempVoertuigObject: {},
  });
  const setTempContent = (key, value) => {
    setTemp({
      ...temp,
      [key] : value
    });
  };


  return (
    <>
        <Overlay />

        <Popup id="Popup">
          {textPopupVerwijderItem.map((p) => {
            return (
              <PopupRemoveItem 
                key={p.id}
                popup={{
                  title: p.title,
                  text: p.text,
                  textBtnLeft: p.textBtnLeft,
                  textBtnRight: p.textBtnRight,
                  popup: p
                }}
                apiFunction={DeleteVoertuig}
                setPopupVisibility={setPopupVisibility}
                tempVoertuigId={temp.tempVoertuigId}
              />
            );
          })}
        </Popup>

        <div
          id="detailChange"
          style={{
            zIndex: 60,
            position: "absolute",
            width: "100%",
            display: "none",
          }}
        >
          <DetailChange setPopupVisibility={setPopupVisibility} UpdateVoertuig={UpdateVoertuig} tempVoertuigObject={temp.tempVoertuigObject}/>
        </div>

        <div
          id="detailDisplay"
          style={{
            zIndex: 60,
            position: "absolute",
            width: "100%",
            display: "none",
          }}
        >
          <DetailDisplay setPopupVisibility={setPopupVisibility} tempVoertuigContent={temp.tempVoertuigContent}/>
        </div> 

        <Popup id="popupGoBack">
          {textPopupGaTerug.map((p) => {
            return (
              <PopupCloseDetailChange
                key={p.id}
                popup={{
                  title: p.title,
                  text: p.text,
                  textBtnLeft: p.textBtnLeft,
                  textBtnRight: p.textBtnRight,
                  popup: p
                }}
                setPopupVisibility={setPopupVisibility}
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
          <AddItem setPopupVisibility={setPopupVisibility} apiCmd={PostVoertuig} />
        </div>

        <Table {...{tableHeaderContent, data, inputData, setPopupVisibility, setTempContent}} />
    </>
  );
};

export default VoertuigenPage;
