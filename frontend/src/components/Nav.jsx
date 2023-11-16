// eslint-disable-next-line no-unused-vars
import React from "react";
import LogoImg from "./Logo";
import { useNavigate } from "react-router-dom";
import Button from "./Button";

const Nav = () => {
  const navigate = useNavigate();

  return (
    <nav className="bg-white dark:bg-gray-900 fixed w-full z-20 top-0 left-0 border-b border-gray-200 dark:border-gray-600 ">
      <div className="max-w-screen-xl flex flex-wrap items-center justify-between mx-auto p-4 ">
        <a
          onClick={() => {
            navigate("/home");
          }}
          className="flex items-center"
        >
          <LogoImg />
        </a>
        <div className="flex md:order-2 ">
          <Button
            className="text-white bg-blueBtn hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center mr-3 md:mr-0 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
            onClick={() => {
              navigate("/admin");
            }}
          >
            Administratie
          </Button>
          <button
            data-collapse-toggle="navbar-sticky"
            type="button"
            className="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
            aria-controls="navbar-sticky"
            aria-expanded="false"
          >
            <span className="sr-only">Open main menu</span>
            <svg
              className="w-5 h-5"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 17 14"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M1 1h15M1 7h15M1 13h15"
              />
            </svg>
          </button>
        </div>
        <div
          className="items-center justify-between hidden w-full md:flex md:w-auto md:order-1"
          id="navbar-sticky"
        >
          <ul className="flex flex-col p-4 md:p-0 mt-4 font-medium border border-gray-100 rounded-lg bg-gray-50 md:flex-row md:space-x-8 md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700">
            <li>
              <a
                onClick={() => {
                  navigate("/items/1");
                }}
                className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >
                Voertuigen
              </a>
            </li>
            <li>
              <a
                onClick={() => {
                  navigate("/items/2");
                }}
                className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >
                Bestuurders
              </a>
            </li>
            <li>
              <a
                onClick={() => {
                  navigate("/items/3");
                }}
                className="block py-2 pl-3 pr-4 text-blueText rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blueTextHover md:hover:underline md:p-0 md:dark:hover:text-blue-500 dark:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
              >
                Tankkaarten
              </a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Nav;
