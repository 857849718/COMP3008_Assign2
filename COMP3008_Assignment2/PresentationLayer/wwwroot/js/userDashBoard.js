const storedEmail = localStorage.getItem("email");

console.log("ENTRY");

let firstName = document.getElementById("fName");
let lastName = document.getElementById("lName");
let email = document.getElementById("email");
let balance = document.getElementById("balance");

email.innerHTML = String(storedEmail);