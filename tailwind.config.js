/** @type {import('tailwindcss').Config} */
/*eslint-env node*/

module.exports = {
  content: ["./src/**/*.{js,jsx,ts,tsx}"],
  theme: {
    colors: {
      blueBtn: "#19B9CE",
      blueText: "#0B5A64",
    },
    fontFamily: {
      mainFont: "inter",
    },
    fontSize: {
      btnFontSize: "21px",
    },
    fontWeight: {
      btnFontWeigt: "600",
    },
    extend: {},
  },
  plugins: [],
};

