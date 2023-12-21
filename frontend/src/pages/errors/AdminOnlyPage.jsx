import React from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../../authentication/useAuth";


const AdminOnlyPage = () => {
  const {logout} = useAuth();
  const navigate = useNavigate();

  return (
    <div className="p-35 flex flex-col items-center">
      <div className="flex flex-col items-center bg-white border border-gray-200 rounded-lg shadow md:flex-row md:max-w-xl hover:bg-gray-100 dark:border-gray-700 dark:bg-gray-800 dark:hover:bg-gray-700">
        <img
          className="object-cover w-full rounded-t-lg h-96 md:h-auto md:w-48 md:rounded-none md:rounded-s-lg"
          src="src/assets/Media/locked_car.jpg"
          alt="locked car"
        />
        <div className="flex flex-col items-center justify-between p-4 leading-normal">
          <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
            Geen toegang!
          </h5>
          <p className="mb-3 font-normal text-gray-700 dark:text-gray-400">
            <span
              onClick={() => navigate(-1)}
              className="font-btnFontWeigt hover:underline cursor-pointer"
            >
              Ga terug
            </span>{" "}
            of{" "}
            <span
              onClick={() => {
                logout();
                navigate("/");
              }}
              className="font-btnFontWeigt hover:underline cursor-pointer"
            >
              meldt je aan als administrator.
            </span>
          </p>
        </div>
      </div>

      {/* staat hier om te tonen en testen
      <div className="pt-16">
        deze knop staat hier voorlopig om te tonen, je kan hem gebruiken waar je
        wil
      </div>
      <LogOutButton /> */}
    </div>
  );
};

export default AdminOnlyPage;
