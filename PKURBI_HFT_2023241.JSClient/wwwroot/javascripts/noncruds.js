let AvgPriceBySalesperson = [];
let BasicInformation = [];
let AvgPriceByCity = [];
let MostRealEstate = [];
let TenantsEstate = [];
let connection = null;
//getdata();
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

    connection.on("RealEstateCreated", (user, message) => {
        getdata();
    });

    connection.on("RealEstateDeleted", (user, message) => {
        getdata();
    });

    connection.on("RealEstateUpdated", (user, message) => {
        getdata();
    });

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
    //getAvgPriceBySalesperson(id);
    //GetBasicInformationID(id);
    //getAvgPriceByCity();
    //getMostRealEstates();
    //getTenantsEstate();
}
async function getAvgPriceBySalesperson(id) {
    return await fetch('http://localhost:35487/NCRealEstate/AvgPriceBySalespersonID/' + id)
        .then(x => x.json())
        .then(y => {
            AvgPriceBySalesperson = y;
            console.log(AvgPriceBySalesperson)
            return AvgPriceBySalesperson;
        });
}

async function GetBasicInformationID(id) {
    return await fetch('http://localhost:35487/NCRealEstate/BasicInformation/' + id)
        .then(x => x.json())
        .then(y => {
            BasicInformation = y;
            console.log(BasicInformation)
            return BasicInformation;
        });
}


async function AvgPriceBySalespersonID() {
    let givenId = 0;
    if (document.getElementById('avgprice_r1').checked) { givenId = Number(document.getElementById('avgprice_r1').value); }
    else if (document.getElementById('avgprice_r2').checked) { givenId = Number(document.getElementById('avgprice_r2').value); }
    else if (document.getElementById('avgprice_r3').checked) { givenId = Number(document.getElementById('avgprice_r3').value); }
    else if (document.getElementById('avgprice_r4').checked){ givenId = Number(document.getElementById('avgprice_r4').value); }
    AvgPriceBySalesperson = await getAvgPriceBySalesperson(givenId);
    Number(AvgPriceBySalesperson);
    AvgPriceBySalesperson = Math.round(AvgPriceBySalesperson);
    document.getElementById('avgprice_result').innerHTML = 'Az átlag RealEstate ára a megadott ID-val rendelkező Salesperson-nek: ' + '<b>' + AvgPriceBySalesperson + ' </b>';
}



async function BasicInformationID() {
    let givenId = document.getElementById('basicinformation_id').value;
    BasicInformation = await GetBasicInformationID(givenId);
    document.getElementById('bi_loc').innerHTML = BasicInformation.location;
    document.getElementById('bi_val').innerHTML = BasicInformation.value;
    document.getElementById('bi_area').innerHTML = BasicInformation.area;
    document.getElementById('bi_salp').innerHTML = BasicInformation.salesperson;
    document.getElementById('bi_tenant').innerHTML = BasicInformation.tenant;
    document.getElementById('bi_cont').innerHTML = BasicInformation.tenantContact;
}

async function AvgPriceByCities() {
    await fetch('http://localhost:35487/NCRealEstate/AvgPriceByCity/')
        .then(x => x.json())
        .then(y => {
            AvgPriceByCity = y;
            console.log(AvgPriceByCity);
            showAvgPriceByCities();
        });
}

async function MostRealEstates() {
    await fetch('http://localhost:35487/NCSalesperson/MostRealEstates/')
        .then(x => x.json())
        .then(y => {
            MostRealEstate = y;
            console.log(MostRealEstate);
            showMostRealEstates();
        });
}

async function TenantsByEstates() {
    await fetch('http://localhost:35487/NCTenant/TenantsByCity/')
        .then(x => x.json())
        .then(y => {
            TenantsEstate = y;
            console.log(TenantsEstate);
            showTenantsByEstates();
        });
}

function showAvgPriceByCities() {
    document.getElementById('avgpricebycity_body').innerHTML = "";
    document.getElementById('avgpricebycity_body').innerHTML = `<tr><th colspan="2"><input type="button" value="Lekérdez" onclick="AvgPriceByCities()"/></th></tr>`
    AvgPriceByCity.forEach(t => {
        document.getElementById('avgpricebycity_body').innerHTML += `<tr><th>${t.city}:</th><th>${t.avgPrice}</th></tr>`
    });
}

function showMostRealEstates() {
    document.getElementById('mostrealestates_body').innerHTML = `<tr><th colspan="2"><input type="button" value="Lekérdez" onclick="MostRealEstates()" /></th></tr>`;
    let count = 0;
    MostRealEstate.forEach(t => {
        count++;
        document.getElementById('mostrealestates_body').innerHTML += `<tr><th>${count}. ${t}</th></tr>`;
    });
}

function showTenantsByEstates() {
    document.getElementById('tenantsbycity_body').innerHTML = `<tr><th colspan="2"><input type="button" value="Lekérdez" onclick="TenantsByEstates()" /></th></tr><tr><th>Name</th><th>Owned Estates</th></tr>`;
    TenantsEstate.forEach(t => {
        document.getElementById('tenantsbycity_body').innerHTML += `<tr><th>${t.name}</th><th>${t.estateCount}</th></tr>`;
    });
}

