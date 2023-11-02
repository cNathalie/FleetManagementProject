// Button moet twee parameters meekrijgen:
//      func : de funtie die code bevat om een klik op de knop af te handelen (die is gedefinieerd in de parent-component die de Button gebruikt)
//      btnText : de tekst die in de Button moet staan

const Button = (props) => {
  return (
    <>
      <button {...props}>{props.children}</button>
    </>
  );
};

export default Button;
