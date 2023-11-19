export const baseUrl = "http://localhost:5100/";

export const getVoertuigen = async () => {
    console.log(baseUrl + 'Voertuig')
    const response = await fetch(baseUrl + 'Voertuig');
    const data = await response.json();
    return(data);
    };

export const DeleteVoertuig = (tempid) => {
    fetch(baseUrl + `Voertuig/id?id=${tempid}`, { method: 'DELETE' });
    };



