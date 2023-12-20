import React from "react";
import Nav from "../components/Nav";
import { Outlet, useActionData } from "react-router-dom";
import useAuth from "../authentication/useAuth";

const RootLayout = () => {


  

  return (
    <div>
      <Nav />
      <Outlet />
    </div>
  );
};

export default RootLayout;
