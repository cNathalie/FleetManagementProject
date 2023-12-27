/* eslint-disable react/jsx-key */
/* eslint-disable react/prop-types */
// eslint-disable-next-line no-unused-vars
import { useState, useEffect } from "react";
import Pagination from "./Table_Pagination";
import {
  BG_STYLES,
  BUTTON_STYLES,
  IMG_STYLES,
  INPUT_STYLES,
  TEXT_STYLES,
} from "../constants/tailwindStyles";
import { useDarkMode } from "../hooks/useDarkMode";

const Table = ({
  tableHeaderContent,
  inputData,
  data,
  setPopupVisibility,
  setTempContent,
  iDname,
  refRemoveItem,
  refDetailChange,
  refDetailDisplay,
  refAddItem,
  refOverlay,
}) => {
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredData, setFilteredData] = useState([]);
  const [currentPage, setCurrentPage] = useState(0);
  const itemsPerPage = 10;

  const { isDarkMode, toggleDarkMode } = useDarkMode();

  useEffect(() => {
    // Reset currentPage to 0 when search term changes
    setCurrentPage(0);

    // Introduce a delay using setTimeout before updating filtered data
    const timeoutId = setTimeout(() => {
      const updatedFilteredData = data
        ? data.filter((v) =>
            Object.values(v).some(
              (value) =>
                (typeof value === "string" || typeof value === "number") &&
                String(value).toLowerCase().includes(searchTerm.toLowerCase())
            )
          )
        : [];

      setFilteredData(updatedFilteredData);
    }, 500); // Adjust the delay time (in milliseconds) as needed

    // Clear the timeout if the component unmounts or the search term changes again
    return () => clearTimeout(timeoutId);
  }, [data, searchTerm]);

  const indexOfLastItem = (currentPage + 1) * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const pagedData = filteredData.slice(indexOfFirstItem, indexOfLastItem);

  const handlePageChange = ({ selected }) => {
    setCurrentPage(selected);
  };

  return (
    <div className={isDarkMode ? "dark" : ""}>
      <div className="flex items-center justify-center h-screen">
        <div className="max-h-[585px] overflow-y-auto group group scrollbar-thin hover:scrollbar-thumb-gray-100 rounded-xl">
          <table className={TEXT_STYLES.OVERVIEW_TABLEHEAD}>
            <thead className={BG_STYLES.OVERVIEW_TABLEHEADBG}>
              {/* First Section of thead */}
              <tr>
                <th colSpan={tableHeaderContent.length} className="py-3">
                  <div className="flex justify-between items-center">
                    {/* Button on the left */}
                    <button
                      type="button"
                      className={BUTTON_STYLES.OVERVIEW_ADDBUTTON}
                      onClick={() => {
                        setPopupVisibility(refOverlay, true);
                        setPopupVisibility(refAddItem, true);
                      }}
                    >
                      Toevoegen
                    </button>
                    {/* Search input on the right */}
                    <input
                      type="text"
                      value={searchTerm}
                      onChange={(e) => setSearchTerm(e.target.value)}
                      placeholder="Zoek..."
                      className={INPUT_STYLES.OVERVIEW_SEARCH_INPUT}
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
                        className={TEXT_STYLES.OVERVIEW_TABLETITLE}
                      >
                        <div className="flex items-center ">{t}</div>
                      </th>
                    </>
                  );
                })}
              </tr>
            </thead>
            <tbody>
              {pagedData.map((d) => {
                const id = d[iDname];
                return (
                  <>
                    <tr key={id} className={BG_STYLES.OVERVIEW_TABLEDATABG}>
                      {inputData.map((i) => {
                        return (
                          <th className={TEXT_STYLES.OVERVIEW_TABLEDATA}>
                            {eval(i)}
                          </th>
                        );
                      })}
                      <td className="px-6 py-4 text-right flex items-center">
                        <img
                          className={IMG_STYLES.OVERVIEW_IMG_DETAIL}
                          alt="Bulleted list"
                          src={`${
                            isDarkMode
                              ? "https://c.animaapp.com/NEqUXNGg/img/bulleted-list@2x.png"
                              : "https://c.animaapp.com/1ptxcx7H/img/bulleted-list-1@2x.png"
                          }`}
                          onClick={() => {
                            setPopupVisibility(refOverlay, true);
                            setPopupVisibility(refDetailDisplay, true);
                            setTempContent("tempObject", d);
                          }}
                        />
                        <img
                          className={IMG_STYLES.OVERVIEW_IMG_EDIT}
                          alt="Edit"
                          src={`${
                            isDarkMode
                              ? "https://c.animaapp.com/km1ykSHQ/img/edit@2x.png"
                              : "https://c.animaapp.com/1ptxcx7H/img/edit-1@2x.png"
                          }`}
                          onClick={() => {
                            setPopupVisibility(refOverlay, true);
                            setPopupVisibility(refDetailChange, true);
                            setTempContent("tempObject", d);
                          }}
                        />
                        <img
                          className={IMG_STYLES.OVERVIEW_IMG_DELETE}
                          alt="Trash"
                          src={`${
                            isDarkMode
                              ? "https://c.animaapp.com/7y4T4Xk7/img/trash@2x.png"
                              : "https://c.animaapp.com/1ptxcx7H/img/trash-1@2x.png"
                          }`}
                          onClick={() => {
                            //updateData(default.voertuigId);
                            setPopupVisibility(refOverlay, true);
                            setPopupVisibility(refRemoveItem, true);
                            setTempContent("tempId", id);
                          }}
                        />
                      </td>
                    </tr>
                  </>
                );
              })}
            </tbody>
            <tfoot>
              <Pagination
                pageCount={Math.ceil(filteredData.length / itemsPerPage)}
                onPageChange={handlePageChange}
              />
            </tfoot>
          </table>
        </div>
      </div>
    </div>
  );
};

export default Table;
