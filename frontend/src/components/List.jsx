/* eslint-disable no-unused-vars */
import React from "react";
/* eslint-disable react/prop-types*/

const List = (props) => {
  const {
    listHeader: {
      eigenaar,
      merk,
      model,
      chassisnummer,
      nummerplaat,
      brandstoftype,
      typeWagen,
      kleur,
      aantalDeuren,
    },
  } = props;

  return (
    <ul>
      <li {...props}>{eigenaar}</li>
      <li {...props}>{merk}</li>
      <li {...props}>{model}</li>
      <li {...props}>{chassisnummer}</li>
      <li {...props}>{nummerplaat}</li>
      <li {...props}>{brandstoftype}</li>
      <li {...props}>{typeWagen}</li>
      <li {...props}>{kleur}</li>
      <li {...props}>{aantalDeuren}</li>
    </ul>
  );
};

export default List;