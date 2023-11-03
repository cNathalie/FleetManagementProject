// eslint-disable-next-line no-unused-vars
import React, { useState } from 'react'
import { signupFields } from '../constants/formFields'


export const AdminContent = () => {

  const [formData, setFormData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    isAdmin: false // Checkbox initially set to false
  });

  // Handles input change for form
  const handleInputChange = (e) => {
    const {name, value, type, checked} = e.target;
    setFormData({
      ...formData,
      [name]: type === 'checkbox' ? checked : value,
    });
  };

  // Handle form submission
  const handleSubmit = (e) => {
    e.preventDefault();
    // Here you have to replace the console.log with the form submission later
    console.log(formData);
  }

  const handleRemoveUserSubmit = (e) => {
    e.preventDefault();
    // Handle the form submission for removing a user
    console.log('Remove User:', formData.removeEmail);
  };

  return (
    <div className="flex justify-center">
    <div className="relative flex my-10 w-[784px] h-[604px]">
      <div className="w-[790px] h-[604px]">
      {/* Left side - add user portion of the screen */}
        <div className="left-0 flex justify-center absolute w-[324px] h-[604px] top-0 bg-[#19b9ce] rounded-[10px] flex-col items-center shadow-2xl">
          <div className="w-[200px] h-[100px] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-[#ffffff] text-[25px] text-center wrap">
            Gebruiker toevoegen
          </div>
          {/* Add user form */}
          <div>
            <form className="w-full" onSubmit={handleSubmit}>
              {signupFields.map((field) => {
                if (field.name !== 'username') {
                  return (
                    <div className="mb-4" key={field.id}>
                      <label
                        htmlFor={field.id}
                        className="block text-[#ffffff] font-semibold mb-2"
                      >
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
                          className="w-full px-3 py-2 border-b bg-[#ffffff00] border-[#0b5a64] text-[#0b5a64] placeholder-[#ffffff7a] focus:outline-none focus:ring-2 focus:ring-[#19b9ce]"
                          placeholder={field.placeholder}
                        />
                      )}
                    </div>
                  );
                }
                return null;
              })}
            </form>
          </div>
          <div className="w-[60%] mb-2">
              <button
                type="submit"
                className="w-full py-2 mt-4 rounded-[10px] bg-[#ffffff] border border-solid border-[#0b5a64] font-semibold hover:bg-[#0b5a64] hover:text-[#ffffff] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-[#0b5a64] text-[15px] text-center"
              >
                Voeg toe
              </button>
          </div>
        </div>
        {/* Right side - remove user portion of the screen */}
        <div className="right-0 flex absolute w-[324px] h-[604px] top-0 bg-[#19b9ce] rounded-[10px] flex-col items-center shadow-2xl">
          <div className="w-[200px] h-[100px] mt-12 [font-family:'Inter-SemiBold',Helvetica] font-semibold text-[#ffffff] text-[25px] text-center wrap">
          Gebruiker verwijderen
          </div>
          {/* Remove user form */}
          <div className="mt-20">
          <form className="w-full" onSubmit={handleRemoveUserSubmit}>
            <div className="mb-4 mt-3">
              <label htmlFor="remove-email" className="block text-[#ffffff] font-semibold mb-2">
                E-mail
              </label>
              <input
                type="email"
                id="remove-email"
                name="removeEmail"
                value={formData.removeEmail}
                onChange={handleInputChange}
                className="w-full px-3 py-2 border-b bg-[#ffffff00] border-[#0b5a64] text-[#0b5a64] placeholder-[#ffffff7a] focus:outline-none focus:ring-2 focus:ring-[#19b9ce]"
                placeholder="Email Adress"
              />
            </div>
          </form>
          </div>
          <div className="w-[60%] mt-40">
            <button
              type="submit"
              className="w-full py-2 mt-4 rounded-[10px] bg-[#ffffff] border border-solid border-[#0b5a64] font-semibold hover:bg-[#0b5a64] hover:text-[#ffffff] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-[#0b5a64] text-[15px] text-center"
            >
              Verwijder
            </button>
            </div>
        </div>
        {/* Dividing two portions of the screen */}
        <div className="absolute w-[36px] top-[294px] left-[374px] [font-family:'Inter',Helvetica] font-semibold text-[#0b5a64] text-[18px] text-center tracking-[0] leading-[normal] whitespace-nowrap">
          OF
        </div>
        {/* Two lines to divide the add-user on the left and the remove-user on the right */}
        <img
          className="top-0 absolute w-px h-[278px] left-[391px] object-cover"
          alt="Line"
          src="https://c.animaapp.com/KarkG0Dg/img/line-3.svg"
        />
        <img
          className="top-[326px] absolute w-px h-[278px] left-[391px] object-cover"
          alt="Line"
          src="https://c.animaapp.com/KarkG0Dg/img/line-4.svg"
        />
      </div>
    </div>
    </div>
  )
}
