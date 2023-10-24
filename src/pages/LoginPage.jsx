import Header from "../components/Header";
import Login from "../components/Login";


export default function LoginPage() {
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
          className="flex justify-center items-center min-h-screen">
            <div 
            id="formContainer"
            className="w-[431px] h-[509px] bg-opacity-50 bg-white  p-5 bg-[#FFFFFF] rounded-lg">
              <Header heading="" />
              <Login />
            </div>
          </div>
        </div>
        
      </>
    );

}
