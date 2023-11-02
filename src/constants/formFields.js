const loginFields = [
  {
    labelText: "Email adress",
    labelFor: "email-address",
    id: "email-address",
    name: "email",
    type: "email",
    autoComplete: "email",
    isRequired: true,
    placeholder: "Voer in...",
  },
  {
    labelText: "Wachtwoord",
    labelFor: "password",
    id: "password",
    name: "password",
    type: "password",
    autoComplete: "current-password",
    isRequired: true,
    placeholder: "Voer in...",
  },
];

const signupFields = [
  {
    labelText: "Username",
    labelFor: "username",
    id: "username",
    name: "username",
    type: "text",
    autoComplete: "username",
    isRequired: true,
    placeholder: "Username",
  },
  {
    labelText: "E-mail",
    labelFor: "email-address",
    id: "email-address",
    name: "email",
    type: "email",
    autoComplete: "email",
    isRequired: true,
    placeholder: "Email address",
  },
  {
    labelText: "Wachtwoord",
    labelFor: "password",
    id: "password",
    name: "password",
    type: "password",
    autoComplete: "current-password",
    isRequired: true,
    placeholder: "Password",
  },
  {
    labelText: "Wachtwoord bevestigen",
    labelFor: "confirm-password",
    id: "confirm-password",
    name: "confirmPassword",
    type: "password",
    autoComplete: "confirm-password",
    isRequired: true,
    placeholder: "Confirm Password",
  },
  {
    labelText: "Admin?",
    labelFor: "admin-privileges",
    id: "admin-privileges",
    name: "isAdmin",
    type: "checkbox",
  },
];

export { loginFields, signupFields };
