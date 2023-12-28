import React, { useRef } from "react";
import Nav from "../components/Nav";
import { Outlet, useActionData } from "react-router-dom";
import useAuth from "../authentication/useAuth";
import { Box } from "@mui/material";
import { useDarkMode } from "../hooks/useDarkMode";

const RootLayout = () => {
  const { isDarkMode } = useDarkMode();
  const navBtnRef = useRef();

  return (
    <div>
      <Box
        sx={
          isDarkMode
            ? { bgcolor: "#032030", color: "white" }
            : { bgcolor: "white", color: "black" }
        }
      >
        <Nav navBtnRef={navBtnRef} />
        <Outlet context={navBtnRef} />
      </Box>
    </div>
  );
};

export default RootLayout;
