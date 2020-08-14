using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FictionalLanguageTranslator.Models.Application.Repository;
using FictionalLanguageTranslator.Models.Application.Service;
using FictionalLanguageTranslator.Models.Application.Value;
using Microsoft.AspNetCore.Mvc;

namespace FictionalLanguageTranslator.Controllers
{
    public class GermanStyleController : Controller
    {
        public GermanStyleController(SpecificCharRepository? specificCharRepository = null)
        {
            this.specificCharRepository = specificCharRepository ?? new SpecificCharRepository();
        }
        SpecificCharRepository specificCharRepository { get; }

        public IActionResult Index(string japanese = "")
        {
            var japaneseText = japanese;
            var fictionalText = japaneseText.ToFictional(specificCharRepository);
            var pronunciation = fictionalText.ToGermanStylePronunciation(specificCharRepository);

            var model = new GermanStyleModel(japaneseText, fictionalText, pronunciation);

            return View(model);
        }
    }
}