
import React, { createContext, useState } from 'react'

export const ConfirmContext = createContext();


const ConfirmContextProvider = (props) => {

    const [adminDecision, setAdminDecision] = useState(null);


  return (
    <ConfirmContext.Provider
    value = {{adminDecision, setAdminDecision}}>
        {props.children}
    </ConfirmContext.Provider>
  )
}

export default ConfirmContextProvider