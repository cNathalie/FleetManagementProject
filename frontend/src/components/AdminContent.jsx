// eslint-disable-next-line no-unused-vars
import React, { useState } from "react";
import UserForm from "./UserForm";
import { signupFields } from "../constants/formFields";
import Button from "./Button";
import { BUTTON_STYLES, TEXT_STYLES, CARD_STYLES } from "../constants/tailwindStyles";

export const AdminContent = () => {
  const handleAddUserSubmit = (formData) => {
    // Handle form submission for adding a user
    console.log("Add User:", formData);
  };

  const handleRemoveUserSubmit = (formData) => {
    // Handle the form submission for removing a user
    console.log("Remove User:", formData.removeEmail);
  };

  return (
    <div className="flex justify-center">
      <div className="relative flex my-10 w-[784px] h-[604px]">
        <div className="w-[790px] h-[604px]">
          {/* Left side - add user portion of the screen */}
          <div className={`${CARD_STYLES.BLUE_CARD} left-0 absolute w-[324px] h-[604px] top-0`}>
            <div className={`${TEXT_STYLES.ADMIN_CARDTITLE} w-[200px] h-[100px]`}>
              Gebruiker toevoegen
            </div>
            {/* Add user form */}
            <UserForm
              formFields={signupFields.filter(
                (field) => field.name !== "username"
              )}
              onSubmit={handleAddUserSubmit}
              buttonText="Voeg toe"
              ButtonComponent={Button} //Default button component
            />
          </div>
          {/* Right side - remove user portion of the screen */}
          <div className={`${CARD_STYLES.BLUE_CARD} right-0 absolute w-[324px] h-[604px] top-0`}>
            <div className={`${TEXT_STYLES.ADMIN_CARDTITLE} w-[200px] h-[100px] mt-5`}>
              Gebruiker verwijderen
            </div>
            {/* Remove user form */}
            <div className="w-full mt-23">
              <UserForm
                formFields={signupFields.filter(
                  (field) => field.name === "email"
                )}
                onSubmit={handleRemoveUserSubmit}
                ButtonComponent={() => (
                  <div className="w-[60%] mb-5 flex items-center justify-center mt-44">
                    <Button
                      className={`${BUTTON_STYLES.ADMIN_REMOVE} w-full py-2`}
                      onClick={() => {
                        const popup =
                          document.getElementById("popupRemoveUser");
                        const overlay = document.getElementById("overlay");
                        popup.style.display = "block";
                        overlay.style.display = "block";
                      }}
                    >
                      Verwijder
                    </Button>
                  </div>
                )}
              />
            </div>
          </div>
          {/* Dividing two portions of the screen */}
          <div className={`${TEXT_STYLES.ADMIN_OR} absolute w-[36px] top-[294px] left-[374px]`}>
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
  );
};
