let salespeople = [];
let connection = null;
let salespersonIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35487/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("SalespersonCreated", (user, message) => {
        getdata();
    });

    connection.on("SalespersonDeleted", (user, message) => {
        getdata();
    });

    connection.on("SalespersonDeleted", (user, message) => {
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

async function getdata() {
    await fetch('http://localhost:35487/Salesperson')
        .then(x => x.json())
        .then(y => {
            salespeople = y;
            //console.log(salespeople);
            display();
        })
}



function display() {
    document.getElementById('contentarea').innerHTML = "";
    salespeople.forEach(t => {
        document.getElementById('contentarea').innerHTML += "<tr><td>" + t.salesId + "</td><td>" + t.name + "</td><td>" + t.age + "</td><td>" + /*"</td><td>" + t.salesId +*/
            `<button type="button" onclick="Remove(${t.salesId})">Delete</button>` +
            `<button type="button" onclick="showUpdate(${t.salesId})">Update</button>` + "</td></tr>";
    });
}

function create() {
    let salesname = document.getElementById('salespersonname').value;
    let salesage = document.getElementById('salespersonage').value;
    fetch('http://localhost:35487/Salesperson/', { //!!!!!!!!!!!!!!!!!!!!!!!!
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: salesname, age: salesage }), //!!!!!!!!!!!!!!!!!!!!!!
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let salesname = document.getElementById('salespersonnameupdate').value;
    let salesage = document.getElementById('salespersonageupdate').value;
    fetch('http://localhost:35487/Salesperson/', { //!!!!!!!!!!!!!!!!!!!!!!!!!!
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: salesname, age: salesage, salesId: salespersonIdToUpdate }), //!!!!!!!!!!!!!!!!!!!!!!!!!!!!4
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function Remove(id) {
    fetch('http://localhost:35487/Salesperson/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showUpdate(id) {
    document.getElementById('salespersonnameupdate').value = salespeople.find(t => t['salesId'] == id)['name'];
    document.getElementById('salespersonageupdate').value = salespeople.find(t => t['salesId'] == id)['age'];
    document.getElementById('updateformdiv').style.display = 'flex';
    salespersonIdToUpdate = id;
}
