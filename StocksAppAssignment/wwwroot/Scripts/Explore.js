document.getElementById('all-stocks').style.display = 'inline-block';
document.getElementById('top-25').style.display = 'inline-block';
document.getElementById('trade-link').style.display = 'none';
document.getElementById('order-link').style.display = 'none';

var onStockSelected = async (stockSymbol) => {
    var selectedStockSymbol = document.getElementById(stockSymbol).innerHTML;

    var response = await fetch("/stocks/explore", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(selectedStockSymbol)
    });
    var companyDetails = await response.text();
    document.getElementById("selected-stock").innerHTML = companyDetails;

};