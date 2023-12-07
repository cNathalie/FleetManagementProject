import { BUTTON_STYLES, CARD_STYLES, TEXT_STYLES } from "../constants/tailwindStyles";
import Button from "./Button";
/* eslint-disable react/prop-types*/

const PopupRemoveItem = ({
  popup,
  apiFunction,
  setPopupVisibility,
  tempId,
}) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;
  //const { data } = useData();
  return (
    <div className={`${CARD_STYLES.POPUP_CARD} w-[30%] h-[85%]`}>
      <div className="h-[50%] pl-4 pt-4">
        <h1 className={`${TEXT_STYLES.POPUP_TITLE}`}>
          {title}
        </h1>
        <p className={`${TEXT_STYLES.POPUP_TEXT}`}>
          {text}
        </p>
      </div>
      <div className="h-[50%] pl-4 mt-8">
        <Button
          className={`${BUTTON_STYLES.POPUP_LEFT}`}
          onClick={() => {
            setPopupVisibility("overlay", false);
            setPopupVisibility("Popup", false);
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className={`${BUTTON_STYLES.POPUP_RIGHT}`}
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
            
            setPopupVisibility("overlay", false);
            setPopupVisibility("Popup", false);
          }}
        >
          {textBtnRight}
        </Button>
      </div>
    </div>
  );
};

export default PopupRemoveItem;
