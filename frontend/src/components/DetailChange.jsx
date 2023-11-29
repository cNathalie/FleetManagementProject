/* eslint-disable react/prop-types */
import { useState } from "react";
import Button from "./Button";
import CheckNoBg from "../assets/Media/CheckNoBg.png";
import { useFormik } from "formik";
import { TEXT_STYLES, INPUT_STYLES } from "../constants/tailwindStyles";

const DetailChange = ({setPopupVisibility, UpdateApi, tempObject, formFields}) => {
  const [isEditing, setIsEditing] = useState(false);
  const [showCheckMark, setShowCheckMark] = useState(false);

  const handleToggleEditMode = () => {
    if (isEditing) {
      formik.handleSubmit();
    }

      // Reset checkmark after 3 seconds
    setTimeout(() => {setShowCheckMark(true)}, 3000);

    setIsEditing(!isEditing);
  };

  const formik = useFormik({
    initialValues: tempObject,
    onSubmit: async (changedData) => {
      // Check for required fields
      const errors = formik.validateForm(changedData);

      if (Object.keys(errors).length === 0) {
        try {
          const updatedData = { ...tempObject, ...changedData };
          await UpdateApi(updatedData);
          setShowCheckMark(true);
        } catch (error) {
          console.error("Error during PUT request:", error.message);
        }
      }
    },
    validate: (changedData) => {
      const errors = {};
      formFields.forEach((field) => {
        if (field.required && !changedData[field.name]) {
          errors[field.name] = "Verplicht veld";
        }
      });
      return errors;
    },
  });

  return (
    <div className=" inline-block w-1/2 ml-[25%] mt-6 rounded-xl bg-[#DBDBDB] max-w-xl">
      <div className="p-6">
        <form onSubmit={formik.handleSubmit} className="space-y-4">

          <header className="flex justify-between">
            <h1 className="font-mainFont font-titleFontWeigt text-4xl">
              Detailweergave
            </h1>
            <Button
              className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt"
              onClick={() => {
                setPopupVisibility("popupGoBack", true);
                setPopupVisibility("detailChange", false);
                setIsEditing(!isEditing);
              }}
            >
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </header>
          
          {formFields.map((field) => (
            <div key={field.name} className="space-x-4 items-center grid grid-cols-2">
              <label className={TEXT_STYLES.ADMIN_OR} htmlFor={field.name}>
                {field.label}:
              </label>

              {field.type === "select" ? (
                <select id={field.name} name={field.name} onChange={formik.handleChange} value={formik.values[field.name]} className={INPUT_STYLES.USERFORM_INPUT_BLACK} disabled={!isEditing}>
                  <option value="">Selecteer...</option>
                  {field.options.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </select>
              ) : (
                <input id={field.name} name={field.name} type={field.type || "text"} onChange={formik.handleChange} value={formik.values[field.name]} className={INPUT_STYLES.USERFORM_INPUT_BLACK} disabled={!isEditing}/>
              )}

              {formik.errors[field.name] && (
                <div className="text-red-500">{formik.errors[field.name]}</div>
              )}
            </div>
          ))}

          <footer className="flex items-center mt-4">
            {showCheckMark && (
              <div className="flex items-center space-x-2">
                <p className="text-[#858585] font-btnFontWeigt font-Helvetica">
                  Succes!
                </p>
                <img src={CheckNoBg} alt="Checkmark" className="w-8 h-8 rounded-full"/>
              </div>
            )}
            <div className="ml-auto">
              <Button className="w-28 h-[40px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-whiteText bg-blueBtn hover:bg-hoverBtn cursor-pointer" onClick={handleToggleEditMode} type="button">
                {isEditing ? "Opslaan" : "Bewerk"}
              </Button>
            </div>
          </footer>

        </form>
      </div>
    </div>
  );
};

export default DetailChange;
