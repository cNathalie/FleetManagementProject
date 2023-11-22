/* eslint-disable react/prop-types */
import { Link } from "react-router-dom";

export default function Header({
  heading,
  paragraph,
  linkName,
  linkUrl = "#",
}) {
  return (
    <div className="mb-10">
      <div className="flex justify-center font-mainFont">
        <img alt="" className=" w-80" src="src/assets/Media/FM_noBG.png" />
      </div>
      {/* <h2 className="mt-6 text-center text-3xl  text-gray-900">
        {heading}
      </h2>
      <p className="mt-2 text-center text-sm mt-5">
        {paragraph}{" "}
        <Link
          to={linkUrl}
          className="font-medium text-purple-600 hover:text-purple-500"
        >
          {linkName}
        </Link>
      </p> */}
    </div>
  );
}
