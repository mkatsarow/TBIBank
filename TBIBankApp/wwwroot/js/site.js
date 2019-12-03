// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.onkeyup = function (e) {
    if (e.altKey && e.which === 66) {
        window.location.replace("/Manager/Register");
    }
    if (e.altKey && e.which == 73) {
        window.location.replace("/Email/ListEmailsAsync?emailStatus=InvalidApplication")
    }
    if (e.altKey && e.which == 82) {
        window.location.replace("/Email/ListEmailsAsync?emailStatus=NotReviewed");
    }
    if (e.altKey && e.which == 79) {
        window.location.replace("/Email/ListEmailsAsync?emailStatus=Open");
    }
    if (e.altKey && e.which == 67) {
        window.location.replace("/Email/ListEmailsAsync?emailStatus=Closed");
    }
    if (e.altKey && e.which == 78) {
        window.location.replace("/Email/ListEmailsAsync?emailStatus=New");
    }
    //$(document).ready(function () {
    //    $(window).bind("beforeunload", function () {
    //        return confirm("Do you really want to close?");
    //    });
    //});
}