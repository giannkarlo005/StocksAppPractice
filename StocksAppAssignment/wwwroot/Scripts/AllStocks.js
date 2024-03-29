﻿var min = 0;
var max = 20;

var isShowLoadStockButtonShown = true;

document.getElementById('top-25').style.display = 'inline-block';
document.getElementById('all-stocks').style.display = 'none';
document.getElementById('trade-link').style.display = 'none';
document.getElementById('order-link').style.display = 'none';

var createTableRows = (min, max) => {
    var strHtml = '';
    for (var i = min; i < max; i++) {
        if (!model[i]) {
            return strHtml;
        }
        strHtml += "<tr>"
            + "<td>" + model[i].currency + "</td>"
            + "<td><a class=\"cursor-pointer\" href=\"/get-company-stockPrice/" + model[i].symbol + "\">" + model[i].description + "</a></td>"
            + "<td>" + model[i].displaySymbol + "</td>"
            + "<td>" + model[i].symbol + "</td>"
            + "<td>" + model[i].type + "</td>"
            + "</tr>"
    }
    return strHtml;
};

var loadNextItems = () => {
    var strHtml = createTableRows(min, max);
    document.getElementById("stock-list").insertAdjacentHTML('beforeend', strHtml);

    min = max;
    max += 20;
}
var loadStocks = () => {
    if (!isShowLoadStockButtonShown) {
        return;
    }

    var strHtml = createTableRows(min, max);
    document.getElementById("stock-list").insertAdjacentHTML('afterbegin', strHtml);

    isShowLoadStockButtonShown = false;
    document.getElementById('load-stocks-button').style.display = 'none';
    document.getElementById('stocks-table').style.display = 'inline-block';
    document.getElementById('load-more-button').style.display = 'inline-block';

    min = max;
    max += 20;
}