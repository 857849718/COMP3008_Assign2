using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Intermed;

namespace BusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccController : ControllerBase
    {
        private readonly RestClient RestClient = new RestClient("http://localhost:5185");

        // Create new bank account
        [HttpPost]
        public IActionResult CreateAcc([FromBody] AccountIntermed acc)
        {
            var request = new RestRequest("/api/Acc", Method.Post);
            request.AddJsonBody(acc);

            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Account successfully created");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

        // Retrieve account details by account number
        [HttpGet]
        [Route("get/{accNo}")]
        public IActionResult GetAccByAccNo(int accNo)
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

        // Update account details
        [HttpPatch]
        public IActionResult UpdateAcc([FromBody] AccountIntermed acc)
        {
            var request = new RestRequest("/api/Acc", Method.Patch);
            request.AddJsonBody(acc);

            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Account detail update successful");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

        // Delete account
        [HttpDelete]
        [Route("{accNo}")]
        public IActionResult DeleteAcc(int accNo)
        {
            var request = new RestRequest($"/api/Acc?accNo={accNo}", Method.Delete);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Account deleted");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
