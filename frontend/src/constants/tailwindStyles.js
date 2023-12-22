export const BUTTON_STYLES = {
  //ADMIN PAGE
  ADMIN_GOBACK:
    "bg-whiteText text-darkBlue rounded-[10px] border border-solid border-darkBlue hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold dark:text-white dark:border-darkBlue1 dark:bg-darkBlue6 dark:hover:bg-darkBlue4",
  ADMIN_REMOVE:
    "rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold text-darkBlue text-adminBtnFontSize text-center dark:text-darkBlue1 dark:border-darkBlue4 dark:hover:bg-darkBlue6 dark:hover:text-white",

  //USERFORM COMPONENT
  USERFORM_SUBMIT:
    "rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold text-darkBlue text-adminBtnFontSize text-center dark:text-darkBlue1 dark:border-darkBlue4 dark:hover:bg-darkBlue6 dark:hover:text-white",

  //LOGIN FORMACTION
  LOGIN_SUBMIT:
    "bg-blueBtn text-white border-blueBtn font-btnFontWeigt font-Helvetica text-btnFontSize rounded-[10px] hover:bg-hoverBtn",

  //HOMEPAGE
  NAV_BUTTONS: "text-blueText rounded hover:bg-gray-100 md:p-0 font-mainFont dark:text-white dark:hover:bg-darkBlue4",
  NAV_ADMINBUTTON:
    "text-white font-mainFont bg-blueBtn hover:bg-hoverBtn focus:ring-4 focus:outline-none font-medium rounded-lg text-sm text-center dark:bg-darkBlue4 dark:hover:bg-darkBlue5",
  NAV_LOGOUTBUTTON:
    "rounded-[10px] bg-whiteText border border-solid border-darkBlue font-semibold hover:bg-darkBlue hover:text-whiteText font-mainFont font-semibold text-darkBlue text-adminBtnFontSize text-center dark:hover:bg-darkBlue5",
  HOMEPAGE_CARDBUTTON:
    "w-3/4 h-[53px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-white bg-blueBtn hover:bg-hoverBtn cursor-pointer dark:bg-darkBlue1 dark:hover:bg-darkBlue4",

  //OVERVIEW PAGE
  OVERVIEW_EXITBUTTON: "rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt dark:bg-darkBlue2",
  OVERVIEW_EDITBUTTON: "w-28 h-[40px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-whiteText bg-blueBtn hover:bg-hoverBtn cursor-pointer dark:bg-darkBlue5 dark:hover:bg-darkBlue4",
  OVERVIEW_ADDBUTTON: "w-[150px] md:w-[150px] h-[35] md:h-[43px] font-mainFont font-semibold text-white bg-blueBtn hover:bg-hoverBtn flex items-center justify-center focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center mr-3 md:mr-0 transform translate-x-2 dark:bg-darkBlue4 dark:hover:bg-darkBlue5",

  //POPUPS
  POPUP_LEFT:
    "bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn dark:bg-darkBlue5 dark:hover:bg-darkBlue4",
  POPUP_RIGHT:
    "bg-blueBtn w-[100px] h-8 text-center text-whiteText font-mainFont font-btnFontWeigt text-popupTextSize rounded-lg cursor-pointer hover:bg-hoverBtn ml-6 dark:bg-darkBlue5 dark:hover:bg-darkBlue4",
};

