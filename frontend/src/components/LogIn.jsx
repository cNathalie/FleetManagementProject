import { useState, useEffect } from "react";
import { loginFields } from "../constants/formFields";
import FormAction from "./FormAction";
import Input from "./Input";
import { useNavigate } from "react-router-dom";
import baseUrl from "../constants/baseUrl";
import Axios from "axios";
import {sessionStorageItems, sessionStorageValues} from "../constants/sessionStorage"

const fields = loginFields;
let fieldsState = {};
fields.forEach((field) => (fieldsState[field.id] = ""));

export default function Login() {
  const [loginState, setLoginState] = useState(fieldsState);


  const handleChange = (e) => {
    setLoginState({ ...loginState, [e.target.id]: e.target.value });
  };

  const login = async (email, password) => {

    try {
      const response = await Axios.post(
        baseUrl + "Auth/login",
        {
          email: email,
          wachtwoord: password,
        },
        { headers: { "Content-Type": "application/json" } }
      );

      const { token, role } = response.data;

      sessionStorage.setItem(sessionStorageItems.token, token);
      sessionStorage.setItem(sessionStorageItems.userRole, role);
      sessionStorage.setItem(sessionStorageItems.isLoggedIn, sessionStorageValues.true)

      console.log("Login authorized by API")
      return { succes: true, role };

    } catch (error) {
      console.error("Login failed: ", error.response?.data);
      return { succes: false, error: error.response?.data };
    }
  };

  const handleSubmit = async (e) => {

    e.preventDefault();
    const { succes, role, error } = await login(
      loginState["email-address"],
      loginState["password"]
    );

    if(succes) {
      navigate(`/home`);
    } else{
      navigate('/')
      console.error("Login failed: ", error)
    }

  };
    
  

  // const getLogins = async () => {
  //   try {
  //     const response = await fetch(baseUrl + "Logins");

  //     if (!response.ok) {
  //       throw new Error(`Request failed with status: ${response.status}`);
  //     }
  //     console.log("GET successful");
  //     const data = await response.json();
  //     setLoginsState(data);
  //   } catch (error) {
  //     console.error(error);
  //   }
  // };

  // const handleSubmit = (e) => {
  //   e.preventDefault();
  //   console.log("login button clicked, authenticating user");
  //   setButtenState(true);
  //   authenticateUser(loginState["email-address"], loginState["password"]);
  //   setButtenState(false);
  // };

  // useEffect(() => {
  //   getLogins();
  // }, [buttonState]);

  const navigate = useNavigate();

  //Handle Login
  // const authenticateUser = (email, password) => {
  //   const loginExists = loginsState.some(
  //     (l) => l.email == email && l.wachtwoord == password
  //   );

  //   if (loginExists) {
  //     console.log("login bestaat");
  //     navigate(`/home`);
  //   } else {
  //     console.log("login bestaat niet");
  //   }
  // };

  return (
    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
      <div className="-space-y-px">
        {fields.map((field, index) => (
          <div key={index} className="p-5">
            <label
              htmlFor={field.labelFor}
              className="text-left font-mainFont font-titleFontWeigt text-titleFontSize text-blueText"
            >
              {field.labelText}
            </label>

            <Input
              handleChange={handleChange}
              value={loginState[field.id]}
              labelText={field.labelText}
              labelFor={field.labelFor}
              id={field.id}
              name={field.name}
              type={field.type}
              isRequired={field.isRequired}
              placeholder={field.placeholder}
            />
          </div>
        ))}
      </div>
      <FormAction handleSubmit={handleSubmit} text="Login" />
    </form>
  );
}
