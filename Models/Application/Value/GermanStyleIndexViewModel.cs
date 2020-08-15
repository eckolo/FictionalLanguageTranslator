using FictionalLanguageTranslator.Models.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleIndexViewModel
    {
        public GermanStyleIndexViewModel(
            GermanStyleTranslationModel translation,
            GermanStylePronunciationModel pronunciation,
            GermanStyleReTranslationModel retranslation)
        {
            this.translation = translation;
            this.pronunciation = pronunciation;
            this.retranslation = retranslation;
        }

        public GermanStyleTranslationModel translation { get; }
        public GermanStylePronunciationModel pronunciation { get; }
        public GermanStyleReTranslationModel retranslation { get; }

        public string pronunciationOpen => pronunciation.pronunciation.IsNotEmpty() ? "open" : string.Empty;
        public string retranslationOpen => retranslation.japanese.IsNotEmpty() ? "open" : string.Empty;
    }
}
