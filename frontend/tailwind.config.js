/** @type {import('tailwindcss').Config} */

/*eslint-env node*/

module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}", "./node_modules/flowbite/**/*.js"],
  theme: {
    colors: {
      blueBtn: "#19B9CE",
      blueText: "#0B5A64",
      whiteText: "#FFFFFF",
      hoverBtn: "#118190",
      blueTextHover: "#118190",
      // Darker blue color so the forms (AdminContent) are easier to read
      darkBlue: "#0b5a64",
      transparent: "#ffffff00",
      adminPlaceHolder: "#ffffff7a",
      overviewData: "#858585",
      darkBlue1: "#032030",
      darkBlue2: "#022B42",
      darkBlue3: "#003554",
      darkBlue4: "#004D74",
      darkBlue5: "#006494",
      darkBlue6: "#006DA4",
    },
    fontFamily: {
      mainFont: "inter",

      Helvetica: ["Helvetica", "Arial", "sans-serif"],
    },
    fontSize: {
      btnFontSize: "21px",
      adminBtnFontSize: "15px",
      titleFontSize: "21px",
      popupTextSize: "16px",
      adminTitle: "35px",
      adminHeader: "25px",
    },
    fontWeight: {
      btnFontWeigt: "600",
      titleFontWeight: "600",
    },
    extend: {
      spacing: {
        15: "3.75rem",
        16: "4rem",
        17: "4.25rem",
        18: "4.5rem",
        19: "4.75rem",
        20: "5rem",
        21: "5.25rem",
        22: "5.5rem",
        23: "5.75rem",
        25: "6.25rem",
        26: "6.5rem",
        27: "6.75rem",
        29: "7.25rem",
        30: "7.5rem",
        31: "7.75rem",
        33: "8.25rem",
        34: "8.50rem",
        35: "8.75rem",
        37: "9.25rem",
        38: "9.5rem",
        39: "9.75rem",
        40: "10rem",
        41: "10.25rem",
        42: "10.5rem",
        43: "10.75rem",
        44: "11rem",
        45: "11.25rem",
      },
    },
    screens: {
      "3xl": "1920px",
    },
  },
  plugins: [require("flowbite/plugin"), require("tailwind-scrollbar")],
};
