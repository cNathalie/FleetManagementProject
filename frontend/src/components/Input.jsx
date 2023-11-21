import { INPUT_STYLES } from "../constants/tailwindStyles";

/* eslint-disable react/prop-types */
// const fixedInputClass =
//   " appearance-none relative block  w-full px-3 py-2 border-b bg-transparent border-blueText placeholder-blueText text-gray-900";

export default function Input({
  handleChange,
  value,
  labelText,
  labelFor,
  id,
  name,
  type,
  isRequired = true,
  placeholder,
  customClass,
}) {
  return (
    <input
      key={id}
      onChange={handleChange}
      defaultValue={value}
      id={id}
      name={name}
      type={type}
      required={isRequired}
      className={INPUT_STYLES.LOGIN_INPUT}
      placeholder={placeholder}
      autoComplete="on"
      style={{
        background: "transparent" /* wilt alleen via style werken :-( ) */,
      }}
    />
  );
}
