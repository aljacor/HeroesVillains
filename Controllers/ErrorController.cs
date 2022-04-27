using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesVillains.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    ViewBag.ErrorMessage = "Oh, the character you're looking for doesn't exist";
                    break;
                case 404:
                    ViewBag.ErrorMessage = "Oh, the thing than you are looking for doesn't exist in this universe";
                    break;
            }
            return View("NotFound");
        }
    }
}