export const TEXT_STYLES = {
  //ADMIN PAGE
  ADMIN_TITLE:
    "font-mainFont font-semibold text-darkBlue text-adminTitle underline whitespace nowrap dark:text-white",
  ADMIN_CARDTITLE:
    "font-mainFont font-semibold text-whiteText text-adminHeader text-center wrap",
  ADMIN_OR:
    "font-mainFont font-semibold text-darkBlue text-[18px] text-center tracking-[0] leading-[normal] whitespace-nowrap dark:text-white",

  //USERFORM COMPONENT
  USERFORM_LABEL: "block text-whiteText font-semibold mb-2",

  //HOMEPAGE
  HOMEPAGE_CARDTITLE:
    "font-titleFontWeight text-[32px] font-Helvetica text-center h-[15%] pt-5",
  HOMEPAGE_CARDTEXT:
    "font-semibold text-[20px] font-Helvetica text-center font-titleFontWeight h-[60%] flex items-center p-5",

  //LOGIN PAGE
  LOGIN_LABEL:
    "text-left font-Helvetica font-titleFontWeight text-titleFontSize text-blueText",
  LOGIN_ERROR: "text-center text-red-600 font-btnFontWeigt text-xs",

  // OVERVIEW PAGE
  OVERVIEW_TITLE: "font-mainFont font-titleFontWeight text-4xl text-black dark:text-white",
  OVERVIEW_DATAHEADER: "font-mainFont font-titleFontWeight uppercase",
  OVERVIEW_DATAVALUE: "font-mainFont font-semibold text-overviewData text-[18px] text-left tracking-[0] leading-[normal] whitespace-nowrap w-[75%]",
  OVERVIEW_TABLEHEAD: "w-full max-w-[1122px] h-[585px] font-mainFont text-sm text-left text-overviewData shadow-md sm:rounded-lg",
  OVERVIEW_TABLETITLE: "px-6 py-3 font-mainFont font-semibold text-overviewData text-[17px] tracking-[0] leading-[normal] dark:text-darkBlue6",
  OVERVIEW_TABLEDATA: "px-6 py-4 font-mainFont font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal] dark:text-white",

  //POPUPS
  POPUP_TITLE: "font-mainFont font-titleFontWeight text-titleFontSize text-black dark:text-white",
  POPUP_TEXT:
    "font-mainFont font-titleFontWeight text-popupTextSize text-[#858585] pt-2",
};

export const INPUT_STYLES = {
  //USERFORM COMPONENT
  USERFORM_INPUT:
    "border-b bg-transparent border-whiteText text-whiteText placeholder-adminPlaceHolder focus:outline-none focus:ring-2 focus:ring-whiteText focus:border-whiteText rounded-md",

  //LOGIN PAGE
  LOGIN_INPUT:
    "appearance-none relative block  w-full px-3 py-2 border-b bg-transparent border-blueText placeholder-blueText text-gray-900 rounded-md focus:outline-none focus:ring-2 focus:ring-blueText focus:border-blueText",

  //OVERVIEW PAGE
  OVERVIEW_INPUT: "border-2 bg-transparent border-white placeholder-adminPlaceHolder focus:outline-none focus:ring-1 focus:ring-white focus:border-white rounded-md font-mainFont font-semibold text-darkBlue dark:text-white",
  OVERVIEW_DROPDWN_INPUT: "w-1/2 font-mainFont font-semibold text-darkBlue dark:text-darkBlue1",
  OVERVIEW_SEARCH_INPUT: "w-36 h-6 bg-[#EDEDED] rounded-lg text-overviewData font-semibold font-mainFont px-2 border-none focus:ring-2 focus:ring-blueBtn dark:text-darkBlue1 dark:focus:ring-darkBlue6",
};

export const CARD_STYLES = {
  //ADMIN PAGE
  BLUE_CARD:
    "flex justify-center items-center flex-col bg-blueBtn rounded-[10px] shadow-2xl dark:bg-darkBlue3",

  //LOGIN PAGE
  LOGIN_CARD: "bg-opacity-50 bg-white p-5 rounded-lg",

  //POPUPS
  POPUP_CARD: "bg-[#DBDBDB] rounded-xl dark:bg-darkBlue2",
};

export const BG_STYLES = {
  //LOGIN PAGE
  LOGIN_BG:
    "absolute top-0 left-0 w-full h-full bg-center bg-no-repeat bg-fixed bg-cover filter blur-md z-[-10]",

  //HOMEPAGE
  NAV_BG: "bg-white border-b border-gray-200 dark:bg-darkBlue2 dark:border-darkBlue1",

  //OVERVIEW PAGE
  OVERVIEW_BG: "w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB] dark:bg-darkBlue2",
  OVERVIEW_TABLEHEADBG: "sticky top-0 bg-white z-10 border-b-2 border-gray-300 shadow-sm dark:bg-darkBlue1",
  OVERVIEW_TABLEDATABG: "bg-white border-b dark:bg-darkBlue1",
};

export const IMG_STYLES = {
  //OVERVIEW PAGE

  OVERVIEW_IMG_DETAIL: "w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110",
  OVERVIEW_IMG_EDIT: "w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110",
  OVERVIEW_IMG_DELETE: "w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110",
};
