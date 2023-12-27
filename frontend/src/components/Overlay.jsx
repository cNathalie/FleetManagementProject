/**
 * The Overlay component is a React component that renders a transparent overlay with a specified
 * visibility.
 * @returns The Overlay component is returning a div element with the id "overlay". The display
 * property of the div is set to "none" if the visible prop is true, and "block" if the visible prop is
 * false.
 */
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
      }}
    ></div>
  );
};

export default Overlay;
