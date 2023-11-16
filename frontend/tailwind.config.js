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
    },
    fontFamily: {
      mainFont: "inter",

      Helvetica: ["Helvetica", "Arial", "sans-serif"],
    },
    fontSize: {
      btnFontSize: "21px",
      titleFontSize: "21px",
      popupTextSize: "16px",
    },
    fontWeight: {
      btnFontWeigt: "600",
      titleFontWeigt: "600",
    },
    screens: {
      xl3: "1920px",
    },
    extend: {},
  },
  plugins: [require("flowbite/plugin")],
};
