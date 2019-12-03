let idleTime = 0;
$(document).ready(function () {
    //Increment the idle time counter every minute.
    let idleInterval = setInterval(timerIncrement, 60000); // 1 minute
    //Zero the idle timer on mouse movement.
    $(this).mousemove(function (e) {
        idleTime = 0;
    });
    $(this).keypress(function (e) {
        idleTime = 0;
    });
});

function timerIncrement() {
    idleTime = idleTime + 1;
    if (idleTime > 14) { // 15 minutes
        $.ajax({
            type: "Get",
            url: "/Home/LogOutAsync",
            success: function () {
                window.location.replace("");
            }
        })
    }
}

//window.onunload = function () {
//    $.ajax({
//        type: "Post",
//        url: "/Home/LogOutAsync",
//    })
//}