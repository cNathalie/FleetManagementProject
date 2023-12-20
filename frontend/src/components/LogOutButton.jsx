// eslint-disable-next-line no-unused-vars
import React from "react";
import { useNavigate } from "react-router-dom";
import Button from "./Button";

import { BUTTON_STYLES } from "../constants/tailwindStyles";
import useAuth from "../authentication/useAuth";

const LogOutButton = () => {
  /* In this code snippet, `useNavigate` is a hook provided by the `react-router-dom` library. It returns
a navigate function that can be used to programmatically navigate to different routes in your
application. */
  const navigate = useNavigate();
  const {logout} = useAuth();
  const onClick = async () => {
    await logout();
    console.log("Navigating to loginpage");
    navigate("/");
  };

  return (
    <>
      <Button
        onClick={() => onClick()}
        className={`${BUTTON_STYLES.NAV_LOGOUTBUTTON} px-2 py-2`}
      >
        <span>Afmelden</span>
      </Button>
    </>
  );
};

export default LogOutButton;
