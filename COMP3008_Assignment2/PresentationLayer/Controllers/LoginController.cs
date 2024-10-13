using Microsoft.AspNetCore.Mvc;
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
        private ProfileSingleton profileS;

        // show login form if no sessionID, user dashboard if logged in
        [HttpGet("ShowLoginForm")]
        public IActionResult ShowLoginForm()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookie = Request.Cookies["SessionID"];
                Console.WriteLine(cookie);
                if (sessions.ContainsKey(cookie))
                {
                    Console.WriteLine("Showing user dashboard");
                    return PartialView("UserDashBoard");
                }
            }

            return PartialView("LoginForm");
        }

        // login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Profile profile)
        {
            string email = profile.Email;
            string password = profile.Password;

            // get user account info
            var client = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/get/{email}");
            RestResponse response = client.Get(request);
            UserProfileIntermed newProfile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);

            Console.WriteLine(email);
            var auth = new { auth = false, msg = "Account does not exist." };

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
                profileS = ProfileSingleton.GetInstance();
                profileS.Email = email;
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

        [HttpPost("GetAcc")]
        public IActionResult GetAccByAccNo([FromBody] int accNo)
        {
            var request = new RestRequest($"/api/Acc/get/{accNo}", Method.Get);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                var account = JsonConvert.DeserializeObject<AccountIntermed>(response.Content);
                return Ok(account);
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

    }
}
