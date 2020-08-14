using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleModel
    {
        public GermanStyleModel(string japanese, string fictional, string pronunciation)
        {
            this.japanese = japanese;
            this.fictional = fictional;
            this.pronunciation = pronunciation;
        }

        public string japanese { get; }
        public string fictional { get; }
        public string pronunciation { get; }
    }
}
