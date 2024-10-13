console.log("userDashBoard.js loaded");

function transfer() {
    var amount = document.getElementById('amount').value;
    var accountID = document.getElementById('accountID').value;
    var description = document.getElementById('description').value;

    var data = {
        Amount: amount,
        AccountID: accountID,
        Description: description
    };

    const request = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    fetch('/api/user/transfer', request)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(response => {
            if (response.success)
            {
                alert(response.msg);
                window.location.reload();
            }
            else
            {
                alert(response.msg);
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}