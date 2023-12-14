import Axios from "axios";
import {
  sessionStorageItems,
  sessionStorageValues,
} from "../constants/sessionStorage";
import { baseUrl } from "../constants/Api";
import { Navigate } from "react-router-dom";

export async function login(email, password) {
  try {
    const response = await Axios.post(
      baseUrl + "accounts/authenticate",
      {
        email: email,
        password: password,
      },
      { headers: { "Content-Type": "application/json" } }
    );

    const { accessToken, refreshToken, role } = response.data;
    
    sessionStorage.setItem(sessionStorageItems.accessToken, accessToken);
    sessionStorage.setItem(sessionStorageItems.refreshToken, refreshToken)
    sessionStorage.setItem(sessionStorageItems.userRole, role);
    sessionStorage.setItem(
      sessionStorageItems.isLoggedIn,
      sessionStorageValues.true
    );

    console.log("Login authorized by API");
    console.log("token " + accessToken);
    return { succes: true, role };

  } catch (error) {
    console.error("Login failed: ", error.response?.data);
    return { succes: false, error: error.response?.data };
  }
}

export function logout() {
  console.log(
    "Closing session, deleting token and userole from sessionStorage"
  );
  sessionStorage.setItem(sessionStorageItems.accesToken, null);
  sessionStorage.setItem(sessionStorageItems.userRole, null);
  sessionStorage.setItem(
    sessionStorageItems.isLoggedIn,
    sessionStorageValues.false
  );
}
