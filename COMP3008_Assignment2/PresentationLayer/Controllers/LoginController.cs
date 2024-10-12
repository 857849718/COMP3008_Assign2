using Microsoft.AspNetCore.Mvc;
//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;
using Intermed;

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
        public IActionResult Login([FromBody] string email)
        {
            email = email.ToLower();
            // get user account info
            var request = new RestRequest($"/api/User/{email}", Method.Get);
            RestResponse response = RestClient.Execute(request);

            Console.WriteLine(email);

            var auth = new { auth = false, msg = "Account does not exist." };

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
