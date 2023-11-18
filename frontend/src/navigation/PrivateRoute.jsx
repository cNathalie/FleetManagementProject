import {useNavigate} from "react-router-dom";
import React from "react";
import LoginPage from "../pages/LoginPage";
import ForbiddenPage from "../pages/ForbiddenPage";

const PrivateRoute = ({ component: Component, requiredRoles, parentRoutePath, ...rest }) => {
  const navigate = useNavigate();
  const isAuthenticated = localStorage.getItem("token") !== null;
  const userRole = localStorage.getItem("userRole");


  if (!isAuthenticated) {
    console.log("Not authorized, pleas login")
    // Redirect to login page if not authenticated
    navigate("/"); // Redirect to login page
    return null; // Render nothing until authentication is verified
  }

  if (!requiredRoles.includes(userRole)) {
    console.log(userRole);
    console.log("wrong role");
    // Redirect to a forbidden page or show a message
    return <ForbiddenPage/>; // Render nothing for unauthorized users
  }

  console.log("user is known and has rights")
  return Component;
};

export default PrivateRoute;
