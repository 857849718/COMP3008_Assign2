﻿using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;
using Intermed;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
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
                    ViewBag.balance = account.Balance;

                    return PartialView("UserDashBoard");
                }
            }
            Console.WriteLine("Showing login form");
            return PartialView("LoginForm");
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
            var client = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/get/{email}");
            RestResponse response = client.Get(request);
            UserProfileIntermed newProfile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);

            // Console.WriteLine(email);
            var auth = new { auth = false, msg = "Account does not exist." };

            if (newProfile.Password == null)
            {
                auth = new { auth = false, msg = "Error: Invalid credentials!" };
                return Json(auth);
            }

            if (password.Trim() != newProfile.Password)
            {
                auth = new { auth = false, msg = "Error: Invalid credentials!" };
                return Json(auth);
            }

            // check if email is logged in
            if (sessions.ContainsValue(email))
            {
                auth = new { auth = false, msg = "This account is currently in use." };
                return Json(auth);
            }

            // generate response
            
            if (response.IsSuccessful)
            {
                // create new session
                string sessionID;
                do sessionID = random.Next(1, 9999).ToString();
                while (sessions.ContainsKey(sessionID));
                sessions.Add(sessionID, email);

                //var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
                Response.Cookies.Append("SessionID", sessionID.ToString());
                auth = new { auth = true, msg = "Log in successful." };
            }

            return Json(auth);
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
        public UserProfileIntermed GetProfileByEmail(string email)
        {
            var request = new RestRequest($"/api/user/get/{email}", Method.Get);
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
