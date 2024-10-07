using DataTier.Database;
using DataTier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DataTier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacController : ControllerBase
    {
        //deposite
        [HttpPatch]
        public IActionResult Deposite(int accNo, double amount)
        {
            // retrieving account data
            Account accountOld = AccountsOps.GetAccountByID(accNo); // backup point

            Account accountNew = accountOld; // used to performing change
            double startamount = accountOld.balance;
            double endamount;
            Transaction transaction = new Transaction(amount, accNo);

            // account info availability and input amount check
            if (accountOld == null || amount <= 0)
            {
                return BadRequest("Account not found or invalid input");
            }

            // adding amount into account balance and updating entry in DB
            accountNew.balance += amount;
            AccountsOps.Update(accountNew);

            // retrieving updated account balance
            endamount = AccountsOps.GetAccountByID(accNo).balance;

            // verifying
            if (!TransVeri(startamount, amount, endamount))
            {
                // if failed, revert change and return badrequest
                AccountsOps.Update(accountOld);
                return BadRequest("Transaction failed");
            }

            // saving transaction record
            TransactionsOps.Insert(transaction);

            return Ok("Transaction successful");
        }
        // withdrawal
        [HttpPatch]
        public IActionResult Withdraw(int accNo, double amount)
        {
            // retrieving account data
            Account accountOld = AccountsOps.GetAccountByID(accNo); // backup point

            Account accountNew = accountOld; // used to performing change
            double startamount = accountOld.balance;
            double endamount;
            Transaction transaction = new Transaction((amount * -1), accNo);

            // account info availability and input amount check
            if (accountOld == null || amount <= 0)
            {
                return BadRequest("Account not found or invalid input");
            }

            // adding amount into account balance and updating entry in DB
            accountNew.balance -= amount;
            AccountsOps.Update(accountNew);

            // retrieving updated account balance
            endamount = AccountsOps.GetAccountByID(accNo).balance;

            // verifying
            if (!TransVeri(startamount, amount, endamount))
            {
                // if failed, revert change and return badrequest
                AccountsOps.Update(accountOld);
                return BadRequest("Transaction failed");
            }
            // saving transaction record
            TransactionsOps.Insert(transaction);

            return Ok("Transaction successful");
        }

        // verifying
        private bool TransVeri(double startamount, double amount, double endamount)
        {
            return Math.Abs(startamount - endamount) == amount;
        }
        /*logging?
        private void Log(string log)
        {

        }
        */
    }
}
