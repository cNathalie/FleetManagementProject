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

export const UpdateVoertuig = (formData) => {
    fetch(baseUrl + 'Voertuig', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
    });
};

export const getVoertuig = (tempid) => {
    fetch(baseUrl + `Voertuig/id?id=${tempid}`, { method: 'GET' });
    };


export const PostVoertuig = (formData) => {
    fetch(baseUrl + 'Voertuig', {
        method: 'POST',
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
    });
};