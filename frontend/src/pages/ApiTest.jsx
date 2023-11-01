import React, { useEffect, useState } from "react";

const ApiTest = () => {
  const [bestuurders, setBestuurders] = useState([]);

  const getBestuurders = async () => {
    try {
      const response = await fetch("http://localhost:5100/bestuurders");

      if (!response.ok) {
        // Handle non-successful response
        throw new Error(`Request failed with status: ${response.status}`);
      }
      console.log("GET successful");
      const data = await response.json();
      setBestuurders(data);
    } catch (error) {
      console.error(error);
      // Handle the error gracefully (e.g., show an error message to the user)
    }
  };

  useEffect(() => {
    getBestuurders();
    const getBestuurdersInterval = setInterval(() => {
      getBestuurders();
      console.log("Bestuurders new get");
    }, 60000);

    return () => {
      clearInterval(getBestuurdersInterval);
    };
  }, []);

  return (
    <>
      <div>ApiTest Bestuurders</div>
      {bestuurders.map((persoon) => {
        return (
          <p key={persoon.id}>
            {persoon.naam} {persoon.voornaam} {persoon.rijksregisternummer}
          </p>
        );
      })}
    </>
  );
};

export default ApiTest;
