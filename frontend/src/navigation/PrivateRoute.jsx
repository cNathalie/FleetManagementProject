/* eslint-disable react/prop-types */
import { Navigate } from "react-router-dom";
import React, { useEffect } from "react";
import AdminOnlyPage from "../pages/errors/AdminOnlyPage";
import { loginInfo } from "../constants/loginInfo";
import useAuth from "../authentication/useAuth";

const PrivateRoute = ({ component: Component, requiredRoles, ...rest }) => {
  const { userRole, isLoading, refreshAccessToken } = useAuth();
  console.log(userRole);

  // if(isLoading){
  //   return <p>LOADING</p>
  // }

  //Refresh the token every 4 minutes
  useEffect(() => {
    setInterval(() => {
      refreshAccessToken();
    }, 10000);
  });

  if (userRole !== null && requiredRoles.includes(userRole)) {
    console.log("User is logged in and has rights");
    return Component;
  }

  if (userRole !== null && !requiredRoles.includes(userRole)) {
    console.log(userRole);
    console.log("User is logged in but has no rights");

    return <AdminOnlyPage />;
  }

  const info = loginInfo.notLoggedIn;
  console.log("No user logged in, redirecting to loginpage");
  return <Navigate to="/" state={info} />;
};

export default PrivateRoute;
