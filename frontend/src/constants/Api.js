import Axios from 'axios';

export const baseUrl = "http://localhost:5210/";


//#region Voertuigen Api
export const getVoertuigen = () => {
  return fetch(baseUrl + "voertuigen", 
  {method: "GET" , credentials: "include", 
  
    headers: {
      "Content-Type": "application/json",
    },
  })
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
  fetch(
    baseUrl + `voeruigen/${tempid}`,
    { method: "DELETE", credentials: "include" },
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const UpdateVoertuig = (formData) => {
  return new Promise((resolve) => {
    fetch(baseUrl + "voertuigen", {
      method: "PUT",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
      .then((response) => {
        resolve(response);
      })
      .then((data) => {
        console.log("data:", data);
      });
  });
};

export const getVoertuig = (tempid) => {
  fetch(
    baseUrl + `voertuigen${tempid}`,
    { method: "GET", credentials: "include" },
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const PostVoertuig = (formData) => {
  return new Promise((resolve) => {
    fetch(baseUrl + "voertuigen", {
      method: "POST",
      credentials: "include",
      headers: {
        Accept: "*/*",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
      .then((response) => {
        resolve(response);
      })
      .then((data) => {
        console.log("data:", data);
      });
  });
};
//#endregion

//#region Tankkaarten Api
export const getTankkaarten = async () => {
  console.log(baseUrl + "tankkaarten");
  const response = await fetch(baseUrl + "tankkaarten", {
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
  });
  const data = await response.json();
  return data;
};

export const DeleteTankkaart = (tempid) => {
  fetch(
    baseUrl + `tankkaarten/${tempid}`,
    { method: "DELETE" , credentials: "include"},
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const UpdateTankkaart = (formData) => {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + "tankkaarten", {
      method: "PUT",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        resolve(response);
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
        reject(error);
      });
  });
};

export const getTankkaart = (tempid) => {
  fetch(
    baseUrl + `tankkaarten/${tempid}`,
    { method: "GET" , credentials: "include"},
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const PostTankkaart = (formData) => {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + "tankkaarten", {
      method: "POST",
      credentials: "include",
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
        resolve(response);
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
        reject(error);
      });
  });
};
//#endregion

//#region Bestuurders Api
export const getBestuurders = async () => {
  const response = await fetch(baseUrl + "bestuurders", {
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
  });
  const data = await response.json();
  return data;
};

export const DeleteBestuurder = (tempid) => {
  fetch(
    baseUrl + `bestuurders/${tempid}`,
    { method: "DELETE" , credentials: "include"},
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const UpdateBestuurder = (formData) => {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + "bestuurders", {
      method: "PUT",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        resolve(response);
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
        reject(error);
      });
  });
};

export const getBestuurder = (tempid) => {
  fetch(
    baseUrl + `bestuurders/${tempid}`,
    { method: "GET", credentials: "include" },
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const PostBestuurder = (formData) => {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + "bestuurders", {
      method: "POST",
      credentials: "include",
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
        resolve(response);
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
        reject(error);
      });
  });
};
//#endregion

//#region Fleets Api

export const getFleets = async () => {
  const response = await fetch(baseUrl + "fleet", {
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
  });
  const data = await response.json();
  return data;
};

export const DeleteFleet = (tempid) => {
  fetch(
    baseUrl + `fleet/${tempid}`,
    { method: "DELETE", credentials: "include" },
    {
      headers: {
        "Content-Type": "application/json",
      },
    }
  );
};

export const UpdateFleet = (formData) => {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + "fleet", {
      method: "PUT",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        resolve(response);
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
        reject(error);
      });
  });
};

export const getFleet = (tempid) => {
  fetch(baseUrl + `fleet/${tempid}`, { method: "GET", credentials: "include" });
};

export const PostFleet = (formData) => {
  return new Promise((resolve, reject) => {
    fetch(baseUrl + "fleet", {
      method: "POST",
      credentials: "include",
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
        resolve(response);
      })
      .then((data) => {
        console.log("Success:", data);
      })
      .catch((error) => {
        console.error("There was a problem with the fetch operation:", error);
        reject(error);
      });
  });
};
//#endregion

//#region typeWagen Api
export const getTypeWagen = () => {
  return fetch(baseUrl + "typeswagen", 
  { 
    method: "GET",
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
  })
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
  return fetch(baseUrl + "brandstoftypes", {
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error(`Failed to fetch data. Status: ${response.status}`);
      }
      console.log(response);
      return response.json();
    })
    .then((data) => data)
    .catch((error) => {
      console.error("Error fetching data:", error.message);
      throw error; // Rethrow the error to handle it at the calling site if needed
    });
};

//#endregion

//#region RijbewijsTypes Api

export const getTypeRijbewijs = () => {
  return fetch(baseUrl + "typesrijbewijs", {
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
  })
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

//#endregion


// ADMIN PAGE

export const getAllUsers = async () => {
  try {
    const response = await Axios.get(
      baseUrl + "accounts",
      {
        withCredentials: true,
      }
    );

    return response.data;

  } catch (error) {
    console.log(error);
  }
};

export const addUser = async (formData) => {

  const role = formData.isAdmin ? "Admin" : "User";

  try {
  const response = await Axios.post(
    baseUrl + "accounts/register",
    {
      email: formData.email,
      password: formData.password,
      role: role,
    },
    {
      withCredentials: true,
    }
  );
  console.log(response.data);
  return true;

  } catch (error){
    console.log(error);
    return false;
  }
}

const findUserbyEmail = async (userToFind) => {
  const allUsers = await getAllUsers();
  const user = allUsers.find(u => u.email === userToFind);
  console.log(user);
  return user;
}

export const removeUser = async (userToRemove) => {
  const user = await findUserbyEmail(userToRemove);
  console.log(user);
  try{
    const response = await Axios.delete(baseUrl + `accounts/${user.userId}`, 
    {
      withCredentials: true
    });
    return response.status
  }
  catch(error){
    console.log(error);
  }
}