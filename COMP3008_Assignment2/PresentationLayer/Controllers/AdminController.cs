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
            ViewBag.adminName = adminProfile.FirstName;
            ViewBag.adminEmail = adminProfile.Email;
            ViewBag.adminPhone = adminProfile.Phone; 
            return PartialView("AdminDashboard");
        }

        public UserProfileIntermed GetProfileByEmail(string email)
        {
            RestClient restClient = new RestClient("http://localhost:5186");
            var request = new RestRequest($"/api/user/get/{email}", Method.Get);
            RestResponse response = restClient.Execute(request);

            if (response.IsSuccessful)
            {
                var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
                return profile;
            }
            return null;
        }
    }
}
