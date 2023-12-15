import Axios from 'axios';
import { sessionStorageItems, sessionStorageValues } from "./sessionStorage";


export const baseUrl = "http://localhost:5210/";

// Authentication
export async function login(email, password) {
  try {
    const response = await Axios.post(
      baseUrl + "accounts/authenticate",
      {
        email: email,
        password: password,
      },
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );

    const { role } = response.data;
    sessionStorage.setItem(sessionStorageItems.accessToken, "accessToken");
    sessionStorage.setItem(sessionStorageItems.refreshToken, "refreshToken");
    sessionStorage.setItem(sessionStorageItems.userRole, role);
    sessionStorage.setItem(sessionStorageItems.isLoggedIn,sessionStorageValues.true);

    console.log("Login authorized by API");
    return { succes: true, role };
  } catch (error) {
    console.error("Login failed: ", error);
    return { succes: false, error: error.response?.data };
  }
}

export function logout() {
  console.log(
    "Closing session, deleting token and userole from sessionStorage"
  );
  sessionStorage.setItem(sessionStorageItems.accesToken, null);
  sessionStorage.setItem(sessionStorageItems.userRole, null);
  sessionStorage.setItem(
    sessionStorageItems.isLoggedIn,
    sessionStorageValues.false
  );
}


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



// VIA HEADR//


// import { sessionStorageItems } from "./sessionStorage";

// const accessToken = sessionStorage.getItem(sessionStorageItems.accessToken);

// export const baseUrl = "http://localhost:5210/";

// //#region Voertuigen Api
// export const getVoertuigen = () => {
//   return fetch(baseUrl + "voertuigen", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json"
//     },
//   })
//     .then((response) => {
//       if (!response.ok) {
//         throw new Error(`Failed to fetch data. Status: ${response.status}`);
//       }
//       return response.json();
//     })
//     .catch((error) => {
//       console.error("Error fetching data:", error.message);
//       throw error; // Rethrow the error to handle it at the calling site if needed
//     });
// };

// export const DeleteVoertuig = (tempid) => {
//   fetch(
//     baseUrl + `voeruigen/${tempid}`,
//     { method: "DELETE" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const UpdateVoertuig = (formData) => {
//   return new Promise((resolve) => {
//     fetch(baseUrl + "voertuigen", {
//       method: "PUT",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("data:", data);
//       });
//   });
// };

// export const getVoertuig = (tempid) => {
//   fetch(
//     baseUrl + `voertuigen${tempid}`,
//     { method: "GET" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const PostVoertuig = (formData) => {
//   return new Promise((resolve) => {
//     fetch(baseUrl + "voertuigen", {
//       method: "POST",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         Accept: "*/*",
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("data:", data);
//       });
//   });
// };
// //#endregion

// //#region Tankkaarten Api
// export const getTankkaarten = async () => {
//   console.log(baseUrl + "tankkaarten");
//   const response = await fetch(baseUrl + "tankkaarten", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json",
//     },
//   });
//   const data = await response.json();
//   return data;
// };

// export const DeleteTankkaart = (tempid) => {
//   fetch(
//     baseUrl + `tankkaarten/${tempid}`,
//     { method: "DELETE" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const UpdateTankkaart = (formData) => {
//   return new Promise((resolve, reject) => {
//     fetch(baseUrl + "tankkaarten", {
//       method: "PUT",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         if (!response.ok) {
//           throw new Error("Network response was not ok");
//         }
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("Success:", data);
//       })
//       .catch((error) => {
//         console.error("There was a problem with the fetch operation:", error);
//         reject(error);
//       });
//   });
// };

