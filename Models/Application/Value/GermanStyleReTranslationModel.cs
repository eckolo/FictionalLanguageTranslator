using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleReTranslationModel
    {
        public GermanStyleReTranslationModel(string fictional, string japanese)
        {
            this.fictional = fictional;
            this.japanese = japanese;
        }

        public string fictional { get; }
        public string japanese { get; }
    }
}
