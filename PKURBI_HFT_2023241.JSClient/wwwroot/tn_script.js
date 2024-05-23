let tenants = [];
let connection = null;
let tenantIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35487/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TenantCreated", (user, message) => {
        getdata();
    });

    connection.on("TenantDeleted", (user, message) => {
        getdata();
    });

    connection.on("TenantUpdated", (user, message) => {
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
    await fetch('http://localhost:35487/Tenant')
        .then(x => x.json())
        .then(y => {
            tenants = y;
            //console.log(tenants);
            display();
        })
}



function display() {
    document.getElementById('contentarea').innerHTML = "";
    tenants.forEach(t => {
        document.getElementById('contentarea').innerHTML += "<tr><td>" + t.tenantId + "</td><td>" + t.name + "</td><td>" + t.phone + "</td><td>" +/*"</td><td>" + t.salesId +*/
            `<button type="button" onclick="Remove(${t.tenantId})">Delete</button>` +
            `<button type="button" onclick="showUpdate(${t.tenantId})">Update</button>` + "</td></tr>";
    });
}

function create() {
    let tenantname = document.getElementById('tenantname').value;
    let tenantphone = document.getElementById('tenantphone').value;
    fetch('http://localhost:35487/Tenant/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: tenantname, phone: tenantphone }),
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
    let tenantname = document.getElementById('tenantnameupdate').value;
    let tenantphone = document.getElementById('tenantphoneupdate').value;
    fetch('http://localhost:35487/Tenant/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: tenantname, phone: tenantphone, tenantId: tenantIdToUpdate }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function Remove(id) {
    fetch('http://localhost:35487/Tenant/' + id, {
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
    document.getElementById('tenantnameupdate').value = tenants.find(t => t['tenantId'] == id)['name'];
    document.getElementById('tenantphoneupdate').value = tenants.find(t => t['tenantId'] == id)['phone'];
    document.getElementById('updateformdiv').style.display = 'flex';
    tenantIdToUpdate = id;
}
