using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FictionalLanguageTranslator.Models.Application.Service;
using FictionalLanguageTranslator.Models.Application.Value;
using Microsoft.AspNetCore.Mvc;

namespace FictionalLanguageTranslator.Controllers
{
    public class GermanStyleController : Controller
    {
        public IActionResult Index(string japanese = null)
        {
            var japaneseText = japanese ?? "";
            var fictionalText = japaneseText.ToFictional();

            var model = new GermanStyleModel(japaneseText, fictionalText);

            return View(model);
        }
    }
}