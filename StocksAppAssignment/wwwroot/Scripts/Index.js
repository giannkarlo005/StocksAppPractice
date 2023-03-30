document.getElementById('all-stocks').style.display = 'inline-block';
document.getElementById('top-25').style.display = 'inline-block';
document.getElementById('trade-link').style.borderBottom = "2px solid #626262";
document.getElementById('order-link').style.borderBottom = "none";

var alertUser = (message) => {
    var isConfirmed = window.confirm(message);
    if (isConfirmed) {
        document.getElementById('order-quantity').value = "";
    }
};

var onSellOrderClick = async () => {
    var orderQuantity = document.getElementById('order-quantity').value;
    var data = {
        StockName: stockName,
        StockSymbol: stockSymbol,
        OrderQuantity: orderQuantity,
        OrderPrice: stockPrice,
        DateAndTimeOfOrder: null
    };

    await fetch("/sell-order", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    //alertUser("Stock Successfully Queued for Sell");
};

var onBuyOrderClick = async () => {
    var orderQuantity = document.getElementById('order-quantity').value;
    var data = {
        StockName: stockName,
        StockSymbol: stockSymbol,
        OrderQuantity: orderQuantity,
        OrderPrice: stockPrice,
        DateAndTimeOfOrder: null
    };

    var response = await fetch("/buy-order", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
    var responseText = await response.text();
    console.log(responseText);

    //window.location.pathname = JSON.parse(responseText);
    //alertUser("Stock Successfully Queued for Buy");
};