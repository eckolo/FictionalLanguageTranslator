using System;
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

            textDecomposer = new TextDecomposer(this.specificCharRepository);
            textReconstructor = new TextReconstructor(this.specificCharRepository, textDecomposer, this.context);
            pronunciationConverter = new PronunciationConverter(this.specificCharRepository, textDecomposer);
        }
        SpecificCharRepository specificCharRepository { get; }
        TranslationContext context { get; }
        TextDecomposer textDecomposer { get; }
        TextReconstructor textReconstructor { get; }
        PronunciationConverter pronunciationConverter { get; }

        public async Task<IActionResult> Index(
            string japaneseOrigin = "",
            string fictionalOrigin = "",
            string fictionalReOrigin = "")
        {
            var fictionalTran = await textReconstructor.TranslateToFictional(japaneseOrigin);
            var pronunciationTran = pronunciationConverter.Pronounce(fictionalTran);
            var translation = new GermanStyleTranslationModel(japaneseOrigin, fictionalTran, pronunciationTran);

            var pronunciationPron = pronunciationConverter.Pronounce(fictionalOrigin);
            var pronunciation = new GermanStylePronunciationModel(fictionalOrigin, pronunciationPron);

            var retranslation = new GermanStyleReTranslationModel(fictionalReOrigin, "");

            var model = new GermanStyleIndexViewModel(translation, pronunciation, retranslation);

            return View(model);
        }
    }
}