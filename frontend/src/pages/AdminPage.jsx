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

const AdminPage = () => {
  return (
    <div>
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
