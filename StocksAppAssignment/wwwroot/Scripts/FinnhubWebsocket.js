var socket = new WebSocket(`wss://ws.finnhub.io?token=${finnhubToken}`);

socket.onopen = function () {
    console.log('window.onopen');
    var obj = {
        'type': 'subscribe',
        'symbol': stockSymbol
    }
    socket.send(JSON.stringify(obj))
};

socket.onmessage = function (e) {
    console.log('window.onmessage');
    if (e.data.type == 'error') {
        return;
    }
    var eventData = JSON.parse(e.data);
    if (!eventData || !eventData.data) {
        return;
    }

    document.getElementById("stock-price").innerHTML = eventData.data[0].p;
};

window.onclose = function () {
    console.log('window.onclose');
    var obj = {
        'type': 'unsubscribe',
        'symbol': stockSymbol
    }
    socket.send(JSON.stringify(obj))
}
