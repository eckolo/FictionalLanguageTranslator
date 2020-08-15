﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FictionalLanguageTranslator.Models.Application.Entity;
using FictionalLanguageTranslator.Models.Application.Repository;
using FictionalLanguageTranslator.Models.Application.Service;
using FictionalLanguageTranslator.Models.Application.Value;
using Microsoft.AspNetCore.Mvc;

namespace FictionalLanguageTranslator.Controllers
{
    public class GermanStyleController : Controller
    {
        public GermanStyleController(
            SpecificCharRepository specificCharRepository,
            TranslationContext context)
        {
            this.specificCharRepository = specificCharRepository;
            this.context = context;
        }
        SpecificCharRepository specificCharRepository { get; }
        TranslationContext context { get; }

        public async Task<IActionResult> Index(string japaneseOrigin = "", string fictionalOrigin = "")
        {
            var fictionalTran = japaneseOrigin.ToFictional(specificCharRepository);
            var pronunciationTran = fictionalTran.ToGermanStylePronunciation(specificCharRepository);
            var translation = new GermanStyleTranslationModel(japaneseOrigin, fictionalTran, pronunciationTran);

            var pronunciationPron = fictionalOrigin.ToGermanStylePronunciation(specificCharRepository);
            var pronunciation = new GermanStylePronunciationModel(fictionalOrigin, pronunciationPron);

            var model = new GermanStyleIndexViewModel(pronunciation, translation);

            return View(model);
        }
    }
}