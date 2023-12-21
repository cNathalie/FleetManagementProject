export const loginFields = [
  {
    labelText: "Email",
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

export const signupFields = [
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

export const removeUserField = [
  {
    type: "dropdown",
    id: "email-dropdown",
    name: "chosenUser",
  },
];

export const initialVoertuigFormData = [
  {
    voertuigId: 0,
    merkEnModel: "string",
    chassisnummer: "string",
    nummerplaat: "5-JWQ-162",
    brandstoftype: "string",
    typewagen: "string",
    kleur: "string",
    aantalDeuren: 5,
  },
];

export const initialTankkaartFormData = [
  {
    tankkaartId: 0,
    kaartnummer: 0,
    geldigheidsdatum: "2023-11-21T17:08:58.155Z",
    pincode: 0,
    brandstofType: "string",
    isActief: true,
  },
];

export const initialBestuurdersFormData = [
  {
    bestuurderId: 0,
    naam: "string",
    voornaam: "string",
    adres: "string",
    rijksregisternummer: "string",
    rijbewijs: "string",
  },
];

export const initialFleetsFormData = [
  {
    fleetMemberId: 0,
    bestuurderNaam: "string",
    bestuurderVoornaam: "string",
    tankkaartId: 0,
    voertuigMerkModel: "string",
    voertuigNummerplaat: "string",
    voertuigChassisnummer: "string",
  },
];
