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
    if (view == 'loginForm' || view == 'userDashBoard') {
        apiUrl = '/api/user/ShowLoginForm';
        console.log("loading login view");
    }
    if (view == 'adminDashboard') {
        apiUrl = 'api/admin/ShowAdminDashboard';
        loadScript('/js/adminDashboard.js');
        console.log("Loading admin view");
    }
        
    //if (view === 'userDashBoard') {
    //    apiUrl = '';
    //    console.log("loading user dashboard view");
    //}


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
}

function loginAuthentication() {
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    const loginAPI = "api/user/authenticate";
}

document.addEventListener("DOMContentLoaded", loadLoginForm);