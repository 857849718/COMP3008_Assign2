using Intermed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

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
                var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
                return profile;
            }
            return null;
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
                var profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                return Ok(profiles);
                //List<UserProfileIntermed> profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                //return profiles;
            }
            return null;
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
                var transactions = JsonConvert.DeserializeObject<List<TransactionIntermed>>(response.Content);
                return Ok(transactions);
                //List<UserProfileIntermed> profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                //return profiles;
            }
            return null;
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
                var profiles = JsonConvert.DeserializeObject<List<UserProfileIntermed>>(response.Content);
                return Ok(profiles);
            }
            return null;
        }

    }
}
