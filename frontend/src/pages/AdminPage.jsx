/* eslint-disable no-unused-vars */
import React from "react";
import AdminHeader from "../components/AdminHeader";
import { AdminContent } from "../components/AdminContent";
import {
  textPopupGaTerug,
  textPopupVerwijderGebruiker,
} from "../constants/textPopups";
import PopupGoBack from "../components/PopupGoBack";
import Overlay from "../components/Overlay";
import Popup from "../components/Popup";
import PopupRemoveUser from "../components/PopupRemoveUser";
import PopupMessage from "../components/PopupMessage";

const AdminPage = () => {
  return (
    <div>
      <div style={{ display: "none" }} id="passErr">
        <PopupMessage title="passErr">
          Wachtwoorden komen niet overeen
        </PopupMessage>
      </div>
      <div style={{ display: "none" }} id="generalErr">
        <PopupMessage title="generalErr">
          Er liep iets mis, probeer opnieuw
        </PopupMessage>
      </div>
      <div style={{ display: "none" }} id="succes">
        <PopupMessage title="succes">Gebruiker toegevoegd</PopupMessage>
      </div>
      <div style={{ display: "none" }} id="removedUser">
        <PopupMessage title="removedUser">Gebruiker verwijderd</PopupMessage>
      </div>
      <Overlay />
      <Popup id="popupGoBack">
        {textPopupGaTerug.map((p) => {
          return (
            <PopupGoBack
              key={p.id}
              title={p.title}
              text={p.text}
              textBtnLeft={p.textBtnLeft}
              textBtnRight={p.textBtnRight}
              popup={p}
            />
          );
        })}
      </Popup>
      <Popup id="popupRemoveUser">
        {textPopupVerwijderGebruiker.map((p) => {
          return (
            <PopupRemoveUser
              key={p.id}
              title={p.title}
              text={p.text}
              textBtnLeft={p.textBtnLeft}
              textBtnRight={p.textBtnRight}
              popup={p}
            />
          );
        })}
      </Popup>
      <div>
        <AdminHeader />
      </div>
      <div>
        <AdminContent />
      </div>
    </div>
  );
};

export default AdminPage;
