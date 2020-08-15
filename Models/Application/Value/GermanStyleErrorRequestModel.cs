using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStyleErrorRequestModel
    {
        public string errorText { get; set; } = new Exception(nameof(GermanStyleErrorRequestModel)).ToString();
    }
}
