const Overlay = (visible) => {
  return (
    <div
      id="overlay"
      style={{
        display: visible ? "none" : "block",
        position: "fixed",
        top: 0,
        left: 0,
        width: "100vw",
        height: "100vh",
        zIndex: 20,
        background: "rgb(111,111,111,0.5)",
      }}
    ></div>
  );
};

export default Overlay;
