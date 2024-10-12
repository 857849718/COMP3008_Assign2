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
    if (view === 'loginForm') {
        apiUrl = '/api/login/ShowLoginForm';
        console.log("loading form view");
    }
        
    if (view === 'userDashBoard') {
        apiUrl = '';
        console.log("loading user dashboard view");
    }


    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.text();
        })
        .then(data => {
            document.getElementById('main').innerHTML = data;
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
function loadLoginForm() {
    loadView('loginForm');
    loadScript('/js/login.js');
}

document.addEventListener("DOMContentLoaded", loadLoginForm);