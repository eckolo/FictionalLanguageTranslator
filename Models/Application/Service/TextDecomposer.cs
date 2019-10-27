using FictionalLanguageTranslator.Models.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Service
{
    /// <summary>
    /// 文字列分解サービス
    /// </summary>
    public static class TextDecomposer
    {
        public static IEnumerable<string> SeparateSpecialChars(this string originText, SpecificCharRepository repos)
            => Regex
            .Split(originText, $"([{repos.specialLetterText}])")
            .Where(text => text?.Any() ?? false);
    }
}
