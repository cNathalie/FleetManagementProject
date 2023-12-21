// eslint-disable-next-line no-unused-vars
import React, { createContext, useState } from "react";

// making a context and exporting it
export const DarkModeContext = createContext();

// define a provider
const DarkModeContextProvider = (props) => {
    const [isDarkMode, setIsDarkMode] = useState(false);

    const toggleDarkMode = () => {
        setIsDarkMode((prevState) => !prevState);
    };

    //connect between context and provider
    return (
        <DarkModeContext.Provider
        value={{isDarkMode: isDarkMode, toggleDarkMode: toggleDarkMode}}>
            {props.children}
        </DarkModeContext.Provider>
    );
};

// export default
export default DarkModeContextProvider;