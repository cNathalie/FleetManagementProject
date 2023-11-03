/* eslint-disable react/prop-types */
// eslint-disable-next-line no-unused-vars
import React from "react";
import VoertuigenContent from "../constants/VoertuigPageContent";
import Button from "./Button";


const Table = (props) => {
    const { tableHeaderContent } = props;

    return(       
        <div className="flex justify-center items-center h-screen mt-20">
            <div className="relative overflow-x-auto shadow-md sm:rounded-lg">
            <div className="pb-4 bg-white dark:bg-gray-900">
                <label htmlFor="table-search" className="sr-only">Search</label>
                <div className="relative mt-1">
                    <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <svg className="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20">
                            <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"/>
                        </svg>
                    </div>
                        <input type="text" id="table-search" className="block p-2 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg w-80 bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search for items"/>
                    </div>
                    <div className="absolute left-4 top-4">
                <Button func={"#"} btnText={"Toevoegen"}/>
            </div>
                </div>            
                <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                    <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                        <tr>
                            {tableHeaderContent.map((t) => {
                                return <>
                                    <th scope="col" className="px-6 py-3">
                                        <div className="flex items-center">
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
                                    <th scope="row" className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                                        {v.merk}
                                    </th>
                                    <td className="px-6 py-4">
                                        {v.model}
                                    </td>
                                    <td className="px-6 py-4">
                                        {v.chasisnummer}
                                    </td>
                                    <td className="px-6 py-4">
                                        {v.nummerplaat}
                                    </td>
                                    <td className="px-6 py-4">
                                        {v.brandstoftype}
                                    </td>
                                    <td className="px-6 py-4 text-right">
                                        <a href="#" className="font-medium text-blue-600 dark:text-blue-500 hover:underline">Edit</a>
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