/*Nguyen Hung Manh*/
"use strict";
function getQueryString() {
    var searchUrl = window.location.search;
    var urlParams = searchUrl.substr(1).split('&');
    var result = {};
    for (var i = 0; i < urlParams.length; i++) {
        var pair = urlParams[i].split('=');
        result[pair[0]] = pair[1] || '';
    }
    return result;
}

function preventDefaultAction(event) {
    event = event || window.event;
    if (event.preventDefault) {
        event.preventDefault();
    } else {
        event.returnValue = false;
    }
}