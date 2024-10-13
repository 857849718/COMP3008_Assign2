console.log("admin dashboard loaded");

/*
    1.) Create table using document.createElement (append to last element)

*/
function loadAll() {
    fetch('/api/admin/GetUsers')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            const userTable = document.createElement("table");
            data.forEach(user => {
                const row = document.createElement("tr");
                row.innerHTML =
                    '<td>${user.FirstName}</td><td>${user.LastName}</td><td>${user.Email}</td><td>${user.Address}</td><td>${user.Phone}</td><td>${user.Password}</td><td>${user.AccountID}</td>';
                userTable.appendChild(row);
            });
        })
        .catch(error => console.error("Error: ", error));
}

