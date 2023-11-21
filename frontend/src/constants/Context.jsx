import { createContext, useContext, useState } from 'react';

const DataContext = createContext();

export const DataProvider = ({ children }) => {
  const [tempData, setData] = useState([]);

  const updateData = (newData) => {
    setData(newData);
  };

  return (
    <DataContext.Provider value={{ tempData, updateData }}>
      {children}
    </DataContext.Provider>
  );
};

export const useData = () => {
  const context = useContext(DataContext);
  if (!context) {
    throw new Error('useData must be used within a DataProvider');
  }
  return context;
};
  