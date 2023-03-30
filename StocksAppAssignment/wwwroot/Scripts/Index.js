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
        OrderQuantity: orderQuantity || 0.0,
        OrderPrice: stockPrice || 0.0,
        DateAndTimeOfOrder: null
    };

    alertUser("Sell Stock Submitted");
    var response = await fetch("/sell-order", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });
    var responseText = await response.text();

    document.querySelector("html").innerHTML = responseText;
};

var onBuyOrderClick = async () => {
    var orderQuantity = document.getElementById('order-quantity').value;
    var data = {
        StockName: stockName,
        StockSymbol: stockSymbol,
        OrderQuantity: orderQuantity || 0.0,
        OrderPrice: stockPrice || 0.0,
        DateAndTimeOfOrder: null
    };

    alertUser("Buy Stock Submitted");
    var response = await fetch("/buy-order", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    })
    var responseText = await response.text();

    document.querySelector("html").innerHTML = responseText;
};