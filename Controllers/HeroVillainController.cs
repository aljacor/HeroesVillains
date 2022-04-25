using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Controllers
{
    public class HeroVillainController : Controller
    {
        
        public IActionResult Index(string search)
        {
            return View();
        }
    }
}
