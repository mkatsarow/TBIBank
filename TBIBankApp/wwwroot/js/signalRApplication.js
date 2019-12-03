"use strict";
let connection = new signalR.HubConnectionBuilder().withUrl("/notification").build();

connection.on("LockButton", function (id) {
    let tr = document.getElementById(id + '+classID');
    let butTestDisable = document.getElementById(id);
    $(tr).css('background-color', '#999593');
    butTestDisable.setAttribute("disabled", true);
    $(butTestDisable).css('background-color', '#FF0000');
    $(butTestDisable).text('Denied');
});

connection.on("UnlockButton", function (id) {
    let tr = document.getElementById(id + '+classID');
    let butTestDisable = document.getElementById(id);
    $(tr).css('background-color', 'white');
    $(butTestDisable).removeAttr("disabled");
    $(butTestDisable).css('background-color', '#007bff');
    $(butTestDisable).text('Application Preview');
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});
