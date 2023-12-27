/* eslint-disable no-unused-vars */
import React, { useRef } from "react";
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
import { Box } from "@mui/material";
import { useDarkMode } from "../hooks/useDarkMode";

const AdminPage = () => {
  const passErrRef = useRef();
  const generalErrRef = useRef();
  const succesRef = useRef();
  const removedUserRef = useRef();
  const overlayRef = useRef();
  const removeUserRef = useRef();
  const goBackRef = useRef();
  
  const { isDarkMode } = useDarkMode();
  return (
    <Box sx={
      isDarkMode
      ? { bgcolor: "#032030", color: "white", minHeight: "100vh"}
      : { bgcolor: "white", color: "black", minHeight: "100vh"}
    }>

  return (
    <div>
      <div style={{ display: "none" }} ref={passErrRef}>
        <PopupMessage msgRef={passErrRef}>

          Wachtwoorden komen niet overeen
        </PopupMessage>
      </div>
      <div style={{ display: "none" }} ref={generalErrRef}>
        <PopupMessage msgRef={generalErrRef}>
          Er liep iets mis, probeer opnieuw
        </PopupMessage>
      </div>
      <div style={{ display: "none" }} ref={succesRef}>
        <PopupMessage msgRef={succesRef}>Gebruiker toegevoegd</PopupMessage>
      </div>
      <div style={{ display: "none" }} ref={removedUserRef}>
        <PopupMessage msgRef={removedUserRef}>
          Gebruiker verwijderd
        </PopupMessage>
      </div>
      <div style={{ display: "none" }} id="overlay" ref={overlayRef}>
        <Overlay />
      </div>

      <div style={{ display: "none" }} id="popupGoBack" ref={goBackRef}>
        <Popup>
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
      </div>
      <div style={{ display: "none" }} id="popupRemoveUser" ref={removeUserRef}>
        <Popup>
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
      </div>

      <div>
        <AdminHeader refOverlay={overlayRef} refGoBack={goBackRef} />
      </div>
      <div>
        <AdminContent
          refPass={passErrRef}
          refSucces={succesRef}
          refGeneral={generalErrRef}
          refRemovedUser={removedUserRef}
          refOverlay={overlayRef}
          refRemoveUser={removeUserRef}
        />
      </div>
    </Box>
  );
};

export default AdminPage;
