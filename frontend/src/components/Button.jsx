
// Button moet twee parameters meekrijgen: 
//      func : de funtie die code bevat om een klik op de knop af te handelen (die is gedefinieerd in de parent-component die de Button gebruikt)
//      btnText : de tekst die in de Button moet staan

const Button = ({func, btnText}) => {


    return (
        <>
            <button
              type="button"
              className="bg-blueBtn w-auto px-4 py-1 text-center text-blueText font-mainFont font-btnFontWeigt text-btnFontSize rounded-[10px]"
              onClick={func}
            >
              {btnText}

            </button>
        </>
    );
};

export default Button;