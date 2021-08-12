using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CDBBank.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CDBBank.Controllers
{
    public class FDsController : Controller
    {
        private readonly CDBBankContext _context;
        HttpClient client = new HttpClient();
        string url = "https://localhost:44396/api/FDs/";

        public FDsController(CDBBankContext context)
        {
            _context = context;
        }

        // GET: FDTables/Create
        public async Task<IActionResult> Index()
        {
            //return View(await _context.FD.ToListAsync());
            return View(JsonConvert.DeserializeObject<List<FD>>(await client.GetStringAsync(url)).ToList());
        }

        // GET: FDTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fDTable = await _context.FD
                .FirstOrDefaultAsync(m => m.FdId == id);
            if (fDTable == null)
            {
                return NotFound();
            }

            return View(fDTable);
        }

        // GET: FDTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FDTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FdId,FdInvMon,Month,Period,n,UserID,FdMAmount,FdInMoney")] FD fD)
        {
            if (ModelState.IsValid)
            {
                // fD.UserID = (int)HttpContext.Session.GetInt32("UserId");

                double t = fD.Month;
                int v = (int)t;
                double r;
                HttpContext.Session.SetInt32("Tenure", v);
                double P = fD.FdInvMon;
                if (t <= 3)
                {
                    r = 0.0551;
                    double x = 1 + (r / 1);
                    double y = 1 * t;
                    double z = Math.Pow(x, y);
                    fD.FdMAmount = P * z;
                }
                else if (t <= 5)
                {
                    r = 0.0675;
                    double x = 1 + (r / 1);
                    double y = 1 * t;
                    double z = Math.Pow(x, y);
                    fD.FdMAmount = P * z;
                }
                else
                {
                    r = 0.08;
                    double x = 1 + (r / 1);
                    double y = 1 * t;
                    double z = Math.Pow(x, y);
                    fD.FdMAmount = P * z;
                }
                DateTime date = DateTime.Now.AddYears(v);
                string Date = String.Format("{0:M/d/yyyy}", date);
                HttpContext.Session.SetString("date", Date);

                fD.FdInMoney = fD.FdMAmount - P;
                int conamount = (int)fD.FdMAmount;
                int inamount = (int)fD.FdInvMon;
                int result = (int)fD.FdInMoney;
                HttpContext.Session.SetInt32("InterestOnFd", result);
                double rateinper = 100 * r;
                string Interest = Convert.ToString(rateinper);
                HttpContext.Session.SetString("RateInterest", Interest);

                HttpContext.Session.SetInt32("MAmount", conamount);
                HttpContext.Session.SetInt32("IntAmount", inamount);
                //_context.Add(fD);

                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                await client.PostAsJsonAsync<FD>(url, fD);

                return RedirectToAction("FDSuccess");
            }
            return View(fD);
        }

        public IActionResult FDSuccess()
        {
            ViewBag.Date = HttpContext.Session.GetString("date");
            ViewBag.MaturityAmount = HttpContext.Session.GetInt32("MAmount");
            ViewBag.InterestAmount = HttpContext.Session.GetInt32("InterestOnFd");
            ViewBag.time = HttpContext.Session.GetInt32("Tenure");
            ViewBag.RateOfInterest = HttpContext.Session.GetString("RateInterest");
            ViewBag.PrincipleAmount = HttpContext.Session.GetInt32("IntAmount");
            return View();
        }
        // GET: FDTables/FD
        public IActionResult FD()
        {
            return View();
        }

        // POST: FDTables/FD
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FD([Bind("FdId,FdInvMon,Month,UserID,FdMAmount,FdInMoney")] FD fD)
        {
            if (ModelState.IsValid)
            {
                // fD.UserID = (int)HttpContext.Session.GetInt32("UserId");

                double t = fD.Month;
                double r;
                double P = fD.FdInvMon;
                if (t <= 3)
                {
                    r = 0.0551;
                    double x = 1 + (r / 1);
                    double y = 1 * t;
                    double z = Math.Pow(x, y);
                    fD.FdMAmount = P * z;
                }
                else if (t <= 5)
                {
                    r = 0.0675;
                    double x = 1 + (r / 1);
                    double y = 1 * t;
                    double z = Math.Pow(x, y);
                    fD.FdMAmount = P * z;
                }
                else
                {
                    r = 0.08;
                    double x = 1 + (r / 1);
                    double y = 1 * t;
                    double z = Math.Pow(x, y);
                    fD.FdMAmount = P * z;
                }


                fD.FdInMoney = fD.FdMAmount - P;
                int conamount = (int)fD.FdMAmount;
                int inamount = (int)fD.FdInvMon;
                int result = (int)fD.FdInMoney;
                HttpContext.Session.SetInt32("InterestOnFd", result);
                int v = (int)t;
                HttpContext.Session.SetInt32("Tenure", v);
                double rateinper = 100 * r;
                DateTime date = DateTime.Now.AddYears(v);
                string Date = String.Format("{0:M/d/yyyy}", date);
                HttpContext.Session.SetString("date", Date);
                string Interest = Convert.ToString(rateinper);
                HttpContext.Session.SetString("RateInterest", Interest);

                HttpContext.Session.SetInt32("MAmount", conamount);
                HttpContext.Session.SetInt32("IntAmount", inamount);
                _context.Add(fD);

                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                return RedirectToAction("FDCalculator");
            }
            return View(fD);
        }
        public IActionResult FDCalculator()
        {
            ViewBag.Date = HttpContext.Session.GetString("date");
            ViewBag.MaturityAmount = HttpContext.Session.GetInt32("MAmount");
            ViewBag.InterestAmount = HttpContext.Session.GetInt32("InterestOnFd");
            ViewBag.time = HttpContext.Session.GetInt32("Tenure");
            ViewBag.RateOfInterest = HttpContext.Session.GetString("RateInterest");
            ViewBag.PrincipleAmount = HttpContext.Session.GetInt32("IntAmount");
            return View();
        }

        // GET: FDTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fDTable = await _context.FD.FindAsync(id);
            if (fDTable == null)
            {
                return NotFound();
            }
            return View(fDTable);
        }

        // POST: FDTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FdId,FdInvMon,Month,Period,n,UserID,FdMAmount,FdInMoney")] FD fDTable)
        {
            if (id != fDTable.FdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fDTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FDTableExists(fDTable.FdId))
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
            return View(fDTable);
        }

        // GET: FDTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fDTable = await _context.FD
                .FirstOrDefaultAsync(m => m.FdId == id);
            if (fDTable == null)
            {
                return NotFound();
            }

            return View(fDTable);
        }

        // POST: FDTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fDTable = await _context.FD.FindAsync(id);
            _context.FD.Remove(fDTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FDTableExists(int id)
        {
            return _context.FD.Any(e => e.FdId == id);
        }
    }
}
