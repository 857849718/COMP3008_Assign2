using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Intermed;
using DataTier;
using DataTier.Models;
using DataTier.Database;

namespace BusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RestClient RestClient = new RestClient("http://localhost:5185"); // Base URL of the DataTier service

        // Create new profile
        [HttpPost]
        public IActionResult CreateNewProfile([FromBody] UserProfileIntermed profile)
        {
            var request = new RestRequest("/api/User", Method.Post);
            request.AddJsonBody(profile);

            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("User profile successfully created");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

        // Retrieve user profile by email
        [HttpGet]
        public IActionResult GetProfileByEmail(string email)
        {
            //var request = new RestRequest($"/api/User/{email}", Method.Get);
            //RestResponse response = RestClient.Execute(request);

            UserProfile profile = ProfilesOps.GetProfileByEmail(email);

            return new ObjectResult(profile)
            {
                StatusCode = 200,
                ContentTypes = { "application/json" }
            };

            //if (response.IsSuccessful)
            //{
            //    var profile = JsonConvert.DeserializeObject<UserProfileIntermed>(response.Content);
            //    return Ok(profile);
            //}
            //return StatusCode((int)response.StatusCode, response.Content);
        }

        // Update profile
        [HttpPatch]
        public IActionResult UpdateProfile([FromBody] UserProfileIntermed profile)
        {
            var request = new RestRequest("/api/User", Method.Patch);
            request.AddJsonBody(profile);

            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("User profile successfully updated");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

        // Delete profile
        [HttpDelete]
        [Route("{email}")]
        public IActionResult DeleteProfile(string email)
        {
            var request = new RestRequest($"/api/User/{email}", Method.Delete);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("User profile successfully deleted");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
