import { useEffect, useState, useRef } from "react";
import Table from "../components/Table";
import {
  getBestuurders,
  UpdateBestuurder,
  DeleteBestuurder,
  PostBestuurder,
  getTypeRijbewijs,
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

const BustuurderPage = () => {
  /* The `tableHeaderContent` variable is an array that contains the header titles for a table. Each
  element in the array represents a column header in the table.*/
  const tableHeaderContent = [
    "Naam",
    "Voornaam",
    "Adres",
    "Rijksregisternummer",
    "Rijbewijs",
    "Acties", //Laten blijven
  ];
  /* The `inputData` array is used to define the data that will be displayed in each column of the table.*/
  const inputData = [
    "d.naam",
    "d.voornaam",
    "d.adres",
    "d.rijksregisternummer",
    "d.rijbewijs",
  ];
  /* The `iDname` variable is used to specify the name of the ID field in the table data.*/
  const iDname = "bestuurderId";

  const [counter, setCounter] = useState(0);
  const [data, setData] = useState([]);
  const [typeRijbewijs, setTypeRijbewijs] = useState([]);
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
        const voertuigenData = await getBestuurders();
        const typeRijbewijsData = await getTypeRijbewijs();

        setData(voertuigenData);
        setTypeRijbewijs(typeRijbewijsData);
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
                apiFunction={DeleteBestuurder}
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
          apiCmd={UpdateBestuurder}
          formFields={[
            {
              name: "naam",
              label: "naam",
              type: "text",
              initialValue: "",
              required: false,
            },
            {
              name: "voornaam",
              label: "voornaam",
              type: "text",
              initialValue: "",
              required: false,
            },
            {
              name: "adres",
              label: "adres",
              type: "text",
              initialValue: "",
              required: false,
            },
            {
              name: "rijksregisternummer",
              label: "rijksregisternummer",
              type: "text",
              initialValue: "",
              required: false,
            },
            {
              name: "rijbewijs",
              label: "rijbewijs",
              type: "select",
              options: typeRijbewijs.map((t) => ({
                value: t.type,
                label: t.type,
              })),
              initialValue: "",
              required: false,
            },
          ]}
          tempObject={temp.tempObject}
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
          apiCmd={PostBestuurder}
          formFields={[
            {
              name: "naam",
              label: "naam",
              type: "text",
              initialValue: "",
              required: true,
            },
            {
              name: "voornaam",
              label: "voornaam",
              type: "text",
              initialValue: "",
              required: true,
            },
            {
              name: "adres",
              label: "adres",
              type: "text",
              initialValue: "",
              required: true,
            },
            {
              name: "rijksregisternummer",
              label: "rijksregisternummer",
              type: "text",
              initialValue: "",
              required: true,
            },
            {
              name: "rijbewijs",
              label: "rijbewijs",
              type: "select",
              options: typeRijbewijs.map((t) => ({
                value: t.type,
                label: t.type,
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

export default BustuurderPage;
