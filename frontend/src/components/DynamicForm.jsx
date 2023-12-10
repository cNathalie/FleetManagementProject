/* eslint-disable react/prop-types */
import { useState, useEffect } from "react";
import Button from "./Button";
import StatusMessage from "./StatusMessage";
import CheckNoBg from "../assets/Media/CheckNoBg.png";
import ErrorNoBg from "../assets/Media/ErrorNoBg.png";
import { useFormik } from "formik";
import { TEXT_STYLES, INPUT_STYLES } from "../constants/tailwindStyles";

import Select from "react-select";

const DynamicForm = ({
  setPopupVisibility,
  apiCmd,
  formFields,
  tempObject,
  triggerRerender,
}) => {
  const [isEditing, setIsEditing] = useState(false);
  const [showCheckMark, setShowCheckMark] = useState("");

  const handleToggleEditMode = () => {
    if (isEditing) {
      formik.handleSubmit();
    }

    setIsEditing(!isEditing);
  };

  const formik = useFormik({
    initialValues: tempObject || {},
    onSubmit: async (changedData) => {
      const updatedData = { ...tempObject, ...changedData };
      const errors = formik.validateForm(updatedData);
      if (Object.keys(errors).length === 0) {
        const response = await apiCmd(updatedData);
        if (response.ok) {
          triggerRerender();
          setIsEditing(false);
          setShowCheckMark("success");
        } else {
          setShowCheckMark("error");
        }
      }
    },
    validateOnChange: false,
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

  useEffect(() => {
    // Initialize formik with tempObject as initial values
    formik.setValues(tempObject || {});
  }, [tempObject]);

  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB]">
      <div className="mt-6 p-6 ml-4">
        <form onSubmit={formik.handleSubmit} className="space-y-4">
          <header className="flex justify-between">
            <h1 className="font-mainFont font-titleFontWeigt text-4xl">
              {/* voorlopig staat hier bewerk, dit moet nog dynamisch worden aan de hand van op welke knop je drukt --> misschien voor jou Mohamed aangezien jij deze component gemaakt hebt */}
              Bewerk
            </h1>
            <Button
              className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt"
              onClick={() => {
                setPopupVisibility("popupGoBack", true);
                setPopupVisibility("detailChange", false);
                setPopupVisibility("addItem", false);
                setIsEditing(false);
              }}
              type="button"
            >
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </header>

          {formFields.map((field) => (
            <div key={field.name} className="items-center flex flex-wrap ml-6">
              {/* in de lege classname moet je zelf is kijken die is voor de text voor u input velden idk welke kleur je wilt */}
              <div className="w-1/4">
                <label className={``} htmlFor={field.name}>
                  {field.label}:
                </label>
              </div>
              <div className="w-3/4">
                {field.type === "select" ? (
                  <Select
                    id={field.name}
                    name={field.name}
                    className={`w-1/2`}
                    isDisabled={!isEditing}
                    options={field.options}
                    isSearchable
                    placeholder="Search or Select..."
                    onChange={(selectedOption) => {
                      formik.setFieldValue(
                        field.name,
                        selectedOption ? selectedOption.value : null
                      );
                    }}
                    value={field.options.find(
                      (option) => option.value === formik.values[field.name]
                    )}
                  />
                ) : (
                  <input
                    id={field.name}
                    name={field.name}
                    type={field.type || "text"}
                    onChange={formik.handleChange}
                    value={formik.values[field.name]}
                    className={`${INPUT_STYLES.USERFORM_INPUT_BLACK} w-1/2`}
                    disabled={!isEditing}
                  />
                )}
              </div>

              {formik.errors[field.name] && (
                <div className="text-red-500">{formik.errors[field.name]}</div>
              )}
            </div>
          ))}

          <footer className="flex items-center mt-4">
            <StatusMessage
              type={showCheckMark}
              message={
                showCheckMark == "succes"
                  ? "Success!"
                  : "Error! Something went wrong."
              }
              successImage={CheckNoBg}
              errorImage={ErrorNoBg}
            />

            <div className="ml-auto">
              <Button
                className="w-28 h-[40px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-whiteText bg-blueBtn hover:bg-hoverBtn cursor-pointer"
                onClick={handleToggleEditMode}
                type="button"
              >
                {isEditing ? "Opslaan" : "Bewerk"}
              </Button>
            </div>
          </footer>
        </form>
      </div>
    </div>
  );
};

export default DynamicForm;
