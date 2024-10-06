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
        private readonly DatabaseManager DBManger;

        //create new bank acc
        [HttpPost]
        public IActionResult CreateAcc([FromBody]Account acc)
        {
            //insert DB magic here
        }

        //retrieve acc details by acc no.
        [HttpGet] //TODO: probably need to be post
        public IActionResult GetAccByAccNo(int accNo)
        {
            int accountID = 0;
            double balance = 0;
            string firstName = "";
            string lastName = "";
            Account result = new Account(accountID, balance, firstName, lastName);
            //TODO: insert DB magic here
            return Ok(result);
        }
        //update acc details
        [HttpPut]
        public IActionResult UpdateAcc(Account acc)
        {

        }
        //delete acc
        [HttpDelete]
        public IActionResult DeleteAcc(Account acc)
        {

        }

    }
}