// export const getTankkaart = (tempid) => {
//   fetch(
//     baseUrl + `tankkaarten/${tempid}`,
//     { method: "GET" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const PostTankkaart = (formData) => {
//   return new Promise((resolve, reject) => {
//     fetch(baseUrl + "tankkaarten", {
//       method: "POST",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         Accept: "*/*",
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         if (!response.ok) {
//           throw new Error("Network response was not ok");
//         }
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("Success:", data);
//       })
//       .catch((error) => {
//         console.error("There was a problem with the fetch operation:", error);
//         reject(error);
//       });
//   });
// };
// //#endregion

// //#region Bestuurders Api
// export const getBestuurders = async () => {
//   const response = await fetch(baseUrl + "bestuurders", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json",
//     },
//   });
//   const data = await response.json();
//   return data;
// };

// export const DeleteBestuurder = (tempid) => {
//   fetch(
//     baseUrl + `bestuurders/${tempid}`,
//     { method: "DELETE" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const UpdateBestuurder = (formData) => {
//   return new Promise((resolve, reject) => {
//     fetch(baseUrl + "bestuurders", {
//       method: "PUT",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         if (!response.ok) {
//           throw new Error("Network response was not ok");
//         }
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("Success:", data);
//       })
//       .catch((error) => {
//         console.error("There was a problem with the fetch operation:", error);
//         reject(error);
//       });
//   });
// };

// export const getBestuurder = (tempid) => {
//   fetch(
//     baseUrl + `bestuurders/${tempid}`,
//     { method: "GET" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const PostBestuurder = (formData) => {
//   return new Promise((resolve, reject) => {
//     fetch(baseUrl + "bestuurders", {
//       method: "POST",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         Accept: "*/*",
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         if (!response.ok) {
//           throw new Error("Network response was not ok");
//         }
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("Success:", data);
//       })
//       .catch((error) => {
//         console.error("There was a problem with the fetch operation:", error);
//         reject(error);
//       });
//   });
// };
// //#endregion

// //#region Fleets Api

// export const getFleets = async () => {
//   const response = await fetch(baseUrl + "fleet", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json",
//     },
//   });
//   const data = await response.json();
//   return data;
// };

// export const DeleteFleet = (tempid) => {
//   fetch(
//     baseUrl + `fleet/${tempid}`,
//     { method: "DELETE" },
//     {
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//     }
//   );
// };

// export const UpdateFleet = (formData) => {
//   return new Promise((resolve, reject) => {
//     fetch(baseUrl + "fleet", {
//       method: "PUT",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         if (!response.ok) {
//           throw new Error("Network response was not ok");
//         }
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("Success:", data);
//       })
//       .catch((error) => {
//         console.error("There was a problem with the fetch operation:", error);
//         reject(error);
//       });
//   });
// };

// export const getFleet = (tempid) => {
//   fetch(baseUrl + `fleet/${tempid}`, { method: "GET" });
// };

// export const PostFleet = (formData) => {
//   return new Promise((resolve, reject) => {
//     fetch(baseUrl + "fleet", {
//       method: "POST",
//       headers: {
//         Authorization: `Bearer ${accessToken}`,
//         Accept: "*/*",
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(formData),
//     })
//       .then((response) => {
//         if (!response.ok) {
//           throw new Error("Network response was not ok");
//         }
//         resolve(response);
//       })
//       .then((data) => {
//         console.log("Success:", data);
//       })
//       .catch((error) => {
//         console.error("There was a problem with the fetch operation:", error);
//         reject(error);
//       });
//   });
// };
// //#endregion

// //#region typeWagen Api
// export const getTypeWagen = () => {
//   return fetch(baseUrl + "typeswagen", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json",
//     },
//   })
//     .then((response) => {
//       if (!response.ok) {
//         throw new Error(`Failed to fetch data. Status: ${response.status}`);
//       }
//       return response.json();
//     })
//     .then((data) => data)
//     .catch((error) => {
//       console.error("Error fetching data:", error.message);
//       throw error; // Rethrow the error to handle it at the calling site if needed
//     });
// };
// //#endregion

// //#region BrandstofTypes Api

// export const getBrandstofTypes = () => {
//   return fetch(baseUrl + "brandstoftypes", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json",
//     },
//   })
//     .then((response) => {
//       if (!response.ok) {
//         throw new Error(`Failed to fetch data. Status: ${response.status}`);
//       }
//       return response.json();
//     })
//     .then((data) => data)
//     .catch((error) => {
//       console.error("Error fetching data:", error.message);
//       throw error; // Rethrow the error to handle it at the calling site if needed
//     });
// };

// //#endregion

// //#region RijbewijsTypes Api

// export const getTypeRijbewijs = () => {
//   return fetch(baseUrl + "typesrijbewijs", {
//     headers: {
//       Authorization: `Bearer ${accessToken}`,
//       "Content-Type": "application/json",
//     },
//   })
//     .then((response) => {
//       if (!response.ok) {
//         throw new Error(`Failed to fetch data. Status: ${response.status}`);
//       }
//       return response.json();
//     })
//     .catch((error) => {
//       console.error("Error fetching data:", error.message);
//       throw error; // Rethrow the error to handle it at the calling site if needed
//     });
// };

// //#endregion
