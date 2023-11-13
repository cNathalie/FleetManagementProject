// eslint-disable-next-line no-unused-vars
import React, { useState } from 'react'
import UserForm from './UserForm';
import { signupFields } from '../constants/formFields'
import Button from './Button';


export const AdminContent = () => {

  const handleAddUserSubmit = (formData) => {
    // Handle form submission for adding a user
    console.log('Add User:', formData);
  }

  const handleRemoveUserSubmit = (formData) => {
    // Handle the form submission for removing a user
    console.log('Remove User:', formData.removeEmail);
  };

  return (
    <div className="flex justify-center">
      <div className="relative flex my-10 w-[784px] h-[604px]">
        <div className="w-[790px] h-[604px]">
          {/* Left side - add user portion of the screen */}
          <div className="left-0 flex justify-center absolute w-[324px] h-[604px] top-0 bg-blueBtn rounded-[10px] flex-col items-center shadow-2xl">
            <div className="w-[200px] h-[100px] [font-family:'Inter-SemiBold',Helvetica] font-semibold text-whiteText text-adminHeader text-center wrap">
              Gebruiker toevoegen
            </div>
            {/* Add user form */}
            <UserForm 
            formFields={signupFields.filter((field) => field.name !== 'username')} 
            onSubmit={handleAddUserSubmit} 
            buttonText="Voeg toe"
            ButtonComponent={Button} //Default button component
            />
          </div>
          {/* Right side - remove user portion of the screen */}
          <div className="right-0 flex absolute w-[324px] h-[604px] top-0 bg-blueBtn rounded-[10px] flex-col items-center shadow-2xl">
            <div className="w-[200px] h-[100px] mt-12 [font-family:'Inter-SemiBold',Helvetica] font-semibold text-whiteText text-adminHeader text-center wrap">
              Gebruiker verwijderen
            </div>
            {/* Remove user form */}
            <div className="w-full mt-23">
              <UserForm 
              formFields={signupFields.filter((field) => field.name === 'email')} 
              onSubmit={handleRemoveUserSubmit} 
              buttonText="Verwijder"
              ButtonComponent={(props) => (
                <div className="w-[60%] mb-5 flex items-center justify-center mt-44">
                  <Button {...props} className="w-full py-2 rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText [font-family:'Inter-SemiBold',Helvetica] font-semibold text-darkBlue text-adminBtnFontSize text-center">
                    {props.children}
                  </Button>
                </div>
              )}
              />
            </div>
          </div>
          {/* Dividing two portions of the screen */}
          <div className="absolute w-[36px] top-[294px] left-[374px] [font-family:'Inter',Helvetica] font-semibold text-darkBlue text-[18px] text-center tracking-[0] leading-[normal] whitespace-nowrap">
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
