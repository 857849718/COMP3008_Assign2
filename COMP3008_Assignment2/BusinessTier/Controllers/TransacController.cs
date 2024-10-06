using DataTier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace BusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacController : ControllerBase
    {
        //deposite
        [HttpPatch]
        public IActionResult Deposite(Account acc, float amount)
        {
            
            if (acc == null || amount <= 0)
            {
                return BadRequest("Invalid input");
                
            }
            if (!/*TODO: DBM function call here*/ && )
            {
                return BadRequest("Transaction failed");
                
            }
            return Ok("Transaction successful");
        }
        //withdrawal
        [HttpPatch]
        public IActionResult Withdraw(Account acc, float amount)
        {
            if (acc == null || amount <= 0)
            {
                return BadRequest("Invalid input");
            }
            if (/*TODO: DBM function call here*/)
            {
                return Ok("Transaction successful");
            }
            return BadRequest("Transaction failed");
        }

        //verifying
        private bool TransVeri(float startamount, float amount, float endamount)
        {
            return (Math.Abs(startamount - endamount) == amount);
        }
        //logging?
        private void Log(string log)
        {

        }
    }
}
