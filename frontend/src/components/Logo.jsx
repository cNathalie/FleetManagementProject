import { useNavigate } from "react-router-dom";
import logo from "../assets/Media/FM_noBG.png";

const LogoImg = () => {
  const navigate = useNavigate();

  return (
    <div className="absolute w-[151px] h-[123px] top-[13px] left-[139px] object-cover">
      <img
        alt="Fleet removebg"
        src={logo}
        onClick={() => navigate("/")}
        className="cursor-pointer"
      />
    </div>
  );
};

export default LogoImg;
