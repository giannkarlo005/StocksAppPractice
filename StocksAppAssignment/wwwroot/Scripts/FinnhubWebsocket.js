var socket = new WebSocket(`wss://ws.finnhub.io?token=${finnhubToken}`);

socket.onopen = function() {
    var obj = {
        'type': 'subscribe',
        'symbol': stockSymbol
    }
    socket.send(JSON.stringify(obj))
};

socket.onmessage = function (e) {
    console.log('socket');
    console.log(e);
    if (e.data.type == 'error') {
        return;
    }
    var eventData = JSON.parse(e.data);
    if (!eventData || !eventData.data) {
        return;
    }

    document.getElementById("stock-price").innerHTML = response.data[0].p;
};

window.onclose = function() {
    var obj = {
        'type': 'unsubscribe',
        'symbol': stockSymbol
    }
    socket.send(JSON.stringify(obj))
}
