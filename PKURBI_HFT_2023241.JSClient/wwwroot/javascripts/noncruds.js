let realestates = [];
let salespeople = [];
let tenants = [];
let connection = null;
getdata();

async function getdata() {
    await fetch('http://localhost:35487/RealEstate')
        .then(x => x.json())
        .then(y => {
            realestates = y;
        })
    //console.log(realestates);

    await fetch('http://localhost:35487/Salesperson')
        .then(x => x.json())
        .then(y => {
            salespeople = y;
        })
    //console.log(salespeople);
    await fetch('http://localhost:35487/Tenant')
        .then(x => x.json())
        .then(y => {
            tenants = y;
        })
    //console.log(tenants);
}

function AvgPriceBySalespersonID() {
    let givenId = -1;
    if (document.getElementById('avgprice_r1').checked) { givenId = Number(document.getElementById('avgprice_r1').value); }
    else if (document.getElementById('avgprice_r2').checked) { givenId = Number(document.getElementById('avgprice_r2').value); }
    else if (document.getElementById('avgprice_r3').checked) { givenId = Number(document.getElementById('avgprice_r3').value); }
    else { givenId = Number(document.getElementById('avgprice_r4').value); }
    //document.getElementById('avgprice_result').innerHTML;
}

