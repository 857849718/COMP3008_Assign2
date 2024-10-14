// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadScript(scriptSRC) {
    var script = document.createElement("script");
    script.src = scriptSRC;
    document.body.appendChild(script);
}

function loadView(view) {
    var apiUrl;
    if (view == 'adminDashboard') {
        apiUrl = 'api/admin/ShowAdminDashboard';
        console.log("Loading admin view");
    }
    if (view == 'loginForm' || view == 'userDashBoard') {
        apiUrl = '/api/user/ShowLoginForm';
        console.log("loading login view");
    }
    if (view == 'updateInfoForm') {
        apiUrl = 'api/user/ShowUserInfoUpdateForm';
        console.log("Loading user info update view");
    }
    if (view == 'transacHistory') {
        apiUrl = 'api/user/ShowTransacHistory';
        console.log("Loading transaction history view");
    }

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.text();
        })
        .then(data => {
            document.getElementById("main").innerHTML = data;
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
function loadLoginForm() {
    loadView('loginForm');
    loadScript('/js/login.js');
    loadScript('/js/userDashBoard.js');
    loadScript('/js/adminDashboard.js');
}

function loginAuthentication() {
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    const loginAPI = "api/user/authenticate";
}

function logOut() {
    const request = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    };

    fetch('/api/user/logout', request)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(response => {
            if (response.success) {
                alert(response.msg);
                window.location.reload();
            }
            else {
                alert(response.msg);
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

document.addEventListener("DOMContentLoaded", loadLoginForm);