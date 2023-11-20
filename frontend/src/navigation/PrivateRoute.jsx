/* eslint-disable react/prop-types */
import { Navigate } from "react-router-dom";
import React from "react";
import AdminOnlyPage from "../pages/errors/AdminOnlyPage";
import {
  sessionStorageItems,
  sessionStorageValues,
} from "../constants/sessionStorage";
import { loginInfo } from "../constants/loginInfo";

const PrivateRoute = ({ component: Component, requiredRoles, ...rest }) => {
  const isLoggedIn =
    sessionStorage.getItem(sessionStorageItems.isLoggedIn) ==
    sessionStorageValues.true;
  const isAuthenticated =
    sessionStorage.getItem(sessionStorageItems.token) !== null;
  const userRole = sessionStorage.getItem(sessionStorageItems.userRole);

  if (isLoggedIn && isAuthenticated && requiredRoles.includes(userRole)) {
    console.log("User is logged in and has rights");
    return Component;
  }

  if (isLoggedIn && isAuthenticated && !requiredRoles.includes(userRole)) {
    console.log(userRole);
    console.log("User is logged in but has no rights");

    return < AdminOnlyPage />;
  }

  const info = loginInfo.notLoggedIn;
  console.log("No user logged in, redirecting to loginpage");
  return <Navigate to="/" state={info}/>;
};

export default PrivateRoute;
