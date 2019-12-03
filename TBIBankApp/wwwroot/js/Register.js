document.onkeyup = function (ev) {
    if (ev.which == 13) {
        register(ev);
    }
}

$('#RegisterButton').click(
    
    function (e) {
        console.log(225);
        register(e);
    });

function register(e) {
    
    let password = $('#password').val();
    let confirmpassword = $('#confirm-password').val();
    let username = $('#your-name').val();
    let email = $('#your-email').val();
    let firstName = $('#your-firstName').val();
    let lastName = $('#your-lastName').val();
    let role;
    if (document.getElementById('operator').checked) {
        role = 'Operator';
    }
    else {
        role = 'Manager';
    }
    let flag = 0;
    let data =
    {
        'Email': email,
        'UserName': username,
        'Password': password,
        'FirstName': firstName,
        'LastName': lastName,
        'Role': role
    }
    if (!(password === confirmpassword)) {
        $('#ConfirmPassword').text('Password does not match!');
        flag = 1;
    }
    else {
        $('#ConfirmPassword').text('');
    }
    if (!email) {
        $('#validate-email').text('Please enter email');
        flag = 1;
    }
    else {
        $('#validate-email').text('');
    }
    if (!username) {
        $('#validate-name').text('Please enter Username');
        flag = 1;
    }
    else {
        $('#validate-name').text('');
    }
    if (!password) {
        $('#validate-password').text('Please enter password');
        flag = 1;
    }
    else {
        $('#validate-password').text('');
    }
    if (!confirmpassword) {
        $('#ConfirmPassword').text('Please enter password');
        flag = 1;
    }
    else if (password === confirmpassword) {
        $('#ConfirmPassword').text('');
    }
    if (!firstName) {
        $('#validate-firstName').text('Please neter first name');
    }
    else {
        $('#validate-firstName').text('');

    }
    if (!lastName) {
        $('#validate-lastName').text('Please neter last name');
    }
    else {
        $('#validate-lastName').text('');
    }
    if (flag === 1) {
        e.preventDefault();
    }
    else {

    $.ajax(
        {
            type: "Post",
            url: "/Manager/CheckForUserAndEmailAsync",
            //headers: {
            //    RequestVerificationToken:
            //        $('input:hidden[name="__RequestVerificationToken"]').val(),
            //},
            data: data,
            success: function (returndata) {
                console.log(returndata);
                if (returndata === "true email") {
                    $('#validate-email').text('There is already registered user with this email!');
                    e.preventDefault();
                }
                else {
                    $('#validate-email').text('');
                }
                if (returndata === "true user") {
                    $('#validate-name').text('There is already registered user with this username')
                    e.preventDefault();
                }
                else {
                    $('#validate-name').text('');
                    e.preventDefault();
                }
                if (returndata !== "true email" && returndata !== "true user") {
                    $.ajax({
                        type: "POST",
                        url: "/Manager/RegisterUserAsync",
                        //headers: {
                        //    RequestVerificationToken:
                        //        $('input:hidden[name="__RequestVerificationToken"]').val(),
                        //    'Accept': 'application/json',
                        //    'Content-Type': 'application/json'
                        //},
                        data: data,
                        success: function (response) {
                            //window.location.replace("/Manager/Register")
                            document.getElementById("reg-form").reset();
                            toastr.success("Successfuly registration!");
                        }
                    })
                }
            }
        })
    }
}