import {
  BUTTON_STYLES,
  CARD_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";
import Button from "./Button";
import { useDarkMode } from "../hooks/useDarkMode";
/* eslint-disable react/prop-types*/

const PopupRemoveItem = ({
  popup,
  apiFunction,
  setPopupVisibility,
  tempId,
  triggerRerender,
  refOverlay,
  refRemoveItem,
}) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;
  //const { data } = useData();

  const { isDarkMode, toggleDarkMode } = useDarkMode();
  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div className={`${CARD_STYLES.POPUP_CARD} w-[30%] h-[85%]`}>
      <div className="h-[50%] pl-4 pt-4">
        <h1 className={`${TEXT_STYLES.POPUP_TITLE}`}>{title}</h1>
        <p className={`${TEXT_STYLES.POPUP_TEXT}`}>{text}</p>
      </div>
      <div className="h-[50%] pl-4 mt-8 pb-4">
        <Button
          className={`${BUTTON_STYLES.POPUP_LEFT}`}
          onClick={async () => {
            /* object that is gonna be removed from the database using tha apifuncion
               that is given with this component on a page that uses this component.
               (tempId comes from the page too) */
            console.log("this object is removed" + tempId);
            try {
              await apiFunction(tempId);
            } catch (error) {
              console.error("Error handling deleteVoertuig:", error);
            }
            triggerRerender();
            setPopupVisibility(refOverlay, false);
            setPopupVisibility(refRemoveItem, false);
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className={`${BUTTON_STYLES.POPUP_RIGHT}`}
          onClick={() => {
            setPopupVisibility(refOverlay, false);
            setPopupVisibility(refRemoveItem, false);
          }}
        >
          {textBtnRight}
        </Button>
      </div>
    </div>
    </div>
    
  );
};

export default PopupRemoveItem;
