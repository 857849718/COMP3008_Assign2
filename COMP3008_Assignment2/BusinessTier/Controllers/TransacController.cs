using DataTier.Database;
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
        //deposit
        [HttpPatch]
        [Route("deposit/{accNo}/{amount}")]
        public IActionResult Deposit(int accNo, double amount, string description)
        {
            // retrieving account data
            Account accountOld = AccountsOps.GetAccountByID(accNo); // backup point

            Account accountNew = accountOld; // used to performing change
            double startamount = accountOld.Balance;
            double endamount;
            Transaction transaction = new Transaction(amount, accNo, description, DateTime.Now.ToString());

            // account info availability and input amount check
            if (accountOld == null || amount <= 0)
            {
                return BadRequest("Transaction failed, account not found or invalid input");
            }

            // adding amount into account balance and updating entry in DB
            accountNew.Balance += amount;
            AccountsOps.Update(accountNew);

            // retrieving updated account balance
            endamount = AccountsOps.GetAccountByID(accNo).Balance;

            // verifying
            if (!TransVeri(startamount, amount, endamount))
            {
                // if failed, revert change and return badrequest
                AccountsOps.Update(accountOld);
                return BadRequest("Transaction failed, database failed to be updated");
            }

            // saving transaction record
            TransactionsOps.Insert(transaction);

            return Ok("Transaction successful");
        }
        // withdrawal
        [HttpPatch]
        [Route("withdraw/{accNo}/{amount}")]
        public IActionResult Withdraw(int accNo, double amount, string description)
        {
            // retrieving account data
            Account accountOld = AccountsOps.GetAccountByID(accNo); // backup point

            Account accountNew = accountOld; // used to performing change
            double startamount = accountOld.Balance;
            double endamount;
            Transaction transaction = new Transaction((amount * -1), accNo, description, DateTime.Now.ToString());

            // account info availability and input amount check
            if (accountOld == null || amount <= 0)
            {
                return BadRequest("Transaction failed, account not found or invalid input");
            }

            // adding amount into account balance and updating entry in DB
            accountNew.Balance -= amount;
            AccountsOps.Update(accountNew);

            // retrieving updated account balance
            endamount = AccountsOps.GetAccountByID(accNo).Balance;

            // verifying
            if (!TransVeri(startamount, amount, endamount))
            {
                // if failed, revert change and return badrequest
                AccountsOps.Update(accountOld);
                return BadRequest("Transaction failed, database failed to be updated");
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

        // get transaction list by accoundID
        [HttpGet("{accNo}")]
        public IEnumerable<Transaction> GetTransactionList(int accNo)
        {
            if (TransactionsOps.GetTransactionsByID(accNo) != null)
            {
                return TransactionsOps.GetTransactionsByID(accNo);
            }
            return null;
        }

        // get all transactions
        [HttpGet]
        [Route("gettransactions")]
        public IActionResult GetTransactions()
        {
            try
            {
                List<Transaction> transactions = TransactionsOps.GetAll();
                if (transactions != null)
                {
                    return Ok(transactions);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
