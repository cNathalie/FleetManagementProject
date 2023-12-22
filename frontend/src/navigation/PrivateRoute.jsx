/* eslint-disable react/prop-types */
import { Navigate } from "react-router-dom";
import React, { useEffect } from "react";
import AdminOnlyPage from "../pages/errors/AdminOnlyPage";
import { loginInfo } from "../constants/loginInfo";
import useAuth from "../authentication/useAuth";

const PrivateRoute = ({ component: Component, requiredRoles, ...rest }) => {
  const { userRole, isLoading, verify, refreshAccessToken } = useAuth();
  console.log(userRole);

  //Verify userRole when reloading a page with valid tokens
  useEffect(() => {
    if (userRole === null) {
      verify();
    }
  }, []);

  //Refresh the token every 2 minutes when on a private route
  useEffect(() => {
    const intervalId = setInterval(async () => {
      await refreshAccessToken();
    }, 2 * 60 * 1000);
    return () => clearInterval(intervalId);
  });

  if (isLoading) {
    return <p>LOADING</p>;
  }

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
