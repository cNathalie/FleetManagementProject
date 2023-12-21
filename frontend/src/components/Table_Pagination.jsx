import React from 'react';
import ReactPaginate from 'react-paginate';
import '../constants/table_foot.css'; // Import the CSS file
import { useDarkMode } from '../hooks/useDarkMode';

const Pagination = ({ pageCount, onPageChange }) => {
  const { isDarkMode } = useDarkMode();

  return (
    <ReactPaginate
      previousLabel={'<'}
      nextLabel={'>'}
      breakLabel={'...'}
      breakClassName={'break-me'}
      pageCount={pageCount}
      marginPagesDisplayed={2}
      pageRangeDisplayed={5}
      onPageChange={onPageChange}
      containerClassName={`pagination-container ${isDarkMode ? 'dark-pagination-container' : ''}`}
      subContainerClassName={'pages pagination'}
      activeClassName={`active ${isDarkMode ? 'dark-active' : ''}`}
      pageClassName={`pagination-item ${isDarkMode ? 'dark-pagination-item' : ''}`}
      pageLinkClassName={`pagination-link ${isDarkMode ? 'dark-pagination-link' : ''}`}
    />
  );
};

export default Pagination;
