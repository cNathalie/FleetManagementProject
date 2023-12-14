import React from 'react';
import ReactPaginate from 'react-paginate';
import '../constants/table_foot.css'; // Import the CSS file

const Pagination = ({ pageCount, onPageChange }) => {
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
      containerClassName={'pagination-container'}
      subContainerClassName={'pages pagination'}
      activeClassName={'active'}
      pageClassName={'pagination-item'}
      pageLinkClassName={'pagination-link'}
    />
  );
};

export default Pagination;
