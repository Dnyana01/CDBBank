using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CDBBank.Models;
using Microsoft.AspNetCore.Http;

namespace CDBBank.Controllers
{
    public class BankUsersController : Controller
    {
        private readonly CDBBankContext _context;

        public BankUsersController(CDBBankContext context)
        {
            _context = context;
        }

        // GET: BankUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.BankUsers.ToListAsync());
        }

        // GET: BankUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankUser = await _context.BankUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankUser == null)
            {
                return NotFound();
            }

            return View(bankUser);
        }

        // GET: BankUsers/Create
        public IActionResult Create()
        {
            return View();
            
        }

        // POST: BankUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,AccountNum,FirstName,LastName,Email,Mobile,Password,ConfirmPassword,Balance,Country,AccountType,PinCode,Sques,SAns,DoB")] BankUser bankUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("IsCustomer", "Default");
            }
            return View(bankUser);
        }

        // GET: BankUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankUser = await _context.BankUsers.FindAsync(id);
            if (bankUser == null)
            {
                return NotFound();
            }
            return View(bankUser);
        }

        // POST: BankUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,AccountNum,FirstName,LastName,Email,Mobile,Password,ConfirmPassword,Balance,Country,AccountType,PinCode,Sques,SAns,DoB")] BankUser bankUser)
        {
            if (id != bankUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankUserExists(bankUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IsCustomer", "Default");
            }
            return View(bankUser);
        }

        // GET: BankUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankUser = await _context.BankUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankUser == null)
            {
                return NotFound();
            }

            return View(bankUser);
        }

        // POST: BankUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankUser = await _context.BankUsers.FindAsync(id);
            _context.BankUsers.Remove(bankUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankUserExists(int id)
        {
            return _context.BankUsers.Any(e => e.Id == id);
        }
        public IActionResult CreateNew()
        {
            return View();
        }

        // POST: AccountUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew([Bind("Id,Username,AccountNum,FirstName,LastName,Email,Mobile,Password,ConfirmPassword,Balance,Country,AccountType,PinCode,Sques,SAns,DoB")] BankUser accountUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountUser);
                Random r = new Random();
                int n = r.Next(0000, 9999);
                accountUser.AccountNum = "CDB00DF" + n.ToString();
                HttpContext.Session.SetString("AccNumber", accountUser.AccountNum);
                await _context.SaveChangesAsync();
                return RedirectToAction("SuccessNew");
            }
            return View(accountUser);
        }

        public IActionResult SuccessNew()
        {
            ViewBag.AccNum = HttpContext.Session.GetString("AccNumber");
            ViewBag.Id = HttpContext.Session.GetInt32("UserId");
            return View();
        }
    }
}
