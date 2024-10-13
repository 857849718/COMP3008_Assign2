using DataTier.Database;
using DataTier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccController : ControllerBase
    {
        //private readonly DatabaseManager DBManger;

        //create new bank acc
        [HttpPost]
        public IActionResult CreateAcc([FromBody] Account acc)
        {
            if (AccountsOps.Insert(acc))
            {
                return Ok("Account successfully created");
            }
            return BadRequest("Account creation failed");
        }

        // test
        //retrieve acc details by acc no.
        [Route("get/{accNo}")]
        [HttpGet] //TODO: probably need to be post
        public IActionResult GetAccByAccNo(int accNo)
        {
            Console.WriteLine("accNo: " + accNo);
            Account result = AccountsOps.GetAccountByID(accNo);
            if(result != null)
            {
                Console.WriteLine(result.ToString());
                return Ok(result);
            }
            return NotFound("Cannot find account");
        }
        //update acc details
        [HttpPatch]
        public IActionResult UpdateAcc([FromBody] Account acc)
        {
            if (AccountsOps.Update(acc))
            {
                return Ok("Account detail update successful");
            }
            return BadRequest("Account detail update failed");
        }
        //delete acc
        [HttpDelete]
        public IActionResult DeleteAcc(int accNo)
        {
            if (AccountsOps.Delete(accNo))
            {
                return Ok("Account deleted");
            }
            return BadRequest("Account failed to delete");
        }

    }
}
