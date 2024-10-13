console.log("admin dashboard loaded");

/*
    1.) Create table using document.createElement (append to last element)

*/
function loadAll() {
    fetch('/api/admin/getusers')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            const userTable = document.createElement("table");

            const fNameCol = document.createElement("th");
            fNameCol.innerText = "First Name";
            const lNameCol = document.createElement("th");
            lNameCol.innerText = "Last Name";
            const emailCol = document.createElement("th");
            emailCol.innerText = "Email";
            const addressCol = document.createElement("th");
            addressCol.innerText = "Address";
            const phoneCol = document.createElement("th");
            phoneCol.innerText = "Phone";
            const passwordCol = document.createElement("th");
            passwordCol.innerText = "Password";
            const accountIDCol = document.createElement("th");
            accountIDCol.innerText = "Account ID";

            userTable.appendChild(fNameCol);
            userTable.appendChild(lNameCol);
            userTable.appendChild(emailCol);
            userTable.appendChild(addressCol);
            userTable.appendChild(phoneCol);
            userTable.appendChild(passwordCol);
            userTable.appendChild(accountIDCol);

            const endElement = document.getElementById("adminButtons");
            endElement.appendChild(userTable);
            data.forEach(user => {
                const row = document.createElement("tr");
                row.innerHTML =
                    `<td>${user.firstName}</td><td>${user.lastName}</td><td>${user.email}</td><td>${user.address}</td><td>${user.phone}</td><td>${user.password}</td><td>${user.accountID}</td>`;
                userTable.appendChild(row);
            });
        })
        .catch(error => console.error("Error: ", error));
}

