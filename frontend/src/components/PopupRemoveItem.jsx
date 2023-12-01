import Button from "./Button";
/* eslint-disable react/prop-types*/

const PopupRemoveItem = ({
  popup,
  apiFunction,
  setPopupVisibility,
  tempId,
  triggerRerender
}) => {
  const { title, text, textBtnLeft, textBtnRight } = popup;
  //const { data } = useData();
  return (
    <div className="w-[30%] h-[85%] bg-[#DBDBDB] rounded-xl">
      <div className="h-[50%] pl-4 pt-4">
        <h1 className="font-mainFont font-titleFontWeigt text-titleFontSize">
          {title}
        </h1>
        <p className="font-mainFont font-titleFontWeigt text-popupTextSize pt-2 text-[#858585]">
          {text}
        </p>
      </div>
      <div className="h-[50%] pl-4 mt-8">
        <Button
          className="bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn"
          onClick={() => {
            setPopupVisibility("overlay", false);
            setPopupVisibility("Popup", false);
          }}
        >
          {textBtnLeft}
        </Button>
        <Button
          className="bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn ml-6"
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
