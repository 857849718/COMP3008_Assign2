﻿console.log("admin dashboard loaded");

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
                selectUser.onclick = () => modify(userInfo, false);
                row.appendChild(selectUser);
                userTable.appendChild(row);
            });

        })
        .catch(error => console.error("Error: ", error));
}

function createUser() {
    clearDiv();
    const endElement = document.getElementById("adminButtons");
    const createDiv = document.createElement("div");
    currentDiv = createDiv;
    endElement.appendChild(createDiv);
    const createTable = document.createElement("table");
    createTable.style.backgroundColor = "#69ffa6";
    createTable.style.margin = "10px auto";
    createTable.style.padding = "5px";
    createTable.style.border = "2px solid #0f45a9";
    createDiv.appendChild(createTable);
    const headers = ["First Name", "Last Name", "Address", "Phone", "Password", "Email", "Balance"];
    const headerRow = document.createElement("tr");
    headers.forEach(header => {
        const th = document.createElement("th");
        th.innerText = header;
        th.style.textAlign = "center";
        headerRow.appendChild(th);
    });
    createTable.appendChild(headerRow);
    const createRow = document.createElement("tr");
    createRow.id = "createRow";

    const placeholders = ["First Name", "Last Name", "Address", "Phone", "Password", "Email", "Balance"];
    placeholders.forEach(p => {
        const createData = document.createElement("input")
        createData.type = "text";
        createData.placeholder = p;
        createData.style.border = "2px solid #0f45a9";
        createData.padding = "2px";
        createData.textAlign = "center";
        const td = document.createElement("td");
        td.appendChild(createData);
        createRow.append(td);
    });
    createTable.appendChild(createRow);

    const createButton = document.createElement("button");
    createButton.innerText = "Create User";
    createButton.textAlign = "center";
    createDiv.appendChild(createButton);

    createButton.onclick = () => create();
}

function create() {
    const row = document.getElementById("createRow");
    const rowCells = row.querySelectorAll("td");
    const rowArray = Array.from(rowCells).map(cell => {
        const input = cell.querySelector('input');
        return input ? input.value : cell.textContent;
    });
    rowArray.forEach(info => {
        console.log(info);
    });

    const userProfile = {
        firstName: rowArray[0],
        lastName: rowArray[1],
        address: rowArray[2],
        phone: rowArray[3],
        password: rowArray[4],
        email: rowArray[5],
        balance: rowArray[6],
    };

    fetch('/api/admin/create', {
        method: 'POST',
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

function modify(userInfo, flag) {
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

function transactionEntry() {
    clearDiv();
    const transacDiv = document.createElement("div");
    const filterBox = document.createElement("select");
    filterBox.innerText = "Filter Transactions By:";
    currentDiv = transacDiv;

    const endElement = document.getElementById("adminButtons");
    const filterText = document.createElement("input");
    filterText.type = "text";
    filterText.placeholder = "Value";
    filterText.id = "filterText";
    endElement.appendChild(transacDiv);
    transacDiv.appendChild(filterBox);
    const filterChoices = ["Minimum Withdraw", "Minimum Deposit", "Account ID"];
    filterChoices.forEach(choice => {
        const option = document.createElement("option");
        option.value = choice;
        option.innerText = choice;
        filterBox.appendChild(option);
    });
    const filterButton = document.createElement("button");
    filterButton.innerText = "Filter";
    filterBox.id = "filterBox";

    //const sortBox = document.createElement("select");
    //sortBox.innerText = "Sort Transactions By:";
    //transacDiv.appendChild(sortBox);
    //const sortChoices = ["Date/Time ASC", "Date/Time DESC", "Amount ASC", "Amount DESC"];
    //sortChoices.forEach(choice => {
    //    const option = document.createElement("option");
    //    option.value = choice;
    //    option.innerText = choice;
    //    sortBox.appendChild(option);
    //});
    //const sortButton = document.createElement("button");
    //sortButton.innerText = "Sort";
    //sortButton.id = "sortBox";

    //sortButton.onclick = () => {
    //    clearDiv();
    //    const selectedSort = sortBox.value();
    //    const selectedFilter = filterBox.value;
    //    const filterValue = filterText.value;
    //    const filteredTransacDiv = document.createElement("div");
    //    currentDiv = filteredTransacDiv;
    //    endElement.appendChild(filteredTransacDiv);
    //    loadTransactions(filteredTransacDiv, selectedFilter, filterValue, selectedSort);
    //}

    filterButton.onclick = () => {
        clearDiv();
        const selectedFilter = filterBox.value;
        const filterValue = filterText.value;
        const filteredTransacDiv = document.createElement("div");
        currentDiv = filteredTransacDiv;
        endElement.appendChild(filteredTransacDiv);
        loadTransactions(filteredTransacDiv, selectedFilter, filterValue);
    }
    transacDiv.appendChild(filterText);
    transacDiv.appendChild(filterButton);
    transacDiv.appendChild(sortButton);

    loadTransactions(transacDiv);
}

function loadTransactions(transacDiv, selectedFilter = "", filterValue = "") {
    let request = '/api/admin/gettransactions'; // default -> will retrieve all transactions
    if (filterValue.trim() != "") {
        console.log("filter value not empty");
        if (selectedFilter == "Minimum Withdraw") {
            console.log("Minimum Withdraw selected");
            filterValue = filterValue.trim();
            console.log("Minimum Withdraw chosen: " + filterValue);
            request = '/api/admin/minwithdraw/' + filterValue;
            console.log(request);
        }
        else if (selectedFilter == "Minimum Deposit") {
            console.log("Minimum Deposit selected");
            filterValue = filterValue.trim();
            console.log("Minimum Deposit chosen: " + filterValue);
            request = '/api/admin/mindeposit/' + filterValue;
            console.log(request);
        }
        else if (selectedFilter == "Account ID") {
            console.log("Account ID selected");
            filterValue = filterValue.trim();
            console.log("Account ID chosen: " + filterValue);
            request = '/api/admin/transacid/' + filterValue;
            console.log(request);
        }
    }
    fetch(request)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response not ok');
            }
            return response.json();
        })
        .then(data => {
            const transacTable = document.createElement("table");

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

            transacDiv.appendChild(transacTable);

            //if (selectedSort == "Date/Time ASC") {
            //    data.sort(timeAsc);
            //}
            //else if (selectedSort == "Date/Time DESC") {
            //    data.sort(timeDesc);
            //}
            //else if (selectedSort == "Amount ASC") {
            //    data.sort(amountAsc);
            //}
            //else if (selectedSort == "Amount DESC") {
            //    data.sort(amountDesc);
            //}
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

function timeAsc(a, b) {
    const timeA = a.time;
    const timeB = b.time;

    let c = 0;
    if (timeA > timeB) {
        c = 1;
    } else if (timeA < timeB) {
        c = -1;
    }
    return c;
}

function timeDesc(a, b) {
    const timeA = a.time;
    const timeB = b.time;

    let c = 0;
    if (timeA > timeB) {
        c = 1;
    } else if (timeA < timeB) {
        c = -1;
    }
    return c * -1;
}

function amountAsc(a, b) {
    const amountA = a.amount;
    const amountB = b.amount;

    let c = 0;
    if (amountA > amountB) {
        c = 1;
    } else if (amountA < amountB) {
        c = -1;
    }
    return c;
}

function amountDesc(a, b) {
    const amountA = a.amount;
    const amountB = b.amount;

    let c = 0;
    if (amountA > amountB) {
        c = 1;
    } else if (amountA < amountB) {
        c = -1;
    }
    return c * -1;
}

