using DataTier.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Transactions;

namespace BusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacController : ControllerBase
    {
        private readonly RestClient RestClient = new RestClient("http://localhost:5185");

        // Deposit
        [HttpPatch]
        [Route("deposit/{accNo}/{amount}")]
        public IActionResult Deposit(int accNo, double amount)
        {
            var request = new RestRequest($"/api/Transac/deposit/{accNo}/{amount}", Method.Patch);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Transaction successful");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

        // Withdraw
        [HttpPatch]
        [Route("withdraw/{accNo}/{amount}")]
        public IActionResult Withdraw(int accNo, double amount)
        {
            var request = new RestRequest($"/api/Transac/withdraw/{accNo}/{amount}", Method.Patch);
            RestResponse response = RestClient.Execute(request);

            if (response.IsSuccessful)
            {
                return Ok("Transaction successful");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpPost]
        public IActionResult Post(DataTier.Models.Transaction transaction)
        {
            try
            {
                if (TransactionsOps.Insert(transaction))
                {
                    return Ok("Transaction successfully created!");
                }
                return BadRequest("Transaction creation error!");
            }
            catch (Exception ex)
            {
                return BadRequest("Transaction creation error!");
            }
        }
    }
}
