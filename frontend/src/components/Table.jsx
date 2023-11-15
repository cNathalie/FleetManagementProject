/* eslint-disable react/prop-types */
// eslint-disable-next-line no-unused-vars
import React from "react";
import VoertuigenContent from "../constants/VoertuigPageContent";


const Table = (props) => {
    const { tableHeaderContent } = props;

    return(       
        <div className="flex items-center justify-center h-screen " >
            <div className="max-h-[585px] overflow-y-auto group group scrollbar-thin hover:scrollbar-thumb-gray-100">
                <table className="w-full max-w-[1122px] h-[585px] text-sm text-left text-gray-500 dark:text-gray-400 shadow-md sm:rounded-lg">
                <thead className="sticky top-0 bg-white">
                    <img
                        className="h-[30px] top-[32px] absolute  w-full "
                        alt="Line"
                        src="https://c.animaapp.com/1ptxcx7H/img/line-1.svg"
                    />
                    <tr>                
                        {tableHeaderContent.map((t) => {
                            return <>
                                <th scope="col" className="px-6 py-3 [font-family:'Inter',Helvetica] font-semibold text-[#848484] text-[17px] tracking-[0] leading-[normal]">
                                    <div className="flex items-center ">
                                        {t}
                                    </div>
                                </th>
                            </>
                        })}
                    </tr>
                </thead>
                <tbody>
                        {VoertuigenContent.map((v) => {
                            return <>  
                            <tr className="bg-white border-b dark:bg-gray-800 dark:border-gray-700">
                                <th className="px-6 py-4 [font-family:'Inter',Helvetica] font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal]">
                                    {v.merk}
                                </th>
                                <td className="px-6 py-4 [font-family:'Inter',Helvetica] font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal]">
                                    {v.model}
                                </td>
                                <td className="px-6 py-4 [font-family:'Inter',Helvetica] font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal]">
                                    {v.chasisnummer}
                                </td>
                                <td className="px-6 py-4 [font-family:'Inter',Helvetica] font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal]">
                                    {v.nummerplaat}
                                </td>
                                <td className="px-6 py-4 [font-family:'Inter',Helvetica] font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal]">
                                    {v.brandstoftype}
                                </td>
                                <td className="px-6 py-4 text-right flex items-center">
                                    <img
                                    className="w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110"
                                    alt="Bulleted list"
                                    src="https://c.animaapp.com/1ptxcx7H/img/bulleted-list-1@2x.png"
                                    />
                                    <img
                                    className="w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110"
                                    alt="Edit"
                                    src="https://c.animaapp.com/1ptxcx7H/img/edit-1@2x.png"
                                    />
                                    <img
                                    className="w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110"
                                    alt="Trash"
                                    src="https://c.animaapp.com/1ptxcx7H/img/trash-1@2x.png"
                                    />
                                </td>
                            </tr>
                            </>
                        })}
                </tbody>
            </table>
            </div>           
        </div>
    );
}

export default Table;