import React from "react";

const Overlay = () => {
  return (
    <div
      id="overlay"
      style={{
        position: "fixed",
        top: 0,
        left: 0,
        width: "100vw",
        height: "100vh",
        zIndex: 20,
        background: "rgb(111,111,111,0.5)",
        display: "none",
      }}
    ></div>
  );
};

export default Overlay;
