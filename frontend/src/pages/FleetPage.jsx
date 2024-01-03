import { useEffect, useState, useRef } from "react";
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
    "Familienaam",
    "Voornaam",
    "Tankkaart",
    "Voertuig",
    "Nummerplaat",
    "Chassisnummer",
    "Acties", //Laten blijven
  ];
  /* The `inputData` array is used to define the data that will be displayed in each column of the table.*/
  const inputData = [
    "d.bestuurderNaam",
    "d.bestuurderVoornaam",
    "d.tankaartId",
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
  const overlayRef = useRef();
  const removeItemRef = useRef();
  const detailChangeRef = useRef();
  const detailDisplayRef = useRef();
  const goBackRef = useRef();
  const addItemRef = useRef();

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

  const setPopupVisibility = (ref, visibility) => {
    const element = ref.current;
    if (element) {
      element.style.display = visibility ? "block" : "none";
    } else {
      console.error(`Element with ref ${ref} not found.`);
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
      <div style={{ display: "none" }} ref={overlayRef}>
        <Overlay />
      </div>

      <div style={{ display: "none" }} ref={removeItemRef}>
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
                refOverlay={overlayRef}
                refRemoveItem={removeItemRef}
              />
            );
          })}
        </Popup>
      </div>

      <div
        id="detailChange"
        ref={detailChangeRef}
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
              name: "bestuurderId",
              label: "bestuurder",
              type: "select",
              options: bestuurdersData.map((t) => ({
                value: t.bestuurderId,
                label: Object.values(t).join(" "),
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "tankkaartId",
              label: "tankkaart",
              type: "select",
              options: tankkaartenData.map((t) => ({
                value: t.tankkaartId,
                label: t.tankkaartId,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "voertuigId",
              label: "voertuig",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.voertuigId,
                label: Object.values(t).join(" "),
              })),
              initialValue: "",
              required: false,
            },
          ]}
          tempObject={{
            fleetId: temp.tempObject.fleetId,
            bestuurderId: `${temp.tempObject.bestuurderNaam} ${temp.tempObject.bestuurderVoornaam}`,
            voertuigId: temp.tempObject.voertuigMerkModel,
            tankkaartId: temp.tempObject.tankaartId,
          }}
          triggerRerender={triggerRerender}
          heading="Bewerk"
          btnValue="Bewerk"
          refGoBack={goBackRef}
          refDetailChange={detailChangeRef}
          refAddItem={addItemRef}
          refOverlay={overlayRef}
        />
      </div>

      <div
        id="detailDisplay"
        ref={detailDisplayRef}
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
          refOverlay={overlayRef}
          refDetailDisplay={detailDisplayRef}
        />
      </div>
      <div style={{ display: "none" }} ref={goBackRef}>
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
                refOverlay={overlayRef}
                refDetailChange={detailChangeRef}
                refGoBack={goBackRef}
              />
            );
          })}
        </Popup>
      </div>

      <div
        id="addItem"
        ref={addItemRef}
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
              name: "bestuurderId",
              label: "bestuurder",
              type: "select",
              options: bestuurdersData.map((t) => ({
                value: t.bestuurderId,
                label: Object.values(t).join(" "),
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "tankkaartId",
              label: "tankkaart",
              type: "select",
              options: tankkaartenData.map((t) => ({
                value: t.tankkaartId,
                label: t.tankkaartId,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "voertuigId",
              label: "voertuig",
              type: "select",
              options: voertuigenData.map((t) => ({
                value: t.voertuigId,
                label: Object.values(t).join(" "),
              })),
              initialValue: "",
              required: true,
            },
          ]}
          tempObject={{}}
          triggerRerender={triggerRerender}
          heading="Toevoegen"
          btnValue="Voeg toe"
          refGoBack={goBackRef}
          refDetailChange={detailChangeRef}
          refAddItem={addItemRef}
          refOverlay={overlayRef}
        />
      </div>

      <Table
        tableHeaderContent={tableHeaderContent}
        inputData={inputData}
        data={data}
        setPopupVisibility={setPopupVisibility}
        setTempContent={setTempContent}
        iDname={iDname}
        refRemoveItem={removeItemRef}
        refDetailChange={detailChangeRef}
        refDetailDisplay={detailDisplayRef}
        refAddItem={addItemRef}
        refOverlay={overlayRef}
      />
    </>
  );
};

export default FleetPage;
