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
    <Box
      sx={
        isDarkMode
          ? { bgcolor: "#032030", color: "white", minHeight: "100vh" }
          : { bgcolor: "white", color: "black", minHeight: "100vh" }
      }
    >
      <div>
        <div style={{ display: "none" }} ref={passErrRef}>
          <PopupMessage
            msgRef={passErrRef}
            msg={"Wachtwoorden komen niet overeen"}
          />
        </div>
        <div style={{ display: "none" }} ref={generalErrRef}>
          <PopupMessage
            msgRef={generalErrRef}
            msg={"Er liep iets mis, probeer opnieuw"}
          />
        </div>
        <div style={{ display: "none" }} ref={succesRef}>
          <PopupMessage msgRef={succesRef} msg={"Gebruiker toegevoegd"} />
        </div>
        <div style={{ display: "none" }} ref={removedUserRef}>
          <PopupMessage msgRef={removedUserRef} msg={"Gebruiker verwijderd"} />
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
                  popup={{
                    title: p.title,
                    text: p.text,
                    textBtnLeft: p.textBtnLeft,
                    textBtnRight: p.textBtnRight,
                  }}
                  refGoBack={goBackRef}
                  refOverlay={overlayRef}
                />
              );
            })}
          </Popup>
        </div>
        <div
          style={{ display: "none" }}
          id="popupRemoveUser"
          ref={removeUserRef}
        >
          <Popup>
            {textPopupVerwijderGebruiker.map((p) => {
              return (
                <PopupRemoveUser
                  key={p.id}
                  popup={{
                    title: p.title,
                    text: p.text,
                    textBtnLeft: p.textBtnLeft,
                    textBtnRight: p.textBtnRight,
                  }}
                  refRemoveUser={removeUserRef}
                  refOverlay={overlayRef}
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
      </div>
    </Box>
  );
};

export default AdminPage;
