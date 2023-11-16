// eslint-disable-next-line no-unused-vars
import React from 'react'
import Nav from "../components/Nav"
import Table from '../components/Table'

    
const VoertuigenPage = () => {
  const tableHeaderContent = ["Merk", "Model", "Chasisnummer", "Nummerplaat", "Brandstoftype", "Acties"];

  return (
    <>
      <Nav />
      <Table tableHeaderContent={tableHeaderContent} />
    </>
  )
}
    
export default VoertuigenPage;