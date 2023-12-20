import React, { createContext, useEffect, useState } from "react";
import { baseUrl } from "../constants/Api";
import Axios from "axios";

export const AuthContext = createContext();

const AuthContextProvider = (props) => {
  const [userRole, setUserRole] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  const refreshAccessToken = async () => {
    try {
      const response = await Axios.post(
        baseUrl + "accounts/refresh-token",
        {},
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );
      console.log(response.data);
    } catch (error) {
      logout();
    } 
  };

  //Refresh the token every 4 minutes
  useEffect(() => {
    setInterval(() => {
      refreshAccessToken();
    }, 4* 60 *1000);
  }, []);

  // Authentication
  async function login(email, password) {
    setIsLoading(true);
    try {
      const response = await Axios.post(
        baseUrl + "accounts/authenticate",
        {
          email: email,
          password: password,
        },
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );

      setUserRole(response.data.role);

      console.log("Login authorized by API");

      return { succes: true };
    } catch (error) {
      console.error("Login failed: ", error);
      return { succes: false, error: error.response?.data };
    } finally {
      setIsLoading(false);
    }
  }

  // Log out: server removes cookies from browser and refresh token from db
  async function logout() {
    console.log("Closing session");
    try {
      var response = await Axios.post(
        baseUrl + "accounts/logout",
        {},
        {
          withCredentials: true,
        }
      );
      console.log(response.data);
      setUserRole(null);
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <AuthContext.Provider
      value={{ login, logout, userRole, isLoading }}
    >
      {props.children}
    </AuthContext.Provider>
  );
};

export default AuthContextProvider;
