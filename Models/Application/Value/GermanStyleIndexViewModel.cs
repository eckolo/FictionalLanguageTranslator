using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleIndexViewModel
    {
        public GermanStyleIndexViewModel(
            GermanStylePronunciationModel pronunciation,
            GermanStyleTranslationModel translation)
        {
            this.pronunciation = pronunciation;
            this.translation = translation;
        }

        public GermanStylePronunciationModel pronunciation { get; }
        public GermanStyleTranslationModel translation { get; }
    }
}
