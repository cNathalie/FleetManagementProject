/* eslint-disable react/prop-types */
import { useState, useEffect } from "react";
import Button from "./Button";
import StatusMessage from "./StatusMessage";
import CheckNoBg from "../assets/Media/CheckNoBg.png";
import ErrorNoBg from "../assets/Media/ErrorNoBg.png";
import { useFormik } from "formik";
import {
  TEXT_STYLES,
  INPUT_STYLES,
  BUTTON_STYLES,
  BG_STYLES,
} from "../constants/tailwindStyles";

import Select from "react-select";
import { useDarkMode } from "../hooks/useDarkMode";

const DynamicForm = ({
  setPopupVisibility,
  apiCmd,
  formFields,
  tempObject,
  triggerRerender,
  heading,
  btnValue,
  refGoBack,
  refDetailChange,
  refAddItem,
  refOverlay,
}) => {
  const [isEditing, setIsEditing] = useState(false);
  const [status, setStatus] = useState("");
  const [showStatus, setShowStatus] = useState(false);
  const [removeLeavePopup, setRemoveLeavePopup] = useState(true);
  const { isDarkMode, toggleDarkMode } = useDarkMode();

  const handleToggleEditMode = () => {
    if (isEditing) {
      formik.handleSubmit();
    }

    setIsEditing(!isEditing);
    setRemoveLeavePopup(false);
  };

  const toggleShowStatus = () => {
    setShowStatus(true);
    setTimeout(() => {
      setShowStatus(false);
    }, 3000);
  };

  const formik = useFormik({
    initialValues: tempObject || {},
    onSubmit: async (changedData) => {
      const updatedData = { ...tempObject, ...changedData };
      const errors = formik.validateForm(updatedData);
      if (Object.keys(errors).length === 0) {
        console.log(updatedData);
        const response = await apiCmd(updatedData);
        if (response.ok) {
          triggerRerender();
          setIsEditing(false);
          setRemoveLeavePopup(true);
          setStatus("succes");
          toggleShowStatus();
        } else {
          setStatus("error");
          toggleShowStatus();
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
    <div className={isDarkMode ? "dark" : ""}>
      <div className={BG_STYLES.OVERVIEW_BG}>
      <div className="mt-6 p-6 ml-4">
        <form
          onSubmit={formik.handleSubmit}
          className="space-y-4 relative pb-12"
        >
          <header className="flex justify-between">
            <h1 className={TEXT_STYLES.OVERVIEW_TITLE}>{heading}</h1>
            <Button
              className={BUTTON_STYLES.OVERVIEW_EXITBUTTON}
              onClick={() => {
                if (!removeLeavePopup) {
                  setPopupVisibility(refGoBack, true);
                  setPopupVisibility(refDetailChange, false);
                  setPopupVisibility(refAddItem, false);
                  setIsEditing(false);
                  setRemoveLeavePopup(true);
                } else {
                  setPopupVisibility(refDetailChange, false);
                  setPopupVisibility(refAddItem, false);
                  setPopupVisibility(refOverlay, false);
                  setIsEditing(false);
                }
              }}
              type="button"
            >
              <img src={`${isDarkMode ? "../src/assets/Media/dark_closeBtn.png" : "../src/assets/Media/closeButton.jpg"}`} alt="Close" />
            </Button>
          </header>
          {formFields.map((field) => (
            <div
              key={field.name}
              className="items-center flex flex-wrap ml-1 relative"
            >
              <div className="w-1/4">
                <label
                  className={`${TEXT_STYLES.OVERVIEW_DATAHEADER}`}
                  htmlFor={field.name}
                >
                  {field.label}
                </label>
              </div>
              <div className="w-3/4">
                {isEditing ? (
                  field.type === "select" ? (
                    <Select
                      id={field.name}
                      name={field.name}
                      className={INPUT_STYLES.OVERVIEW_DROPDWN_INPUT}
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
                      className={`${INPUT_STYLES.OVERVIEW_INPUT} w-1/2`}
                      disabled={!isEditing}
                    />
                  )
                ) : (
                  <span className={TEXT_STYLES.OVERVIEW_DATAVALUE}>
                    {formik.values[field.name]}
                  </span>
                )}
              </div>
              {formik.errors[field.name] && (
                <div className="text-red-500 absolute right-30">
                  {formik.errors[field.name]}
                </div>
              )}
            </div>
          ))}
          <footer className="flex absolute right-0">
            {setStatus && (
              <StatusMessage
                status={status}
                successImage={CheckNoBg}
                errorImage={ErrorNoBg}
                isVisible={showStatus}
              />
            )}

            <div className="pl-3">
              <Button
                className={BUTTON_STYLES.OVERVIEW_EDITBUTTON}
                onClick={handleToggleEditMode}
                type="button"
              >
                {isEditing ? "Opslaan" : btnValue}
              </Button>
            </div>
          </footer>
        </form>
      </div>
    </div>
    </div>
    
  );
};

export default DynamicForm;
