// eslint-disable-next-line no-unused-vars
import React from 'react'
import { useNavigate } from "react-router-dom";

const AdminHeader = () => {
  const navigate = useNavigate();
  
  return (
    <div className="w-[100%] h-[100px] flex justify-center">
        {/* Header */}
      <div className="flex justify-between my-14 w-[786px] h-[43px]">
        <button className="relative w-[201px] h-[43px] bg-whiteText text-darkBlue rounded-[10px] border border-solid border-darkBlue hover:bg-darkBlue hover:text-whiteText [font-family: 'Inter-SemiBold',Helvetica] font-semibold"
        onClick={() => {
          navigate(-1);
        }}
        >
                <p>Terug naar homepagina</p>
        </button>
        <div className="[font-family:'Inter-SemiBold',Helvetica] font-semibold text-darkBlue text-adminTitle underline whitespace nowrap">
            Administratie
        </div>
      </div>
    </div>
  );
}

export default AdminHeader