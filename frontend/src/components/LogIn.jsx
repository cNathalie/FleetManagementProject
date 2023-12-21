import { useState, useEffect } from "react";
import { loginFields } from "../constants/formFields";
import FormAction from "./FormAction";
import Input from "./Input";
import { useNavigate } from "react-router-dom";
import { loginInfo } from "../constants/loginInfo";
import { TEXT_STYLES } from "../constants/tailwindStyles";
import useAuth from "../authentication/useAuth";

const fields = loginFields;
let fieldsState = {};
fields.forEach((field) => (fieldsState[field.id] = ""));

export default function Login() {

  const {login} = useAuth();
  const [loginState, setLoginState] = useState(fieldsState);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setLoginState({ ...loginState, [e.target.id]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const { succes, role, error } = await login(
      loginState["email-address"],
      loginState["password"]
    );

    if (succes) {
      navigate(`/home`);
    } else {
      navigate("/", { state: loginInfo.loginUnknown });
      console.error("Login failed: ", error);
    }
  };

  return (
    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
      <div className="-space-y-px">
        {fields.map((field, index) => (
          <div key={index} className="p-5">
            <label
              htmlFor={field.labelFor}
              className={TEXT_STYLES.LOGIN_LABEL}
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
