/* eslint-disable react/jsx-key */
/* eslint-disable react/prop-types */
// eslint-disable-next-line no-unused-vars
import { useState } from "react"; 

const Table = (props) => {
  const { tableHeaderContent, inputData, data, setPopupVisibility, setTempContent, iDname } = props;
  const [searchTerm, setSearchTerm] = useState("");

  const filteredData = data
    ? data.filter((v) =>
        Object.values(v).some(
          (value) =>
            typeof value === "string" &&
            value.toLowerCase().includes(searchTerm.toLowerCase())
        )
      )
    : [];
  
  return (
    <div className="flex items-center justify-center h-screen ">
      <div className="max-h-[585px] overflow-y-auto group group scrollbar-thin hover:scrollbar-thumb-gray-100">
        <table className="w-full max-w-[1122px] h-[585px] text-sm text-left text-gray-500 dark:text-gray-400 shadow-md sm:rounded-lg">
          <thead className="sticky top-0 bg-white z-10 border-b-2 border-gray-300 shadow-sm">
            {/* First Section of thead */}
            <tr>
              <th colSpan={tableHeaderContent.length} className="py-3">
                <div className="flex justify-between items-center">
                  {/* Button on the left */}
                  <button
                    type="button"
                    className="w-[150px] md:w-[150px] h-[29px] md:h-[43px] text-white bg-[#18b8ce] hover:bg-blue-300 flex items-center justify-center focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center mr-3 md:mr-0 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 transform translate-x-2"
                    onClick={() => {
                      setPopupVisibility("overlay", true);
                      setPopupVisibility("addItem", true);
                    }}
                  >
                    Toevoegen
                  </button>
                  {/* Search input on the right */}
                  <input
                    type="text"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    placeholder="zoek..."
                    className="w-36 h-6 bg-gray-200 rounded-lg text-zinc-500 text-base font-semibold font-['Inter'] px-2"
                  />
                </div>
              </th>
            </tr>
            {/* Second Section of thead */}
            <tr>
              {tableHeaderContent.map((t, index) => {
                return (
                  <>
                    <th
                      key={index}
                      scope="col"
                      className="px-6 py-3 [font-family:'Inter',Helvetica] font-semibold text-[#848484] text-[17px] tracking-[0] leading-[normal]"
                    >
                      <div className="flex items-center ">{t}</div>
                    </th>
                  </>
                );
              })}
            </tr>
          </thead>
          <tbody>
            {filteredData.map((d) => {
              const id = d[iDname];
              return (
                <>
                  <tr
                    key={id}
                    className="bg-white border-b dark:bg-gray-800 dark:border-gray-700"
                  >
                    {inputData.map((i) => {
                      return(
                        <th className="px-6 py-4 [font-family:'Inter',Helvetica] font-semibold text-[#4c4c4c] text-[14px] tracking-[0] leading-[normal]">
                          {eval(i)}
                        </th>
                      )
                    })}
                    <td className="px-6 py-4 text-right flex items-center">
                      <img
                        className="w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110"
                        alt="Bulleted list"
                        src="https://c.animaapp.com/1ptxcx7H/img/bulleted-list-1@2x.png"
                        onClick={() => {
                          setPopupVisibility("overlay", true);
                          setPopupVisibility("detailDisplay", true);
                          setTempContent('tempObject' ,d);
                        }}
                      />
                      <img
                        className="w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110"
                        alt="Edit"
                        src="https://c.animaapp.com/1ptxcx7H/img/edit-1@2x.png"
                        onClick={() => {
                          setPopupVisibility("overlay", true);
                          setPopupVisibility("detailChange", true);
                          setTempContent('tempObject' ,d);
                        }}
                      />
                      <img
                        className="w-6 h-6 cursor-pointer transition duration-300 transform hover:scale-110"
                        alt="Trash"
                        src="https://c.animaapp.com/1ptxcx7H/img/trash-1@2x.png"
                        onClick={() => {
                          //updateData(default.voertuigId);
                          setPopupVisibility("overlay", true);
                          setPopupVisibility("Popup", true);
                          setTempContent('tempId' , id);
                        }}
                      />
                    </td>
                  </tr>
                </>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default Table;
