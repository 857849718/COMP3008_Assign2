﻿using Intermed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Models;
using RestSharp;
using System.Security.Principal;
using BusinessTier.Models;
using DataTier.Models;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {

        [HttpGet("ShowAdminDashboard")]
        public IActionResult ShowAdminDashboard()
        {
            Console.WriteLine("Showing admin dashboard");
            UserProfileIntermed adminProfile = GetProfileByEmail("admin@bankingsolutions.com");
            if (adminProfile != null)
            {
                ViewBag.adminName = adminProfile.FirstName;
                ViewBag.adminEmail = adminProfile.Email;
                ViewBag.adminPhone = adminProfile.Phone;
                return PartialView("AdminDashboard");
            }
            return NotFound();
        }

        public UserProfileIntermed GetProfileByEmail(string email)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/{email}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Profile By Email {email}", true);
                var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
                return profile;
            }
            else
            {
                AdminLogger.Log($"Retrieve Profile By Email {email}", false);
                return null;
            }
        }

        // method to retrieve list of users
        [HttpGet]
        [Route("getusers")]
        public IActionResult GetUsers()
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest("/api/user/getprofiles", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log("Retrieve All Users", true);
                var profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                return Ok(profiles);
                //List<UserProfileIntermed> profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                //return profiles;
            }
            else
            {
                AdminLogger.Log("Retrieve All Users", false);
                return null;
            }
        }

        // method to retrieve list of transactions
        [HttpGet]
        [Route("gettransactions")]
        public IActionResult GetTransactions()
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest("/api/transac/gettransactions", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log("Retrieve All Transactions", true);
                var transactions = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);
                return Ok(transactions);
                //List<UserProfileIntermed> profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                //return profiles;
            }
            else
            {
                AdminLogger.Log("Retrieve All Transactions", false);
                return null;
            }
        }

        // method to retrieve list of users based on email
        [HttpGet]
        [Route("{email}")]
        public IActionResult GetUsersByEmail(string email)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/{email}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Users By Email {email}", true);
                var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
                return Ok(profile);
            }
            else
            {
                AdminLogger.Log($"Retrieve Users By Email {email}", false);
                return null;
            }
        }

        // method to retrieve list of users based on id
        [HttpGet]
        [Route("getusersbyid/{id}")]
        public IActionResult GetUsersById(int id)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/getprofilesbyid/{id}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Users By ID {id}", true);
                var profile = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                return Ok(profile);
            }
            else
            {
                AdminLogger.Log($"Retrieve Users By ID {id}", false);
                return null;
            }
        }

        // method to retrieve list of users based on last name
        [HttpGet]
        [Route("getusersbylastname/{lastName}")]
        public IActionResult GetUsersByLastName(string lastName)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/getprofilesbylastname/{lastName}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Users By Last Name {lastName}", true);
                var profile = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                return Ok(profile);
            }
            else
            {
                AdminLogger.Log($"Retrieve Users By Last Name {lastName}", false);
                return null;
            }
        }

        // method to delete a user's profile
        [HttpDelete]
        [Route("delete/{email}")]
        public IActionResult DeleteUser(string email)
        {
            Console.WriteLine($"email to delete: {email}");
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/{email}", Method.Delete);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Delete User By Email {email}", true);
                return Ok(response);
            }
            else
            {
                AdminLogger.Log($"Delete User By Email {email}", false);
                return null;
            }
        }

        // method to update a user's profile
        [HttpPatch]
        [Route("update")]
        public IActionResult UpdateUser([FromBody] UserProfileIntermed profile)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/", Method.Patch);
            request.AddJsonBody(profile);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Update User {profile.Email}", true);
                return Ok(response);
            }
            else
            {
                AdminLogger.Log($"Update User {profile.Email}", false);
                return null;
            }
        }

        // method to create a new account
        [HttpPost]
        [Route("create")]
        public IActionResult CreateUser([FromBody] CreateUserIntermed createUser)
        {
            AccountIntermed newAccount = new AccountIntermed();
            newAccount.FirstName = createUser.FirstName;
            newAccount.LastName = createUser.LastName;
            newAccount.Balance = createUser.Balance;

            // needs to first create the account, and then the user
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/acc/", Method.Post);
            request.AddJsonBody(newAccount);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log("Create User", true);
                CreateProfile(createUser);
                return Ok(response);
            }
            else
            {
                AdminLogger.Log("Create User", false);
                return null;
            }
        }

        private IActionResult CreateProfile(CreateUserIntermed createUser)
        {
            UserProfileIntermed newUser = new UserProfileIntermed();

            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/acc/getlatestid", Method.Get);
            //request.AddJsonBody(newUser);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                var latestID = JsonConvert.DeserializeObject<int>(response.Content);
                newUser.FirstName = createUser.FirstName;
                newUser.LastName = createUser.LastName;
                newUser.Email = createUser.Email;
                newUser.Address = createUser.Address;
                newUser.Phone = createUser.Phone; 
                newUser.Password = createUser.Password; 
                newUser.AccountID = latestID;
                InsertUser(newUser);

                return Ok(response);
            }
            return null;
        }

        private IActionResult InsertUser(UserProfileIntermed newUser)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user", Method.Post);
            request.AddJsonBody(newUser);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return null;
        }

        // method to retrieve list of transactions based on min withdraw
        [HttpGet]
        [Route("minwithdraw/{amount}")]
        public IActionResult GetTransactionsByMinWithdraw(double amount)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/transac/minwithdraw/{amount}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Transaction By Minimum Withdraw {amount}", true);
                var transactions = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);
                return Ok(transactions);
            }
            else
            {
                AdminLogger.Log($"Retrieve Transaction By Minimum Withdraw {amount}", false);
                return null;
            }
        }

        // method to retrieve list of transactions based on min deposit
        [HttpGet]
        [Route("mindeposit/{amount}")]
        public IActionResult GetTransactionsByMinDeposit(double amount)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/transac/mindeposit/{amount}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Transaction By Minimum Deposit {amount}", true);
                var transactions = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);
                return Ok(transactions);
            }
            else
            {
                AdminLogger.Log($"Retrieve Transaction By Minimum Deposit {amount}", false);
                return null;
            }
        }

        // method to retrieve list of transactions based on account ID
        [HttpGet]
        [Route("transacid/{id}")]
        public IActionResult GetTransactionsByID(int id)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/transac/{id}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                AdminLogger.Log($"Retrieve Transaction By Account ID {id}", true);
                var transactions = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);
                return Ok(transactions);
            }
            else
            {
                AdminLogger.Log($"Retrieve Transaction By Account ID {id}", false);
                return null;
            }
        }

    }
}
