import { useEffect, useState } from "react";
import Table from "../components/Table";
import {
  getFleets,
  getBestuurders,
  getTankkaarten,
  getVoertuigen,
  UpdateFleet,
  DeleteFleet,
  PostFleet,
} from "../constants/Api";
import Overlay from "../components/Overlay";
import {
  textPopupVerwijderItem,
  textPopupGaTerug,
} from "../constants/textPopups";
import Popup from "../components/Popup";
import PopupRemoveItem from "../components/PopupRemoveItem";
import PopupCloseDetailChange from "../components/PopupCloseDetailChange";
import DetailDisplay from "../components/DetailDisplay";
import DynamicForm from "../components/DynamicForm";

const FleetPage = () => {
  /* The `tableHeaderContent` variable is an array that contains the header titles for a table. Each
  element in the array represents a column header in the table.*/
  const tableHeaderContent = [
    "bestuurderNaam",
    "bestuurderVoornaam",
    "tankkaartId",
    "voertuigMerkModel",
    "voertuigNummerplaat",
    "voertuigChassisnummer",
    "Acties", //Laten blijven
  ];
  /* The `inputData` array is used to define the data that will be displayed in each column of the table.*/
  const inputData = [
    "d.bestuurderNaam",
    "d.bestuurderVoornaam",
    "d.tankkaartId",
    "d.voertuigMerkModel",
    "d.voertuigNummerplaat",
    "d.voertuigChassisnummer",
  ];
  /* The `iDname` variable is used to specify the name of the ID field in the table data.*/
  const iDname = "fleetMemberId";

  const [counter, setCounter] = useState(0);
  const [data, setData] = useState([]);
  const [voertuigenData, setvoertuigenData] = useState([]);
  const [tankkaartenData, settankkaartenData] = useState([]);
  const [bestuurdersData, setbestuurdersData] = useState([]);

  useEffect(() => {
    console.log("Effect is running");
    const fetchData = async () => {
      try {
        const fleetData = await getFleets();
        const voertuigenData = await getVoertuigen();
        const tankkaartenData = await getTankkaarten();
        const bustuurdersData = await getBestuurders();
  
        setData(fleetData);
        setvoertuigenData(voertuigenData);
        settankkaartenData(tankkaartenData);
        setbestuurdersData(bustuurdersData);

        console.log("Data is fetched");
      } catch (error) {
        console.error("Error fetching data:", error.message);
      }
    };
  
    fetchData();
  }, [counter]);  

  const triggerRerender = () => {
    setTimeout(() => {
      setCounter(counter + 1);
    }, 100);
  };


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
              apiFunction={DeleteFleet}
              setPopupVisibility={setPopupVisibility}
              tempId={temp.tempId}
              triggerRerender={triggerRerender}
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
      <DynamicForm
          setPopupVisibility={setPopupVisibility}
          apiCmd={UpdateFleet}
          //TODO: fix bestuurder naar 1 select
          //TODO: fix voertuig naar 1 select
          formFields={[
            {
              name: "bestuurderNaam",
              label: "bestuurderNaam",
              type: "select",
              options: bestuurdersData.map((t) => ({
                value: t.naam,
                label: t.naam,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "bestuurderVoornaam",
              label: "bestuurderVoornaam",
              type: "select",
              options: bestuurdersData.map((t) => ({
                value: t.voornaam,
                label: t.voornaam,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "tankkaartId",
              label: "tankkaartId",
              type: "select",
              options: tankkaartenData.map((t) => ({
                value: t.tankkaartId,
                label: t.tankkaartId,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "voertuigMerkModel",
              label: "voertuigMerkModel",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.merkEnModel,
                label: t.merkEnModel,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "voertuigNummerplaat",
              label: "voertuigNummerplaat",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.nummerplaat,
                label: t.nummerplaat,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "voertuigChassisnummer",
              label: "voertuigChassisnummer",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.chassisnummer,
                label: t.chassisnummer,
              })),
              initialValue: "",
              required: false,
            },
          ]}
          tempObject={temp.tempObject}  
          triggerRerender={triggerRerender}
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
      <DynamicForm
          setPopupVisibility={setPopupVisibility}
          apiCmd={PostFleet}
          formFields={[
            {
              name: "bestuurderNaam",
              label: "bestuurderNaam",
              type: "select",
              options: bestuurdersData.map((t) => ({
                value: t.naam,
                label: t.naam,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "bestuurderVoornaam",
              label: "bestuurderVoornaam",
              type: "select",
              options: bestuurdersData.map((t) => ({
                value: t.voornaam,
                label: t.voornaam,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "tankkaartId",
              label: "tankkaartId",
              type: "select",
              options: tankkaartenData.map((t) => ({
                value: t.tankkaartId,
                label: t.tankkaartId,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "voertuigMerkModel",
              label: "voertuigMerkModel",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.merkEnModel,
                label: t.merkEnModel,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "voertuigNummerplaat",
              label: "voertuigNummerplaat",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.nummerplaat,
                label: t.nummerplaat,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "voertuigChassisnummer",
              label: "voertuigChassisnummer",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.chassisnummer,
                label: t.chassisnummer,
              })),
              initialValue: "",
              required: true,
            },
          ]}
          tempObject={{}}  
          triggerRerender={triggerRerender}
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

export default FleetPage;
