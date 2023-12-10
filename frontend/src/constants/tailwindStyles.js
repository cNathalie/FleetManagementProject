export const BUTTON_STYLES = {
  //ADMIN PAGE
  ADMIN_GOBACK:
    "bg-whiteText text-darkBlue rounded-[10px] border border-solid border-darkBlue hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold",
  ADMIN_REMOVE:
    "rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold text-darkBlue text-adminBtnFontSize text-center",

  //USERFORM COMPONENT
  USERFORM_SUBMIT:
    "rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold text-darkBlue text-adminBtnFontSize text-center",

  //LOGIN FORMACTION
  LOGIN_SUBMIT:
    "bg-blueBtn text-white border-blueBtn font-btnFontWeigt font-Helvetica text-btnFontSize rounded-[10px] hover:bg-hoverBtn",

  //HOMEPAGE
  NAV_BUTTONS: "text-blueText rounded hover:bg-gray-100 md:p-0 font-mainFont",
  NAV_ADMINBUTTON:
    "text-white font-mainFont bg-blueBtn hover:bg-hoverBtn focus:ring-4 focus:outline-none font-medium rounded-lg text-sm text-center",
  NAV_LOGOUTBUTTON:
    "rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold text-darkBlue text-adminBtnFontSize text-center",
  HOMEPAGE_CARDBUTTON:
    "w-3/4 h-[53px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-white bg-blueBtn hover:bg-hoverBtn cursor-pointer",

  //POPUPS
  POPUP_LEFT:
    "bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn",
  POPUP_RIGHT:
    "bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn ml-6",
};

export const TEXT_STYLES = {
  //ADMIN PAGE
  ADMIN_TITLE:
    "font-mainFont font-semibold text-darkBlue text-adminTitle underline whitespace nowrap",
  ADMIN_CARDTITLE:
    "font-mainFont font-semibold text-whiteText text-adminHeader text-center wrap",
  ADMIN_OR:
    "font-mainFont font-semibold text-darkBlue text-[18px] text-center tracking-[0] leading-[normal] whitespace-nowrap",

  //USERFORM COMPONENT
  USERFORM_LABEL: "block text-whiteText font-semibold mb-2",

  //HOMEPAGE
  HOMEPAGE_CARDTITLE:
    "font-titleFontWeigt text-[32px] font-Helvetica text-center h-[15%]",
  HOMEPAGE_CARDTEXT:
    "font-semibold text-[20px] font-Helvetica text-center font-titleFontWeigt h-[60%] flex items-center p-5",

  //LOGIN PAGE
  LOGIN_LABEL:
    "text-left font-Helvetica font-titleFontWeigt text-titleFontSize text-blueText",
  LOGIN_ERROR: "text-center text-red-600 font-btnFontWeigt text-xs",

  //POPUPS
  POPUP_TITLE: "font-mainFont font-titleFontWeigt text-titleFontSize",
  POPUP_TEXT:
    "font-mainFont font-titleFontWeigt text-popupTextSize text-[#858585] pt-2",
};

export const INPUT_STYLES = {
  //USERFORM COMPONENT
  USERFORM_INPUT:
    "border-b bg-transparent border-whiteText text-whiteText placeholder-adminPlaceHolder focus:outline-none focus:ring-2 focus:ring-whiteText focus:border-whiteText rounded-md",
  USERFORM_INPUT_BLACK:
    "border-b bg-transparent border-whiteText placeholder-adminPlaceHolder focus:outline-none focus:ring-2 focus:ring-whiteText focus:border-whiteText rounded-md",

  //LOGIN PAGE
  LOGIN_INPUT:
    "appearance-none relative block  w-full px-3 py-2 border-b bg-transparent border-blueText placeholder-blueText text-gray-900 rounded-md focus:outline-none focus:ring-2 focus:ring-blueText focus:border-blueText",
};

export const CARD_STYLES = {
  //ADMIN PAGE
  BLUE_CARD:
    "flex justify-center items-center flex-col bg-blueBtn rounded-[10px] shadow-2xl",

  //LOGIN PAGE
  LOGIN_CARD: "bg-opacity-50 bg-white p-5 rounded-lg",

  //POPUPS
  POPUP_CARD: "bg-[#DBDBDB] rounded-xl",
};

export const BG_STYLES = {
  //LOGIN PAGE
  LOGIN_BG:
    "absolute top-0 left-0 w-full h-full bg-center bg-no-repeat bg-fixed bg-cover filter blur-md z-[-10]",

  //HOMEPAGE
  NAV_BG: "bg-white border-b border-gray-200",
};
