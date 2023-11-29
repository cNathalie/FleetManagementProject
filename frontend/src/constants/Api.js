export const baseUrl = "http://localhost:5100/";

//#region Voertuigen Api
export const getVoertuigen = () => {
  return fetch(baseUrl + "Voertuig")
    .then((response) => {
      if (!response.ok) {
        throw new Error(`Failed to fetch data. Status: ${response.status}`);
      }
      return response.json();
    })
    .catch((error) => {
      console.error("Error fetching data:", error.message);
      throw error; // Rethrow the error to handle it at the calling site if needed
    });
};



export const DeleteVoertuig = (tempid) => {
  fetch(baseUrl + `Voertuig/id?id=${tempid}`, { method: "DELETE" });
};

export const UpdateVoertuig = (formData) => {
  fetch(baseUrl + "Voertuig", {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};

export const getVoertuig = (tempid) => {
  fetch(baseUrl + `Voertuig/id?id=${tempid}`, { method: "GET" });
};

export const PostVoertuig = (formData) => {
  fetch(baseUrl + "Voertuig", {
    method: "POST",
    headers: {
      Accept: "*/*",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};
//#endregion

//#region Tankkaarten Api
export const getTankkaarten = async () => {
  console.log(baseUrl + "Tankkaarten");
  const response = await fetch(baseUrl + "Tankkaarten");
  const data = await response.json();
  return data;
};

export const DeleteTankkaart = (tempid) => {
  fetch(baseUrl + `Tankkaarten/id?id=${tempid}`, { method: "DELETE" });
};

export const UpdateTankkaart = (formData) => {
  fetch(baseUrl + "Tankkaarten", {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};

export const getTankkaart = (tempid) => {
  fetch(baseUrl + `Tankkaarten/id?id=${tempid}`, { method: "GET" });
};

export const PostTankkaart = (formData) => {
  fetch(baseUrl + "Tankkaarten", {
    method: "POST",
    headers: {
      Accept: "*/*",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};
//#endregion

//#region Bestuurders Api
export const getBestuurders = async () => {
  const response = await fetch(baseUrl + "Bestuurders");
  const data = await response.json();
  return data;
};

export const DeleteBestuurder = (tempid) => {
  fetch(baseUrl + `Bestuurders/id?id=${tempid}`, { method: "DELETE" });
};

export const UpdateBestuurder = (formData) => {
  fetch(baseUrl + "Bestuurders", {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};

export const getBestuurder = (tempid) => {
  fetch(baseUrl + `Bestuurders/id?id=${tempid}`, { method: "GET" });
};

export const PostBestuurder = (formData) => {
  fetch(baseUrl + "Bestuurders", {
    method: "POST",
    headers: {
      Accept: "*/*",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};
//#endregion

//#region Fleets Api

export const getFleets = async () => {
  const response = await fetch(baseUrl + "Fleet");
  const data = await response.json();
  return data;
};

export const DeleteFleet = (tempid) => {
  fetch(baseUrl + `Fleet/id?id=${tempid}`, { method: "DELETE" });
};

export const UpdateFleet = (formData) => {
  fetch(baseUrl + "Fleet", {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};

export const getFleet = (tempid) => {
  fetch(baseUrl + `Fleet/id?id=${tempid}`, { method: "GET" });
};

export const PostFleet = (formData) => {
  fetch(baseUrl + "Fleet", {
    method: "POST",
    headers: {
      Accept: "*/*",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(formData),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      console.log("Success:", data);
    })
    .catch((error) => {
      console.error("There was a problem with the fetch operation:", error);
    });
};
//#endregion

//#region typeWagen Api
export const getTypeWagen = () => {
  return fetch(baseUrl + "TypeWagen")
    .then((response) => {
      if (!response.ok) {
        throw new Error(`Failed to fetch data. Status: ${response.status}`);
      }
      return response.json();
    })
    .then((data) => data)
    .catch((error) => {
      console.error("Error fetching data:", error.message);
      throw error; // Rethrow the error to handle it at the calling site if needed
    });
};
//#endregion


//#region BrandstofTypes Api
export const getBrandstofTypes = () => {
  return fetch(baseUrl + "BrandstofTypes")
    .then((response) => {
      if (!response.ok) {
        throw new Error(`Failed to fetch data. Status: ${response.status}`);
      }
      return response.json();
    })
    .then((data) => data)
    .catch((error) => {
      console.error("Error fetching data:", error.message);
      throw error; // Rethrow the error to handle it at the calling site if needed
    });
};


//#endregion