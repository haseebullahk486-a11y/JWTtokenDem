// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Register Action Method
$(document).ready(function () {

    $('#Register').click(function () { 
        let data = {
            UserName: $("#UserName").val(),
            Email: $("#Email").val(),
            Password: $("#Password").val(),
            City: $("#City").val()
        };

        $.ajax({
            url: "/Login/Register",
            type: "POST",
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                hideModal();
                alert("User registered successfully!");
            },
            error: function (err) {
                alert("Error: " + err.responseText);
            }
        });
    });

    $('#OpenRegister').click(function () {
        $('#registerModal').modal("show");
    });
});

function hideModal() {
    $('#registerModal').modal("hide");
    $("#UserName").val('');
    $("#Email").val('');
    $("#Password").val('');
    $("#City").val('');
}


//Login Action Start


$(document).ready(function () {

    
    if (localStorage.getItem('jwt')) {
        alert("You are already logged in!");
        break; 
    }

    $('#Login').click(function () {
        let data = {
            Email: $("#Email").val(),
            Password: $("#Password").val(),
        };

        $.ajax({
            url: "/Login/Login",
            type: "POST",
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (response) {
                hideModal();
                localStorage.setItem('jwt',response.token)
                alert("User Login successfully!");
            },
            error: function (err) {
                alert("Error: " + err.responseText);
            }
        });
    });

    $('#OpenLogin').click(function () {
        $('#LoginModal').modal("show");
    });
});

function hideModal() {
    $('#LoginModal').modal("hide");
    $("#Email").val('');
    $("#Password").val('');
}

$("#logoutLink").click(function () {
    localStorage.removeItem("jwt");
    alert("Logged out successfully!");
});










