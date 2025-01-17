﻿using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;
using Intermed;
using PresentationLayer.Models;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly RestClient RestClient = new RestClient("http://localhost:5186");
        private Random random = new Random();
        private static readonly Dictionary<string, string> sessions = new Dictionary<string, string>();


        // show login form if no sessionID, user dashboard if logged in
        [HttpGet("ShowLoginForm")]
        public IActionResult ShowLoginForm()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookie = Request.Cookies["SessionID"];
                UserProfileIntermed userProfile;
                AccountIntermed account;
                Console.WriteLine(cookie);
                if (sessions.ContainsKey(cookie))
                {
                    // get account info
                    userProfile = GetProfileByEmail(sessions[cookie]);
                    Console.WriteLine(userProfile.Email);
                    account = GetAccByAccNo(userProfile.AccountID);

                    Console.WriteLine("Showing user dashboard");
                    ViewBag.fName = account.FirstName;
                    ViewBag.lName = account.LastName;
                    ViewBag.email = userProfile.Email;
                    ViewBag.balance = account.Balance.ToString();
                    ViewBag.phone = userProfile.Phone.ToString();

                    return PartialView("UserDashBoard");
                }
            }
            Console.WriteLine("Showing login form");
            return PartialView("LoginForm");
        }

        // show update user info form
        [HttpGet("ShowUserInfoUpdateForm")]
        public IActionResult ShowUserInfoUpdateForm()
        {
            // get account info
            var cookie = Request.Cookies["SessionID"];
            UserProfileIntermed userProfile;
            userProfile = GetProfileByEmail(sessions[cookie]);
            Console.WriteLine("Showing user info update form");

            // viewbag parameter assign
            ViewBag.password = userProfile.Password;
            ViewBag.phone = userProfile.Phone.ToString();
            ViewBag.address = userProfile.Address;

            return PartialView("UpdateInfoForm");
        }

        [HttpGet("ShowTransacHistory")]
        public IActionResult ShowTransacHistory()
        {
            // get account info
            var cookie = Request.Cookies["SessionID"];
            UserProfileIntermed userProfile;
            userProfile = GetProfileByEmail(sessions[cookie]);

            Console.WriteLine("Showing transaction history");

            var request = new RestRequest($"/api/transac/{userProfile.AccountID}", Method.Get);
            RestResponse response = RestClient.Execute(request);

            List<TransactionIntermed> transacList = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);

            return PartialView("TransacHistory", transacList);
        }


        [HttpPost("ShowTransacHistoryWithFilter/{startDate}/{endDate}")]
        public IActionResult ShowTransacHistoryWithFilter(string startDate, string endDate)
        {
            // get account info
            var cookie = Request.Cookies["SessionID"];
            UserProfileIntermed userProfile;
            userProfile = GetProfileByEmail(sessions[cookie]);

            // parse dates
            DateTime.TryParse(startDate, out DateTime parsedStartDate);
            DateTime.TryParse(endDate, out DateTime parsedEndDate);

            Console.WriteLine("Showing transaction history");

            var request = new RestRequest($"/api/transac/{userProfile.AccountID}", Method.Get);
            RestResponse response = RestClient.Execute(request);

            // converting and storing deserialized list
            List<TransactionIntermed> transacListTemp = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);

            List<TransactionIntermed> transacList = new List<TransactionIntermed>();

            Console.WriteLine("start date: " + parsedStartDate + " end date: " + parsedEndDate);
            // covert string type date to DateTime type
            foreach (var transac in transacListTemp)
            {
                if (DateTime.TryParse(transac.Time, out DateTime parsedTime))
                {
                    Console.WriteLine("parsed date: " + parsedTime);
                    if (parsedTime >= parsedStartDate && parsedTime <= parsedEndDate)
                    {
                        transacList.Add(transac);
                    }
                }
                else
                {
                    Console.WriteLine("Failed to parse time");
                }
            }
            return PartialView("TransacHistory", transacList);
        }

        // login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Profile profile)
        {
            string email = profile.Email;
            string password = profile.Password;

            Console.WriteLine("email: " + email);
            Console.WriteLine("password: "+password);

            // get user account info
            UserProfileIntermed newProfile = GetProfileByEmail(email);

            // Console.WriteLine(email);
            var auth = new { auth = false, msg = "Account does not exist.", adminFlag = false };

            if (newProfile == null)
            {
                Console.WriteLine("retrieved account is null");
                auth = new { auth = false, msg = "Error: Invalid credentials!", adminFlag = false };
                return Json(auth);
            }

            if (password.Trim() != newProfile.Password)
            {
                auth = new { auth = false, msg = "Error: Invalid credentials!", adminFlag = false };
                return Json(auth);
            }

            // check if email is logged in
            if (sessions.ContainsValue(email))
            {
                auth = new { auth = false, msg = "This account is currently in use.", adminFlag = false };
                return Json(auth);
            }

            // generate response
            
            if (newProfile !=null)
            {
                Console.WriteLine(newProfile.ToString());
                Console.WriteLine("retrieved email: " + newProfile.Email);
                Console.WriteLine("retrieved password: " + newProfile.Password);

                // create new session
                string sessionID;
                do sessionID = random.Next(1, 9999).ToString();
                while (sessions.ContainsKey(sessionID));
                sessions.Add(sessionID, email);

                //var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
                Response.Cookies.Append("SessionID", sessionID.ToString());

                if (email.Equals("admin@bankingsolutions.com") && password.Trim().Equals(newProfile.Password))
                {
                    Console.WriteLine("admin");
                    auth = new { auth = true, msg = "Log in successful.", adminFlag = true };
                    return Json(auth);
                }
                else
                {
                    auth = new { auth = true, msg = "Log in successful.", adminFlag = false };
                }
            }

            return Json(auth);
        }

        [HttpPost("logout")]
        public IActionResult Transfer()
        {
            var sessionID = Request.Cookies["SessionID"];
            var Response = new { success = false, msg = "Failed to logout."};
            if (sessionID != null)
            {
                sessions.Remove(sessionID);
                Response = new { success = true, msg = "Successfully logged out." };
            }
            return Json(Response);
        }

        // transfer
        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransactionIntermed transaction)
        {
            // retrieve account info
            var cookie = Request.Cookies["SessionID"];
            UserProfileIntermed userProfile;
            AccountIntermed account;
            userProfile = GetProfileByEmail(sessions[cookie]);
            Console.WriteLine("Transfer attempt by: " + userProfile.Email);
            account = GetAccByAccNo(userProfile.AccountID);
            Console.WriteLine("Acc id: " + account.AccountID + "\n");

            // transaction details
            double amount = transaction.Amount;
            int srcAcc = account.AccountID;
            int dstAcc = transaction.AccountID;
            string description = transaction.Description;

            // withdraw amount
            var withdrawRequest = new RestRequest($"/api/transac/withdraw/{srcAcc}/{amount}/{description}", Method.Patch);
            RestResponse withdrawResponse = RestClient.Execute(withdrawRequest);

            // deposite amount
            var depositeRequest = new RestRequest($"/api/transac/deposit/{dstAcc}/{amount}/{description}", Method.Patch);
            RestResponse depositeResponse = RestClient.Execute(depositeRequest);

            // response handling
            var responseMsg = withdrawResponse.Content + "\n" + depositeResponse.Content;
            var response = new { success = false, msg = responseMsg };
            
            if (withdrawResponse.IsSuccessful && depositeResponse.IsSuccessful)
            {
                response = new { success = true, msg = "Transfer successful." };
                return Json(response);
            }
            Console.WriteLine("=======Error transfering=====\n" + responseMsg + "\n===============");
            return Json(response);
        }

        // update user info
        [HttpPost("updateUserInfo")]
        public IActionResult updateUserInfo([FromBody] UserProfileIntermed profile)
        {
            // retrieving account info
            UserProfileIntermed userProfile;
            AccountIntermed account;
            var cookie = Request.Cookies["SessionID"];
            userProfile = GetProfileByEmail(sessions[cookie]);
            account = GetAccByAccNo(userProfile.AccountID);

            // submitting request
            var request = new RestRequest("/api/user/", Method.Patch);

            var newProfile = new
            {
                Password = profile.Password,
                Phone = profile.Phone,
                Address = profile.Address,
                Email = sessions[cookie],
                FirstName = account.FirstName,
                LastName = account.LastName
            };
            request.AddJsonBody(newProfile);
            var response = RestClient.Execute(request);

            // response handling
            var update = new { success = false, msg = "Update failed." };
            if (response.IsSuccessful)
            {
                update = new { success = true, msg = "Update successful." };
                return Json(update);
            }

            return Json(update);        
        }


        

        // private method: get account info
        private AccountIntermed GetAccByAccNo(int accNo)
        {
            var request = new RestRequest($"/api/Acc/get/{accNo}", Method.Get);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                var account = JsonConvert.DeserializeObject<AccountIntermed>(response.Content);
                return account;
            }

            return null;
        }

        // private method: get user profile
        private UserProfileIntermed GetProfileByEmail(string email)
        {
            var request = new RestRequest($"/api/user/{email}", Method.Get);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
               var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
               return profile;
            }
            return null;
        }

    }
}
