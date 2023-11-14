/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import React, { useState} from 'react'
import Button from './Button';

const UserForm = ({ formFields, onSubmit, buttonText, ButtonComponent}) => {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    isAdmin: false,
  });

  const handleInputChange = (e) => {
    const { name, value, type, checked} = e.target;
    setFormData({
        ...formData,
        [name]: type === 'checkbox' ? checked : value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <form className="w-[80%] mx-auto" onSubmit={handleSubmit}>
      {formFields.map((field) => (
        <div className="mb-4" key={field.id}>
          <label htmlFor={field.id} className="block text-whiteText font-semibold mb-2">
            {field.labelText}
          </label>
          {field.type === 'checkbox' ? (
            <input
              type="checkbox"
              id={field.id}
              name={field.name}
              checked={formData[field.name]}
              onChange={handleInputChange}
              className="mr-2"
            />
          ) : (
            <input
              type={field.type}
              id={field.id}
              name={field.name}
              value={formData[field.name]}
              onChange={handleInputChange}
              className="w-full px-3 py-2 border-b bg-transparent border-whiteText text-whiteText placeholder-adminPlaceHolder focus:outline-none focus:ring-2 focus:ring-whiteText focus:border-whiteText rounded-md"
              placeholder={field.placeholder}
            />
          )}
        </div>
      ))}
      <div className="w-full mb-2 flex items-center justify-center">
        <ButtonComponent
          type="submit"
          className="w-[60%] py-2 mt-4 rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText [font-family:'Inter-SemiBold',Helvetica] font-semibold text-darkBlue text-adminBtnFontSize text-center"
        >
          {buttonText}
        </ButtonComponent>
      </div>
    </form>
  );
};

export default UserForm