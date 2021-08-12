using CDBBank.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CDBBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Offer1()
        {
            return View();
        }
        public IActionResult Offer2()
        {
            return View();
        }
        public IActionResult Offer3()
        {
            return View();
        }
        public IActionResult FD()
        {
            return View();
        }
        public IActionResult RD()
        {
            return View();
        }
        public IActionResult SIP()
        {
            return View();
        }

        public IActionResult InterestRates()
        {
            return View();
        }

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error(int statusCode)
{
var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>(); if (exceptionFeature != null)
{
ViewBag.ErrorMessage = exceptionFeature.Error.Message;
ViewBag.RouteOfException = exceptionFeature.Path;
} return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
[Route("Home/404")]
public IActionResult HandlePageNotFound()
{
var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>(); if (exceptionFeature != null)
{
ViewBag.ErrorMessage = exceptionFeature.Error.Message;
ViewBag.RouteOfException = exceptionFeature.Path;
}
return View();
} [Route("Home/{statusCode}")]
public IActionResult HandleErrorCode(int statusCode)
{
var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>(); switch (statusCode)
{
case 404:
ViewBag.ErrorMessage = "Sorry the page you requested could not be found";
ViewBag.RouteOfException = statusCodeData.OriginalPath;
break;
case 500:
ViewBag.ErrorMessage = "Sorry something went wrong on the server";
ViewBag.RouteOfException = statusCodeData.OriginalPath;
break;
default:
ViewBag.ErrorMessage = "Sorry something went wrong";
ViewBag.RouteOfException = statusCodeData.OriginalPath;
break;
} return View();
}




        //*********************8

    }
}