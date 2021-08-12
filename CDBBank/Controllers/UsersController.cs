using CDBBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDBBank.Controllers
{
    public class UsersController : Controller
    {
        // GET: UsersController
        private readonly CDBBankContext _context;

        //public async Task<IActionResult> DashboardIndex()
        //{
        //    int id = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
        //    return View(await _context.Transactions.Where(u => u.Id == id).OrderByDescending(u => u.TransactionDate).Take(7).ToListAsync());
        //}

        public UsersController(CDBBankContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Dashboard()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            return View(await _context.Transactions.Where(u => u.Id == id).OrderByDescending(u => u.TransactionDate).Take(7).ToListAsync());
        }

        public ActionResult Settings()
        {
            var user = GetLoggedInUSer();
            ViewBag.settings = null;
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(string password, string newPassword, string confirmNewPassword)
        {
            var loguser = GetLoggedInUSer();
            if (loguser.Password == password && newPassword == confirmNewPassword)
            {
                loguser.Password = confirmNewPassword;
                _context.BankUsers.Update(loguser);
                await _context.SaveChangesAsync();
                ViewBag.settings = "Password Changed!";
                //return View("Settings");
                return RedirectToAction("IsCustomer", "Default");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }
        }

      
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAccount(string password)
        {
            var user = GetLoggedInUSer();
            if(user.Password == password)
            {
                _context.BankUsers.Remove(user);
                await _context.SaveChangesAsync();
                ViewBag.settings = "Deleted Successfully";
                return RedirectToAction("Index", "Home");
            }

            return View("Settings");
            
        }

        public async Task<ActionResult> TransactionIndex()
        {
            ViewBag.filter = null;
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));

            return View( await _context.Transactions.Where(u => u.BankUsers.Id == id).OrderByDescending(u => u.TransactionDate).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> FilterTransactions(string fromDate, string toDate, string tranName = null)
        {
            var translist = await _context.Transactions.ToListAsync();
            var user = GetLoggedInUSer();

            DateTime FromDate = Convert.ToDateTime(fromDate);
            DateTime ToDate = Convert.ToDateTime(toDate);

            if (tranName.Equals("select"))
            {
                translist = await _context.Transactions.Where(u => u.TransactionDate >= FromDate && u.TransactionDate <= ToDate && u.UserId == user.Id).OrderByDescending(u => u.TransactionDate).ToListAsync();
            }
            else
            {
                if (tranName.Equals("deposit"))
                {
                    translist = await _context.Transactions.Where(u => u.Deposit != 0 && u.UserId == user.Id).OrderByDescending(u => u.TransactionDate).ToListAsync();
                }
                if (tranName.Equals("withdrawal"))
                {
                    translist = await _context.Transactions.Where(u => u.Withdrawal != 0 && u.UserId == user.Id).OrderByDescending(u => u.TransactionDate).ToListAsync();
                }
            }

          
            return View("TransactionIndex", translist);
        }

        public ActionResult Accounts()
        {
            var user = GetLoggedInUSer();
            return View(user);
        }

        public ActionResult Services()
        {
            return View();
        }


        [HttpGet]
        public ActionResult BankTransferNeft()
        {
            ViewBag.message = null;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BankTransferNeft(NeftRecords neft)
        {

            var user = GetLoggedInUSer();
            var bankrecipient = await _context.BankUsers.Where(b => b.AccountNum == neft.AccountNo).SingleAsync();
            if(bankrecipient == null)
            {
                var otherrecipient = await _context.AllUsers.Where(u => u.AccountNo == neft.AccountNo).SingleAsync();
                otherrecipient.Balance += neft.Amount;
                _context.Update(otherrecipient);
                user.Balance -= neft.Amount;
                _context.Update(user);

                //int balance 
                int userBalance = Convert.ToInt32(user.Balance);
                int neftAmount = Convert.ToInt32(neft.Amount);
                ViewBag.message = $"Amount {neftAmount}₹ Transferred to {otherrecipient.Name} Successfully";

                AddTransaction(userBalance, $"Neft Transfer to {otherrecipient.Name}", user.Id, user, neftAmount, 0);
                
            }
            else
            {
                ViewBag.message = $"Amount {neft.Amount}₹ Transferred to {bankrecipient.FirstName} {bankrecipient.LastName} Successfully";

                bankrecipient.Balance += neft.Amount;
                _context.Update(bankrecipient);
                AddTransaction((int)bankrecipient.Balance, $"Neft Transfer by {user.FirstName} {user.LastName}", bankrecipient.Id, bankrecipient, 0, (int)neft.Amount);

                user.Balance -= neft.Amount;
                _context.Update(user);

                AddTransaction((int)user.Balance, $"Neft Transfer to {bankrecipient.FirstName} {bankrecipient.LastName}", user.Id, user,(int)neft.Amount, 0);

            }
            return View();
        }

        public ActionResult MobileRecharge()
        {
            ViewBag.recharge = null;
            return View();
        }

        public ActionResult Offers()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> MobileRecharge(long mobile, double amount)
        {
            var user = GetLoggedInUSer();
           
            user.Balance = (int)user.Balance - (int)amount;
            _context.Update(user);
            ViewBag.recharge = $"Recharge of ₹{(int)amount} for Mobile Number {mobile} Successfull!";
            await _context.SaveChangesAsync();
            
            //Inserted Transaction to Transaction Table
            AddTransaction((int)user.Balance, "Mobile Recharge", user.Id, user, (int)amount, 0);
           
            return View();
        }


        public ActionResult PayElectricityBill()
        {
            ViewBag.billpaid = null;
            return View();
        }
        


        [HttpPost]
        public async Task<ActionResult> PayElectricityBill(string consumerno, string billno)
        {
            var user = await _context.AllUsers.Where(u => u.ElectricityConsumerNum == consumerno).SingleAsync();
            var bill = await _context.ElectricityBills.Where(b => b.BillNumber == billno).SingleAsync();

            //Update BillAmount in ElectricityBill Table
            bill.BillAmount = GetElectricityBillPrice(bill.UnitsConsumed);
            bill.BillUsers = user;
            _context.Update(bill);
            await _context.SaveChangesAsync();
            
            return View("GetBill", bill);
        }

        
        public async Task<ActionResult> BillPaid(int id)
        {
           // int intid = Int32.Parse(id);
            var bill = await _context.ElectricityBills.Where(u => u.Id == id).SingleAsync();
            var consumer = await _context.AllUsers.Where(u => u.Id == bill.UserId).SingleAsync();

            var user = GetLoggedInUSer();
            ViewBag.billpaid = $"Hi, {user.FirstName}. Bill of {bill.BillAmount}₹ is successfully paid against Consumer Name: {consumer.Name}, Consumer Number : {consumer.ElectricityConsumerNum}";
            user.Balance = (int)user.Balance - (int)bill.BillAmount;

            _context.Update(user);
            await _context.SaveChangesAsync();

            //Inserted Transaction to Transaction Table
            AddTransaction((int)user.Balance, "Electricity Bill", user.Id, user, (int)bill.BillAmount, 0);


            return View("PayElectricityBill");
        }

        public void AddTransaction(double closingBalance, string narration, int userId, BankUser bankUser, double withdrawal = 0, double deposit = 0)
        {
            Transaction _transaction = new Transaction()
            {
                Withdrawal = withdrawal,
                Deposit = deposit,
                ClosingBalance = (int)closingBalance,
                TransactionDate = System.DateTime.Now,
                Narration = narration,
                UserId = userId,
                BankUsers = bankUser

            };

            _context.Transactions.Add(_transaction);
            _context.SaveChanges();

        }

        public BankUser GetLoggedInUSer()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var user = _context.BankUsers.Where(u => u.Id == id).Single();

            return user;
        }

        public float GetElectricityBillPrice(int unitsConsumed)
        {
            float billAmountBelow100 = 0;
            float billAmountAbove100 = 0;
            float billAmountAbove300 = 0;
            float billAmountAbove500 = 0;

            float unitPriceBelow100 = 4.82f;
            float unitPriceAbove100 = 8.72f;
            float unitPriceAbove300 = 11.74f;
            float unitPriceAbove500 = 13.20f;

            if (unitsConsumed >= 100)
            {
                if (unitsConsumed >= 300)
                {
                    if (unitsConsumed >= 500)     //505
                    {
                        billAmountAbove500 = (unitsConsumed - 500) * unitPriceAbove500;
                        billAmountAbove300 = 200 * unitPriceAbove300;
                        billAmountAbove100 = 200 * unitPriceAbove100;
                        billAmountBelow100 = 100 * unitPriceBelow100;
                    }
                    else   //490
                    {
                        billAmountAbove300 = (unitsConsumed - 300) * unitPriceAbove300; //300 - 490
                        billAmountAbove100 = 200 * unitPriceAbove100;  //100-300
                        billAmountBelow100 = 100 * unitPriceBelow100; //1-100
                    }
                }
                else //290
                {
                    billAmountAbove100 = (unitsConsumed - 100) * unitPriceAbove100; //100-290
                    billAmountBelow100 = 100 * unitPriceBelow100;      //1-100
                }
            }
            else //90
            {
                billAmountBelow100 = unitsConsumed * unitPriceBelow100;
            }

            float billAmount = billAmountBelow100 + billAmountAbove100 + billAmountAbove300 + billAmountAbove500;

            return billAmount;
        }

    }
}
