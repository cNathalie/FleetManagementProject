/* eslint-disable react-hooks/exhaustive-deps */
// eslint-disable-next-line no-unused-vars
import React, { useEffect, useState } from "react";
import UserForm from "./UserForm";
import { removeUserField, signupFields } from "../constants/formFields";
import Button from "./Button";
import {
  BUTTON_STYLES,
  TEXT_STYLES,
  CARD_STYLES,
} from "../constants/tailwindStyles";
import { addUser, getAllUsers, removeUser } from "../constants/Api";
import useConfirmation from "../confirmation/useConfirmation";

export const AdminContent = () => {
  const { adminDecision, setAdminDecision } = useConfirmation();
  const [userToRemove, setUserToRemove] = useState(null);

  // Handle form submission for adding a user
  const handleAddUserSubmit = async (formData) => {
    if (formData.password !== formData.confirmPassword) {
      alert("Wachtwoorden komen niet overeen");
    }
    var result = await addUser(formData);
    alert(
      result ? "Gebruiker toegevoegd" : "Er liep iets mis, probeer opnieuw"
    );
  };

  // Function is called by handleRemoveUserSubmit
  const showPopUp = () => {
    const popup = document.getElementById("popupRemoveUser");
    const overlay = document.getElementById("overlay");
    popup.style.display = "block";
    overlay.style.display = "block";
  };

  // Showing the pop up and setting the userToRemove to the selected user
  // use Effect then handles the admin decision.
  const handleRemoveUserSubmit = async (formData) => {
    console.log("Pending confirmation to remove user:", formData.chosenUser);
    showPopUp();
    setUserToRemove(formData.chosenUser);
  };

  // To observe changes in adminDecision => if true then admin confirmed removal
  useEffect(() => {
    if (adminDecision !== null) {
      if (adminDecision) {
        console.log("Admin confirmed removal.");
        (async () => {
          const removal = await removeUser(userToRemove);
          console.log(removal + "User removed with succes.");
        })();
      } else {
        console.log("Admin aborted removal.");
      }
      setAdminDecision(null);
    }
  }, [adminDecision, setAdminDecision]);

  // Getting all users from server to fill dropdown with email-addresses
  const [users, setUsers] = useState([]);
  useEffect(() => {
    const getUsersData = async () => {
      try {
        const data = await getAllUsers();
        console.log(data);
        setUsers(data.map((user) => user.email));
      } catch (error) {
        console.log(error);
      }
    };
    getUsersData();
  }, []);

  return (
    <div className="flex justify-center">
      <div className="relative flex my-10 w-[784px] h-[604px]">
        <div className="w-[790px] h-[604px]">
          {/* Left side - add user portion of the screen */}
          <div
            className={`${CARD_STYLES.BLUE_CARD} left-0 absolute w-[324px] h-[604px] top-0`}
          >
            <div
              className={`${TEXT_STYLES.ADMIN_CARDTITLE} w-[200px] h-[100px]`}
            >
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
          <div
            className={`${CARD_STYLES.BLUE_CARD} right-0 absolute w-[324px] h-[604px] top-0`}
          >
            <div
              className={`${TEXT_STYLES.ADMIN_CARDTITLE} w-[200px] h-[100px] mt-5`}
            >
              Gebruiker verwijderen
            </div>
            {/* Remove user form */}
            <div className="w-full mt-23">
              <UserForm
                Data={users}
                formFields={removeUserField}
                onSubmit={handleRemoveUserSubmit}
                ButtonComponent={() => (
                  <div className="w-[60%] mb-5 flex items-center justify-center mt-44">
                    <Button
                      className={`${BUTTON_STYLES.ADMIN_REMOVE} w-full py-2`}
                    >
                      Verwijder
                    </Button>
                  </div>
                )}
              />
            </div>
          </div>
          {/* Dividing two portions of the screen */}
          <div
            className={`${TEXT_STYLES.ADMIN_OR} absolute w-[36px] top-[294px] left-[374px]`}
          >
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
