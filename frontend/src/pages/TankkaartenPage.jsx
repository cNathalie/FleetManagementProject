import { useEffect, useState } from "react";
import Table from "../components/Table";
import {
  getTankkaarten,
  UpdateTankkaart,
  DeleteTankkaart,
  PostTankkaart,
  getBrandstofTypes,
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

  const [counter, setCounter] = useState(0);
  const [data, setData] = useState([]);
  const [brandstofTypesData, setBrandstofTypesData] = useState([]);

  useEffect(() => {
    console.log("Effect is running");
    const fetchData = async () => {
      try {
        const tankkaartData = await getTankkaarten();
        const brandstofTypesData = await getBrandstofTypes();

        setData(tankkaartData);
        setBrandstofTypesData(brandstofTypesData);
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
              apiFunction={DeleteTankkaart}
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
          apiCmd={UpdateTankkaart}
          formFields={[
            {
              name: "geldigheidsdatum",
              label: "geldigheidsdatum",
              type: "date",
              initialValue: "",
              required: false,
            },
            {
              name: "pincode",
              label: "pincode",
              type: "number",
              initialValue: "",
              required: false,
            },
            {
              name: "brandstofType",
              label: "brandstofType",
              type: "select",
              options: brandstofTypesData.map((t) => ({
                value: t.type,
                label: t.type,
              })),
              initialValue: "",
              required: false,
            },
            {
              name: "isActief",
              label: "isActief",
              type: "select",
              options: [
                { label: "True", value: true },
                { label: "False", value: false },
              ],
              initialValue: "",
              required: false,
            },
          ]}
          tempObject={temp.tempObject}
          triggerRerender={triggerRerender}
          heading="Bewerk"
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
          apiCmd={PostTankkaart}
          formFields={[
            {
              name: "kaartnummer",
              label: "kaartnummer",
              type: "number",
              initialValue: "",
              required: true,
            },
            {
              name: "geldigheidsdatum",
              label: "geldigheidsdatum",
              type: "date",
              initialValue: "",
              required: true,
            },
            {
              name: "pincode",
              label: "pincode",
              type: "number",
              initialValue: "",
              required: true,
            },
            {
              name: "brandstofType",
              label: "brandstofType",
              type: "select",
              options: brandstofTypesData.map((t) => ({
                value: t.type,
                label: t.type,
              })),
              initialValue: "",
              required: true,
            },
            {
              name: "isActief",
              label: "isActief",
              type: "select",
              options: [
                { label: "True", value: true },
                { label: "False", value: false },
              ],
              initialValue: "",
              required: false,
            },
          ]}
          tempObject={{}}
          triggerRerender={triggerRerender}
          heading="Toevoegen"
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
