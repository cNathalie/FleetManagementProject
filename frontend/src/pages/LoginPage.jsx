import { useLocation } from "react-router-dom";
import Header from "../components/Header";
import Login from "../components/Login";
import { loginInfo } from "../constants/loginInfo";

export default function LoginPage() {
  const { state } = useLocation();

  let info;
  if (state != undefined) {
    info = state;
  }

  return (
    <>
      <div>
        <div
          id="pageContainer"
          className="absolute top-0 left-0 w-full h-full bg-center bg-no-repeat bg-fixed bg-cover filter blur-md z-[-10]"
          style={{ backgroundImage: "url(src/assets/Media/login-bg.png)" }}
        ></div>
        <div
          id="flexContainer"
          className="flex justify-center items-center min-h-screen"
        >
          <div
            id="formContainer"
            className="w-[431px] h-[509px] bg-opacity-50 bg-white  p-5 bg-[#FFFFFF] rounded-lg"
          >
            <Header heading="" />
            <Login />
            {info == loginInfo.notLoggedIn && (
              <div className="text-center text-red-600 font-btnFontWeigt text-xs">
                Gelieve je eerst aan te melden.
              </div>
            )}
            {info == loginInfo.loginUnknown && (
              <div className="text-center text-red-600 font-btnFontWeigt text-xs">
                Deze inloggegevens zijn niet gekend, probeer opnieuw.
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
}
