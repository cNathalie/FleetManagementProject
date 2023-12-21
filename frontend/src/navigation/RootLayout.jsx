import React from "react";
import Nav from "../components/Nav";
import { Outlet, useActionData } from "react-router-dom";
import useAuth from "../authentication/useAuth";
import { Outlet } from "react-router-dom";
import { Box } from "@mui/material";
import { useDarkMode } from "../hooks/useDarkMode";

const RootLayout = () => {
  const { isDarkMode } = useDarkMode();

  return (
    <div>
      <Box
      sx={
        isDarkMode
        ? { bgcolor: "#032030", color: "white"}
        : { bgcolor: "white", color: "black"}
      }>
        <Nav />
        <Outlet />
      </Box>
    </div>
  );
};

export default RootLayout;
