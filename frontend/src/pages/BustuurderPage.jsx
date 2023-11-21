import { useEffect, useState } from "react";
import Table from "../components/Table";
import { getBestuurders, UpdateBestuurder, DeleteBestuurder, PostBestuurder } from "../constants/Api";
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
import { initialBestuurdersFormData } from "../constants/formFields";

const BustuurderPage = () => {
  const tableHeaderContent = [
    "naam",
    "voornaam",
    "adres",
    "rijksregisternummer",
    "rijbewijs",
    "Acties", //Laten blijven
  ];
  const inputData = [
    "d.naam",
    "d.voornaam",
    "d.adres",
    "d.rijksregisternummer",
    "d.rijbewijs",
  ];
  const iDname = 'bestuurderId';

  const [data, setData] = useState([]);
  useEffect(() => {
    const fetchData = () => {
      getBestuurders()
        .then((Data) => {
          setData(Data);
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
    //tempContent: [],
    tempId: 0,
    tempObject: {},
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
                apiFunction={DeleteBestuurder}
                setPopupVisibility={setPopupVisibility}
                tempId={temp.tempId}
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
          <DetailChange setPopupVisibility={setPopupVisibility} UpdateVoertuig={UpdateBestuurder} tempObject={temp.tempObject}/>
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
          <DetailDisplay setPopupVisibility={setPopupVisibility} tempObject={temp.tempObject}/>
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
          <AddItem setPopupVisibility={setPopupVisibility} apiCmd={PostBestuurder} initialFormData={initialBestuurdersFormData[0]}/>
        </div>

        <Table {...{tableHeaderContent, data, inputData, setPopupVisibility, setTempContent, iDname}} />
    </>
  );
};

export default BustuurderPage;
