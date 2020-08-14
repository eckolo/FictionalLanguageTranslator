using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleTranslationModel : GermanStylePronunciationModel
    {
        public GermanStyleTranslationModel(string japanese, string fictional, string pronunciation)
            : base(fictional: fictional, pronunciation: pronunciation)
        {
            this.japanese = japanese;
        }

        public string japanese { get; }
    }
}
