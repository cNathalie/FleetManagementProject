import React from "react";
/* eslint-disable react/prop-types*/

const Popup = (props) => {
  return (
    <div
      {...props}
      style={{
        position: "absolute",
        zIndex: 100,
        width: "100%",
        height: "200px",
        left: "35%",
        top: "35%",
      }}
    >
      {props.children}
    </div>
  );
};

export default Popup;
