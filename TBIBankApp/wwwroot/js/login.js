document.onkeyup = function (e) {
    if (e.which == 13) {
        login(e);
    }
}

$('.login100-form-btn').click(
    function (e) {
        login(e);
    })



function login(e) {
    let username = $('#inputUserName').val();
    let password = $('#inputPassword').val();
    var a = false;
    let data = {
        'username': username,
        'password': password,
        'rememberme': false
    }
    if (!username) {
        $('#focus-UserName').text('Please enter username!');
        e.preventDefault();
    }
    if (username) {
        $('#focus-UserName').text('');
    }
    if (!password) {
        $('#focus-Password').text('Please enter password!');
        e.preventDefault();
    }
    if (password) {
        $('#focus-Password').text('');
    }
    if (password && username) {
        $.ajax(
            {
                type: "POST",
                url: "/Home/CheckForUserNameAndPassowrdAsync",

                data: data,
                dataType: 'JSON',
                success: function (returndata) {
                    console.log(returndata);
                    if (returndata === "false") {
                        console.log(returndata);
                        $('#focus-Password').text('Invalid username or password!');
                    }
                    else if (returndata === "true") {
                        console.log(returndata);
                        $('#focus-Password').text('');
                        window.location.replace("/Home/Dashboard");
                    }
                    else if (returndata === "maxlogedusers") {
                        $('#focus-Password').text('There are already the limit of logged users in the site!');
                        //window.location.replace("/Home/ChangePassword")
                    }
                },
                error: function (response) {
                    console.log(response);
                    $("div:first").replaceWith(response.responseText);

                }
            })
    }
}


