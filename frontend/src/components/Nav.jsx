// eslint-disable-next-line no-unused-vars
import React from "react";
import LogoImg from "./Logo";
import { useNavigate } from "react-router-dom";
import Button from "./Button";
import homePage from "../constants/homePageContent";
import LogOutButton from "./LogOutButton";

const Nav = () => {
  const navigate = useNavigate();

  return (
    <div className="w-full bg-white fixed border-b border-gray-200 flex flex-wrap items-center justify-between mx-auto">
      <div className="pt-2 pl-6">
        <Button
          onClick={() => {
            navigate("/home");
          }}
        >
          <img
            src="../src/assets/Media/FM_noBG.png"
            alt="logo"
            className="w-[151px] h-[60px]"
          />
        </Button>
      </div>
      <div className=" flex flex-wrap">
        {homePage.map((h) => {
          return (
            <Button
              key={h.title}
              onClick={() => {
                navigate(`/items/${h.id}`);
              }}
              className="py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:p-0"
            >
              {h.title}
            </Button>
          );
        })}
      </div>
      <div>
        <div className="px-6 flex flex-wrap">
          <Button
            className="text-white bg-blueBtn hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center"
            onClick={() => {
              navigate("/admin");
            }}
          >
            Administratie
          </Button>
          <div className="pl-4">
            <LogOutButton />
          </div>
        </div>
      </div>
    </div>
    // <div className="bg-black fixed w-full z-20 border-b border-gray-200">
    //   <div className="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4">
    //     <div className="fixed left-0 top-0">
    //       <Button
    //         onClick={() => {
    //           navigate("/home");
    //         }}
    //       >
    //         <LogoImg />
    //       </Button>
    //     </div>
    //     <div className="flex md:order-2 ">
    //       <Button
    //         className="text-white bg-blueBtn hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center mr-3 md:mr-0 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 fixed top-0 right-0"
    //         onClick={() => {
    //           navigate("/admin");
    //         }}
    //       >
    //         Administratie
    //       </Button>
    //     </div>
    //     <div
    //       className="items-center justify-between w-full md:flex md:w-auto md:order-1"
    //       id="navbar-sticky"
    //     >
    //       <ul className="flex flex-col p-4 md:p-0 mt-4 font-medium border border-gray-100 rounded-lg bg-gray-50 md:flex-row md:space-x-8 md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700">
    //         <li>
    //           <a
    //             onClick={() => {
    //               navigate("/items/1");
    //             }}
    //             className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
    //           >
    //             Voertuigen
    //           </a>
    //         </li>
    //         <li>
    //           <a
    //             onClick={() => {
    //               navigate("/items/2");
    //             }}
    //             className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
    //           >
    //             Bestuurders
    //           </a>
    //         </li>
    //         <li>
    //           <a
    //             onClick={() => {
    //               navigate("/items/3");
    //             }}
    //             className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
    //           >
    //             Tankkaarten
    //           </a>
    //         </li>
    //         <li>
    //           <a
    //             onClick={() => {
    //               navigate("/items/4");
    //             }}
    //             className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
    //           >
    //             Fleet
    //           </a>
    //         </li>
    //       </ul>
    //     </div>
    //   </div>
    // </div>
  );
};

export default Nav;
