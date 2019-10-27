using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleModel
    {
        public GermanStyleModel(string japanese, string fictional)
        {
            this.japanese = japanese ?? throw new ArgumentNullException(nameof(japanese));
            this.fictional = fictional ?? throw new ArgumentNullException(nameof(fictional));
        }

        public string japanese { get; }
        public string fictional { get; }
    }
}
