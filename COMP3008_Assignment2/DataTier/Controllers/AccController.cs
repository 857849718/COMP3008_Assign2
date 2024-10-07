using DataTier.Database;
using DataTier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccController : ControllerBase
    {
        //private readonly DatabaseManager DBManger;
        public AccController()
        {
            DatabaseManager.CreateTables();
        }

        //create new bank acc
        [HttpPost]
        public IActionResult CreateAcc([FromBody] Account acc)
        {
            if (AccountsOps.Insert(acc))
            {
                return Ok("Account Successfully created");
            }
            return BadRequest("Account creation failed");
        }

        //retrieve acc details by acc no.
        [HttpGet] //TODO: probably need to be post
        public IActionResult GetAccByAccNo(int accNo)
        {
            Account result = AccountsOps.GetAccountByID(accNo);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        //update acc details
        [HttpPatch]
        public IActionResult UpdateAcc(Account acc)
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
