using System;

namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = string.Empty;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
