import { useEffect, useState } from "react";
import Table from "../components/Table";
import {
  getTankkaarten,
  UpdateTankkaart,
  DeleteTankkaart,
  PostTankkaart,
} from "../constants/Api";
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
import { initialTankkaartFormData } from "../constants/formFields";

const TankkaartenPage = () => {
  /* The `tableHeaderContent` variable is an array that contains the header titles for a table. Each
  element in the array represents a column header in the table.*/
  const tableHeaderContent = [
    "kaartnummer",
    "geldigheidsdatum",
    "pincode",
    "brandstofType",
    "isActief",
    "Acties", //Laten blijven
  ];
  /* The `inputData` array is used to define the data that will be displayed in each column of the table.*/
  const inputData = [
    "d.kaartnummer",
    "d.geldigheidsdatum",
    "d.pincode",
    "d.brandstofType",
    "d.isActief ? 'vrij' : 'bezet'",
  ];
  /* The `iDname` variable is used to specify the name of the ID field in the table data.*/
  const iDname = "tankkaartId";

  const [data, setData] = useState([]);
  useEffect(() => {
    const fetchData = () => {
      getTankkaarten()
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

  /* lets a child component change the value in return other child component
   use this value for their */
  const [temp, setTemp] = useState({
    //tempContent: [],
    tempId: 0,
    tempObject: {},
  });
  const setTempContent = (key, value) => {
    setTemp({
      ...temp,
      [key]: value,
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
                popup: p,
              }}
              apiFunction={DeleteTankkaart}
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
        <DetailChange
          setPopupVisibility={setPopupVisibility}
          UpdateVoertuig={UpdateTankkaart}
          tempObject={temp.tempObject}
        />
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
        <DetailDisplay
          setPopupVisibility={setPopupVisibility}
          tempObject={temp.tempObject}
        />
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
                popup: p,
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
        <AddItem
          setPopupVisibility={setPopupVisibility}
          apiCmd={PostTankkaart}
          initialFormData={
            /* `initialTankkaartFormData[0]` is accessing the first Array in the
          `initialBestuurdersFormData` Object. */
            initialTankkaartFormData[0]
          }
        />
      </div>

      <Table
        {...{
          tableHeaderContent,
          data,
          inputData,
          setPopupVisibility,
          setTempContent,
          iDname,
        }}
      />
    </>
  );
};

export default TankkaartenPage;
