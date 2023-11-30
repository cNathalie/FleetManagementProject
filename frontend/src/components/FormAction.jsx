
import { BUTTON_STYLES } from "../constants/tailwindStyles";


export default function FormAction({
  handleSubmit,
  type = "Button",
  action = "submit",
  text,
}) {
  return (
    <>
      {type === "Button" ? (
        <button
          type={action}
          className={`${BUTTON_STYLES.LOGIN_SUBMIT} relative w-full flex justify-center py-2 px-4`}
          onClick={handleSubmit}
        >
          {text}
        </button>
      ) : (
        <></>
      )}
    </>
  );
}
