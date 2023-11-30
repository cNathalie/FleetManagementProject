/* eslint-disable react/prop-types */
import { useState, useEffect } from "react";
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

    setIsEditing(!isEditing);
  };

  useEffect(() => {
    // Initialize formik with tempObject as initial values
    formik.setValues(tempObject || {});
  }, [tempObject]);

  const formik = useFormik({
    initialValues: tempObject || {},
    onSubmit: async (changedData, { setSubmitting }) => {
      try {
        const updatedData = { ...tempObject, ...changedData };
        const response = await UpdateApi(updatedData);
        if(response.status === 200){
          // If the API update is successful, toggle editing off and show checkmark
          setIsEditing(false);
          setShowCheckMark(true);

          // Reset checkmark after 3 seconds
          setTimeout(() => {
            setShowCheckMark(false);
          }, 3000);
        }

      } catch (error) {
        console.error("Error during PUT request:", error.message);
      } finally {
        // Ensure to set submitting to false to unlock the form
        setSubmitting(false);
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
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB]">
      <div className="mt-6 p-6">
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
                setIsEditing(false);
              }}
              type="button"
            >
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </header>
          
          {formFields.map((field) => (
            <div key={field.name} className="items-center grid grid-cols-3">
              <label className={`${TEXT_STYLES.ADMIN_OR} text-right col-span-1`} htmlFor={field.name}>
                {field.label}:
              </label>

              <section className="col-span-2">
                {field.type === "select" ? (
                  <select id={field.name} name={field.name} onChange={formik.handleChange} value={formik.values[field.name]} className={`${INPUT_STYLES.USERFORM_INPUT_BLACK} w-[75%]`} disabled={!isEditing}>
                    <option value="">Selecteer...</option>
                    {field.options.map((option) => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>
                ) : (
                  <input id={field.name} name={field.name} type={field.type || "text"} onChange={formik.handleChange} value={formik.values[field.name]} className={`${INPUT_STYLES.USERFORM_INPUT_BLACK} w-[75%]`} disabled={!isEditing}/>
                )}

                {/* {formik.errors[field.name] && (
                  <div className="text-red-500">{formik.errors[field.name]}</div>
                )} */}
              </section>
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
