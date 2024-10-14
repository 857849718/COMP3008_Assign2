console.log("admin dashboard loaded");

/*
    1.) Create table using document.createElement (append to last element)

*/
let currentDiv = null;

function clearDiv() {
    if (currentDiv) {
        currentDiv.remove();
    }
}
function loadAll() {
    clearDiv();
    const userDiv = document.createElement("div");
    const filterBox = document.createElement("select");
    filterBox.innerText = "Filter Users By:";
    currentDiv = userDiv;

    const endElement = document.getElementById("adminButtons");
    const filterText = document.createElement("input");
    filterText.type = "text";
    filterText.placeholder = "Value";
    filterText.id = "filterText";
    endElement.appendChild(userDiv);
    userDiv.appendChild(filterBox);
    const filterChoices = ["Last Name", "Account ID", "Email"];
    filterChoices.forEach(choice => {
        const option = document.createElement("option");
        option.value = choice;
        option.innerText = choice;
        filterBox.appendChild(option);
    });
    const filterButton = document.createElement("button");
    filterButton.innerText = "Filter";
    filterBox.id = "filterBox";
    
    filterButton.onclick = () => {
        clearDiv();
        const selectedFilter = filterBox.value;
        const filterValue = filterText.value;
        const filteredUserDiv = document.createElement("div");
        currentDiv = filteredUserDiv;
        endElement.appendChild(filteredUserDiv);
        loadUsers(filteredUserDiv, selectedFilter, filterValue);
    }
    userDiv.appendChild(filterText);
    userDiv.appendChild(filterButton);

    loadUsers(userDiv);
}

function loadUsers(userDiv, selectedFilter = "", filterValue = "") {
    console.log("entry selected filter: " + selectedFilter);
    let request = '/api/admin/getusers'; // default -> will retrieve all users
    if (filterValue.trim() != "") {
        console.log("filter value not empty");
        if (selectedFilter == "Email") {
            console.log("Email selected");
            filterValue = filterValue.trim();
            console.log("Email chosen: " + filterValue);
            request = '/api/admin/' + filterValue;
            console.log(request);
        }
        else if (selectedFilter == "Account ID") {
            console.log("Account ID selected");
            filterValue = filterValue.trim();
            console.log("Account ID chosen: " + filterValue);
            request = '/api/admin/getusersbyid/' + filterValue;
            console.log(request);
        }
        else if (selectedFilter == "Last Name") {
            console.log("Last Name selected");
            filterValue = filterValue.trim();
            console.log("Last Name chosen: " + filterValue);
            request = '/api/admin/getusersbylastname/' + filterValue;
            console.log(request);
        }
    }
    fetch(request)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
                alert("No records found for the selected filter!")
            }
            return response.json();
        })
        .then(data => {
            const users = Array.isArray(data) ? data : [data]; // converts to array if not
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
            userTable.appendChild(addressCol);
            userTable.appendChild(phoneCol);
            userTable.appendChild(passwordCol);
            userTable.appendChild(emailCol);
            userTable.appendChild(accountIDCol);

            userTable.style.backgroundColor = "#69ffa6";
            userTable.style.margin = "10px auto";
            userTable.style.padding = "5px";
            userTable.style.border = "2px solid #0f45a9";

            userDiv.appendChild(userTable);
            users.forEach(user => {
                const row = document.createElement("tr");
                const userInfo = [user.firstName, user.lastName, user.address, user.phone, user.password, user.email, user.accountID];
                userInfo.forEach(info => {
                    const infoCell = document.createElement("td");
                    infoCell.innerText = info;
                    infoCell.style.border = "2px solid #0f45a9";
                    infoCell.padding = "2px";
                    infoCell.textAlign = "center";
                    row.appendChild(infoCell);
                })
                const selectUser = document.createElement("button");
                selectUser.innerText = "Modify";
                selectUser.onclick = () => modify(userInfo);
                row.appendChild(selectUser);
                userTable.appendChild(row);
            });

        })
        .catch(error => console.error("Error: ", error));
}

function modify(userInfo) {
    clearDiv();
    const endElement = document.getElementById("adminButtons");
    const modifyDiv = document.createElement("div");
    currentDiv = modifyDiv;
    endElement.appendChild(modifyDiv);
    const modifyTable = document.createElement("table");
    modifyTable.style.backgroundColor = "#69ffa6";
    modifyTable.style.margin = "10px auto";
    modifyTable.style.padding = "5px";
    modifyTable.style.border = "2px solid #0f45a9";
    modifyDiv.appendChild(modifyTable);
    const headers = ["First Name", "Last Name", "Address", "Phone", "Password", "Email", "Account ID"];
    const headerRow = document.createElement("tr");
    headers.forEach(header => {
        const th = document.createElement("th");
        th.innerText = header;
        th.style.textAlign = "center";
        headerRow.appendChild(th);
    });
    modifyTable.appendChild(headerRow);
    const userRow = document.createElement("tr");
    userRow.id = "userRow";
    const newUserInfo = [userInfo[0], userInfo[1], userInfo[2], userInfo[3], userInfo[4], userInfo[5]];
    let accountID = userInfo[6];
    console.log(accountID);
    userInfo.pop(); // remove account ID as input (can't be changed)
    let email = userInfo[5];
    userInfo.pop(); // remove email for same reason
    userInfo.forEach(data => {
        const modifyData = document.createElement("input")
        modifyData.type = "text";
        modifyData.value = data;
        modifyData.style.border = "2px solid #0f45a9";
        modifyData.padding = "2px";
        modifyData.textAlign = "center";
        const td = document.createElement("td");
        td.appendChild(modifyData);
        userRow.append(td);
    });

    const persistent = [email, accountID];
    persistent.forEach(member => {
        const temp = document.createElement("td");
        temp.style.border = "2px solid #0f45a9";
        temp.padding = "2px";
        temp.textAlign = "center";
        temp.innerText = member;
        temp.id = member;
        userRow.append(temp);
    });
    
    modifyTable.appendChild(userRow);

    const buttons = ["Update", "Deactivate"];
    buttons.forEach(button => {
        const b = document.createElement("button");
        b.innerText = button;
        modifyDiv.appendChild(b);
        b.onclick = () => modifyUser(b.innerText, email);
    });
}

function modifyUser(action, email) {
    const row = document.getElementById("userRow");
    const rowCells = row.querySelectorAll("td");
    const rowArray = Array.from(rowCells).map(cell => {
        const input = cell.querySelector('input');
        return input ? input.value : cell.textContent; 
    });
    rowArray.forEach(info => {
        console.log(info);
    });
    if (action == "Deactivate") {
        console.log("Deactivate chosen");
        let request = '/api/admin/delete/' + email;
        console.log("request: " + request);
        fetch(request, {
            method: 'DELETE',
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response not ok');
                    console.log(response.json);
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
            })
            .catch(error => {
                console.error(error);
            });
        clearDiv();
    }
    else if (action == "Update") {
        console.log("Update chosen")
        const userProfile = {
            firstName: rowArray[0],
            lastName: rowArray[1],
            address: rowArray[2],
            phone: rowArray[3],
            password: rowArray[4],
            email: email
        };

        fetch('/api/admin/update', {
            method: 'PATCH', 
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userProfile) 
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
            })
            .catch(error => {
                console.error(error);
            });
        clearDiv();
    }
}


function loadTransactions() {
    clearDiv();
    fetch('/api/admin/gettransactions')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            const transacDiv = document.createElement("div");
            const transacTable = document.createElement("table");
            currentDiv = transacDiv;

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
            endElement.appendChild(transacDiv);
            transacDiv.appendChild(transacTable);
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

