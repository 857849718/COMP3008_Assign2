function login() {
    var email = document.getElementById('Email').value;

    const request = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(email)
    };

    fetch('/api/login/Login', request)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.auth) {
                alert('login success');
                loadView('userDashBoard');
                loadScript('/js/userDashBoard.js')
            }
            else {
                alert(data.msg);
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}