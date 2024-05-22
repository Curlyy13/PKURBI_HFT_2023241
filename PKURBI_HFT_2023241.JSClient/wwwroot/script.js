let realestates = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35487/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("RealEstateCreated", (user, message) => {
        getdata();
    });

    connection.on("RealEstateDeleted", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata(){
    await fetch('http://localhost:35487/RealEstate')
        .then(x => x.json())
        .then(y => {
            realestates = y;
            //console.log(realestates);
            display();
        })
}



function display() {
    document.getElementById('contentarea').innerHTML = "";
    realestates.forEach(t => {
        document.getElementById('contentarea').innerHTML += "<tr><td>" + t.realEstateId + "</td><td>" + t.realEstateCity + "</td><td>" + t.realEstateValue + "</td><td>" + t.basicArea + "</td><td>" +/*"</td><td>" + t.salesId +*/ `<button type="button" onclick="Remove(${t.realEstateId})">Delete</button>` +"</td></tr>";
    });
}

function create() {
    let city = document.getElementById('realestatecity').value;
    let value = document.getElementById('realestatevalue').value;
    let basicarea = document.getElementById('realestatebasicarea').value;
    fetch('http://localhost:35487/RealEstate/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { realEstateCity: city, realEstateValue: value, basicArea: basicarea }),})
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function Remove(id) {
    fetch('http://localhost:35487/RealEstate/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

