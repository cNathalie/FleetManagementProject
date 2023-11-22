import React from "react";
import { useNavigate } from "react-router-dom";
import Button from "./Button";
import { logout } from "../constants/functions";

const LogOutButton = () => {
  /* In this code snippet, `useNavigate` is a hook provided by the `react-router-dom` library. It returns
a navigate function that can be used to programmatically navigate to different routes in your
application. */
  const navigate = useNavigate();

  const onClick = () => {
    logout();
    console.log("Navigating to loginpage");
    navigate("/");
  };

  return (
    <>
      <Button
        onClick={() => onClick()}
        className="px-2 py-2 rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText [font-family:'Inter-SemiBold',Helvetica] font-semibold text-darkBlue text-adminBtnFontSize text-center"
      >
        <span>Afmelden</span>
      </Button>
    </>
  );
};

export default LogOutButton;
