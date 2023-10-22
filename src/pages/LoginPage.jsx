import Header from "../components/Header";
import Login from "../components/Login";

export default function LoginPage() {

  const loginContClasses = "w-[431px] h-[509px] p-5 rounded-[10px] border-4 border-solid border-black absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2";
  const backgroundClasses = "bg-[url('./assets/Media/login-bg.png')] bg-no-repeat bg-cover absolute inset-0 opacity-20 backdrop-blur-md";
  return (
 <>
      <div className="h-screen w-screen relative">
        <div id="loginPageContainer" className="h-screen relative">
          <div
            id="loginContainer"
            className={loginContClasses}
          > 
          <Header heading="Login to your account" />
          <Login></Login>
          </div>
          <div className= {backgroundClasses}></div>
        </div>
      </div>
    </>
  );

}
       