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

            userTable.style.backgroundColor = "#69ffa6";
            userTable.style.margin = "10px auto";
            userTable.style.padding = "5px";
            userTable.style.border = "2px solid #0f45a9";

            const endElement = document.getElementById("adminButtons");
            endElement.appendChild(userTable);
            data.forEach(user => {
                const row = document.createElement("tr");
                const userInfo = [user.firstName, user.lastName, user.email, user.address, user.phone, user.password, user.accountID];
                userInfo.forEach(info => {
                    const infoCell = document.createElement("td");
                    infoCell.innerText = info;
                    infoCell.style.border = "2px solid #0f45a9";
                    infoCell.padding = "2px";
                    infoCell.textAlign = "center";
                    row.appendChild(infoCell);
                })
                userTable.appendChild(row);
            });

        })
        .catch(error => console.error("Error: ", error));
}

