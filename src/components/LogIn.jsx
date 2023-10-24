import { useState } from "react";
import { loginFields } from "../constants/formFields";
import FormAction from "./FormAction";
import Input from "./Input";

const fields = loginFields;
let fieldsState = {};
fields.forEach((field) => (fieldsState[field.id] = ""));

export default function Login() {
  const [loginState, setLoginState] = useState(fieldsState);

  const handleChange = (e) => {
    setLoginState({ ...loginState, [e.target.id]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("login button clicked");

    // authenticateUser();
  };

  //Handle Login API Integration here
  const authenticateUser = () => {};

  return (
    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
      <div className="-space-y-px">
        {fields.map((field, index) => (
          <div 
          key={index} 
          className="p-5">
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
