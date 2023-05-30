using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LittleBigTraveler.Controllers
{
    public class SigninSignupController : Controller
    {
        // GET: /<controller>/
        public IActionResult SignupPage()
        {
            return View();
        }

        public IActionResult SigninPage()
        {
            return View();
        }
    }
}

