﻿$(function () {
    $("#sortable").sortable();
    $("#sortable").disableSelection();
});
function startTime() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = checkTime(m);
    s = checkTime(s);
    document.getElementById('clock').innerHTML = h + ":" + m + ":" + s;
    var t = setTimeout(function () { startTime() }, 500);
    document.getElementById('date').innerHTML = (new Date()).toDateString();
}

function checkTime(i) {
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}
