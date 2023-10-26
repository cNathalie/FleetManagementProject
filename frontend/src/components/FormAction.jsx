export default function FormAction({
  handleSubmit,
  type = "Button",
  action = "submit",
  text,
}) {
  return (
    <>
      {type === "Button" ? (
        <button
          type={action}
          className="relative w-full flex justify-center py-2 px-4 bg-blueBtn border-blueBtn font-btnFontWeigt font-mainFont text-btnFontSize rounded-[10px]"
          onClick={handleSubmit}
        >
          {text}
        </button>
      ) : (
        <></>
      )}
    </>
  );
}
