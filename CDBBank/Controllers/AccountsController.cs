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
        public class AccountsController : Controller
        {
            private readonly CDBBankContext _context;

            public AccountsController(CDBBankContext context)
            {
                _context = context;
            }

            // GET: AccountUsers
            public async Task<IActionResult> Index()
            {
                return View(await _context.BankUsers.ToListAsync());
            }

            // GET: AccountUsers/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accountUser = await _context.BankUsers
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (accountUser == null)
                {
                    return NotFound();
                }

                return View(accountUser);
            }

            // GET: AccountUsers/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: AccountUsers/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("UserId,UserName,AccountNum,FirstName,LastName,Email,Mobile,Password,ConfirmPassword,Amount,Country,AccountType,PinCode,Sques,SAns,DoB")] BankUser accountUser)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(accountUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(accountUser);
            }

            // GET: AccountUsers/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accountUser = await _context.BankUsers.FindAsync(id);
                if (accountUser == null)
                {
                    return NotFound();
                }
                return View(accountUser);
            }

            // POST: AccountUsers/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,AccountNum,FirstName,LastName,Email,Mobile,Password,ConfirmPassword,Amount,Country,AccountType,PinCode,Sques,SAns,DoB")] BankUser accountUser)
            {
                if (id != accountUser.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(accountUser);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AccountUserExists(accountUser.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(accountUser);
            }

            // GET: AccountUsers/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var accountUser = await _context.BankUsers
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (accountUser == null)
                {
                    return NotFound();
                }

                return View(accountUser);
            }

            // POST: AccountUsers/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var accountUser = await _context.BankUsers.FindAsync(id);
                _context.BankUsers.Remove(accountUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool AccountUserExists(int id)
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

