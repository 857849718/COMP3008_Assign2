console.log("admin dashboard loaded");

/*
    1.) Create table using document.createElement (append to last element)

*/
let table = null;

function clearTable() {
    if (table) {
        table.remove();
    }
}
function loadAll() {
    clearTable();
    fetch('/api/admin/getusers')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            const filterBox = document.createElement("select");
            const userTable = document.createElement("table");
            filterBox.innerText = "Filter Users By:";
            const filterChoices = ["Name", "Account ID", "Email"];
            filterChoices.forEach(choice => {
                const option = document.createElement("option");
                option.innerText = choice;
                filterBox.appendChild(option);
            });
            table = userTable;

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
            endElement.appendChild(filterBox);
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

function loadTransactions() {
    clearTable();
    fetch('/api/admin/gettransactions')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            const transacTable = document.createElement("table");
            table = transacTable;

            const amountCol = document.createElement("th");
            amountCol.innerText = "Amount";
            const accountIDCol = document.createElement("th");
            accountIDCol.innerText = "Account ID";
            const descriptionCol = document.createElement("th");
            descriptionCol.innerText = "Description";
            const timeCol = document.createElement("th");
            timeCol.innerText = "Time";

            transacTable.appendChild(amountCol);
            transacTable.appendChild(accountIDCol);
            transacTable.appendChild(descriptionCol);
            transacTable.appendChild(timeCol);

            transacTable.style.backgroundColor = "#69ffa6";
            transacTable.style.margin = "10px auto";
            transacTable.style.padding = "5px";
            transacTable.style.border = "2px solid #0f45a9";

            const endElement = document.getElementById("adminButtons");
            endElement.appendChild(transacTable);
            data.forEach(transaction => {
                const row = document.createElement("tr");
                const transacInfo = [transaction.amount, transaction.accountID, transaction.description, transaction.time];
                transacInfo.forEach(info => {
                    const infoCell = document.createElement("td");
                    infoCell.innerText = info;
                    infoCell.style.border = "2px solid #0f45a9";
                    infoCell.padding = "2px";
                    infoCell.textAlign = "center";
                    row.appendChild(infoCell);
                })
                transacTable.appendChild(row);
            });

        })
        .catch(error => console.error("Error: ", error));
}

