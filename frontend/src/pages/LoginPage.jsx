import { useLocation } from "react-router-dom";
import Header from "../components/Header";
import Login from "../components/LogIn";
import { loginInfo } from "../constants/loginInfo";
import {
  BG_STYLES,
  CARD_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";

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
          className={BG_STYLES.LOGIN_BG}
          style={{ backgroundImage: "url(src/assets/Media/login-bg.png)" }}
        ></div>
        <div
          id="flexContainer"
          className="flex justify-center items-center min-h-screen"
        >
          <div
            id="formContainer"
            className={`${CARD_STYLES.LOGIN_CARD} w-[431px] h-[509px]`}
          >
            <Header heading="" />
            <Login />
            {info == loginInfo.notLoggedIn && (
              <div className={TEXT_STYLES.LOGIN_ERROR}>
                Gelieve je eerst aan te melden.
              </div>
            )}
            {info == loginInfo.loginUnknown && (
              <div className={TEXT_STYLES.LOGIN_ERROR}>
                Deze inloggegevens zijn niet gekend, probeer opnieuw.
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
}
