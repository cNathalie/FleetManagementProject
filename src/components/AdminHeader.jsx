// eslint-disable-next-line no-unused-vars
import React from 'react'


const AdminHeader = () => {
  return (
    <div className="w-[100%] h-[100px] flex justify-center">
        {/* Header */}
      <div className="flex justify-between my-14 w-[786px] h-[43px]">
        <button className="relative w-[201px] h-[43px] bg-[#ffffff] rounded-[10px] border border-solid border-[#0b5a64] hover:bg-[#0b5a64] hover:text-[#ffffff] [font-family: 'Inter-SemiBold',Helvetica] font-semibold">
                <p>Terug naar homepagina</p>
        </button>
        <div className="[font-family:'Inter-SemiBold',Helvetica] font-semibold text-[#0b5a64] text-[35px] underline whitespace nowrap">
            Administratie
        </div>
      </div>
    </div>
  );
}

export default AdminHeader