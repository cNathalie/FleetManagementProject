/* eslint-disable no-unused-vars */
import React, {useState} from 'react'
import listHeaders from "../constants/listHeader";
import List from "./List";
import Button from "./Button";
import CheckNoBg from "../assets/Media/CheckNoBg.png";


const DetailChange = () => {
    const [data, setData] = useState({
        eigenaar: 'Test Eigenaar',
    merk: 'Test Merk',
    model: 'Test Model',
    chassisnummer: 'Test Chassisnummer',
    nummerplaat: 'Test Nummerplaat',
    brandstoftype: 'Test Brandstoftype',
    typeWagen: 'Test Type wagen',
    kleur: 'Test Kleur',
    aantalDeuren: 'Test Aantal deuren',
    });

    const [isEditing, setIsEditing] = useState(false);
    const [showCheckMark, setShowSetMark] = useState(false);

    const handleDataChange = (field, value) => {
        setData({
            ...data,
            [field]: value,
        });
    };

    // const handleSaveChanges = () => {
    //     // Logic to save changes to database goes here
    //     setIsEditing(false); // Exit edit mode after saving
    // }

    const handleToggleEditMode = () => {
        if (isEditing){
            // Handle logic to save data to DB
            
            //Show checkmark to show success of actions
            setShowSetMark(true);
            //Timeout for checkmark so it doesn't stay on the screen endlessly
            setTimeout(() => {
              setShowSetMark(false)
            }, 100000);
        }
        setIsEditing(!isEditing) // Toggle between edit and display mode
    }

  return (
    <div className="w-1/2 ml-[25%] rounded-xl bg-[#DBDBDB] ">
      <div>
        <div className="ml-9 mt-6 pt-6 flex justify-between">
          <h1 className="font-mainFont font-titleFontWeigt text-4xl">
            Detailweergave
          </h1>
          <div className="mr-8">
            <Button className="rounded-full bg-whiteText w-10 h-10 font-btnFontWeigt">
              <img src="../src/assets/Media/closeButton.jpg" alt="Close" />
            </Button>
          </div>
        </div>
        <div className="flex flex-wrap">
          <div className="ml-9 mt-14 pb-6">
            {/* Shows headers of the list on left of actual data */}
            {listHeaders.map((h) => {
              return (
                <List
                  className="pb-2 text-lg font-mainFont font-titleFontWeigt"
                  key={h}
                  field={h}
                  value={data[h]}
                  onChange={handleDataChange}
                  eigenaar={h.eigenaar}
                  merk={h.merk}
                  model={h.model}
                  chassisnummer={h.chassisnummer}
                  nummerplaat={h.nummerplaat}
                  brandstoftype={h.brandstoftype}
                  typeWagen={h.typeWagen}
                  kleur={h.kleur}
                  aantalDeuren={h.aantalDeuren}
                  listHeader={h}
                />
              );
            })}
          </div>
          <div className="ml-12 mt-14">
            {/* Data from database/testdata goes here */}
            {/* Display data for each field */}
            {isEditing ? (
                //Edit mode
                <>
                {Object.entries(data).map(([field, value]) => (
                    <div key={field}>
                        <input
                        type="text"
                        id={field}
                        name={field}
                        value={value}
                        onChange={(e) => handleDataChange(field, e.target.value)}
                        className="w-[80%] h-9 p-2 rounded-md border border-b border-blueText text-blueText bg-transparent"
                    />
                    </div>
                ))}
                </>
            ) : (
                // Display mode
                Object.entries(data).map(([field, value]) => (
                <div key={field} className="pb-2 text-s text-[#858585] font-mainFont font-titleFontWeigt py-1">
                    <span>{value}</span>
                </div>
                ))
            )}
            
          </div>
          <div className="mr-3 mb-8 ml-[80%]">
                {showCheckMark && (
                  <div className="flex mt-10">
                    <p className="text-[#858585] font-btnFontWeigt font-Helvetica p-5">Succes!</p>
                    <img src={CheckNoBg} alt="Checkmark" className="w-16 h-16"/>
                  </div>
                )}
                <Button 
                className="w-28 h-[40px] rounded-[10px] font-btnFontWeigt font-Helvetica text-btnFontSize text-[#FFFFFF] bg-blueBtn hover:bg-hoverBtn cursor-pointer"
                onClick={handleToggleEditMode}>
                {isEditing ? 'Opslaan' : 'Bewerk'}
                </Button>
            </div>
        </div>
      </div>
    </div>
  )
}

export default DetailChange