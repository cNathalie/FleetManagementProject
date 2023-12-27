/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import React, { useState } from "react";
import Button from "./Button";
import {
  TEXT_STYLES,
  INPUT_STYLES,
  BUTTON_STYLES,
} from "../constants/tailwindStyles";
import { useDarkMode } from "../hooks/useDarkMode";

const UserForm = ({ formFields, onSubmit, buttonText, ButtonComponent, Data }) => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    isAdmin: false,
    chosenUser: ""
  });

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    if (name === "chosenUser") {
      setFormData({
        ...formData,
        [name]: value,
      });
    } else {
      setFormData({
        ...formData,
        [name]: type === "checkbox" ? checked : value,
      });
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formData);
  };

  const { isDarkMode, toggleDarkMode } = useDarkMode();
  return (
    <div className={isDarkMode ? "dark" : ""}>
      <form className="w-full mx-auto" onSubmit={handleSubmit}>
      {formFields.map((field) => (
        <div className="mb-4" key={field.id}>
          <label htmlFor={field.id} className={TEXT_STYLES.USERFORM_LABEL}>
            {field.labelText}
          </label>
          {field.type === "checkbox" ? (
            <input
              type="checkbox"
              id={field.id}
              name={field.name}
              checked={formData[field.name]}
              onChange={handleInputChange}
              className="mr-2"
            />
          ) : field.type === "dropdown" ? 
          (
            
            <select 
              id={field.id}
              name={field.name}
       
              value={formData[field.name]}
              onChange={handleInputChange}
              className={INPUT_STYLES.USERFORM_INPUT + " w-full "}
            >
              {Data.map( (email, index) => {
                return <option 
                key={index} 
                value={email}
                className="bg-blueText">
                  {email}
                </option>
              })}
            </select>

          )
          :(
            <input
              type={field.type}
              id={field.id}
              name={field.name}
              value={formData[field.name]}
              onChange={handleInputChange}
              className={`${INPUT_STYLES.USERFORM_INPUT} w-full px-3 py-2`}
              placeholder={field.placeholder}
            />
          )}
        </div>
      ))}
      <div className="w-full mb-2 flex items-center justify-center">
        <ButtonComponent
          type="submit"
          className={`${BUTTON_STYLES.USERFORM_SUBMIT} w-[60%] py-2 mt-4`}
        >
          {buttonText}
        </ButtonComponent>
      </div>
    </form>
    </div>
    
  );
};

export default UserForm;
