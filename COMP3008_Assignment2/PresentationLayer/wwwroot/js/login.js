function login() {
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    var data = {
        Email: email,
        Password: password
    };

    const request = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    fetch('/api/user/Login', request)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.adminFlag) {
                console.log("admindashboard");
                loadView('adminDashboard');
            }
            else if (data.auth) {
                console.log("userdashboard");
                loadView('userDashBoard');
            }
            else {
                alert(data.msg);
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}