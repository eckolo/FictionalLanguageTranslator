using FictionalLanguageTranslator.Models.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Service
{
    /// <summary>
    /// 文字列の再構成処理クラス
    /// </summary>
    public static class TextReconstructor
    {
        public static string ToFictional(this string origin, SpecificCharRepository repos)
        {
            return origin.SeparateSpecialChars(repos).Aggregate((text1, text2) => $"{text1},{text2}");
        }
        public static IEnumerable<string> SeparateSpecialChars(this string originText, SpecificCharRepository repos)
            => Regex.Split(originText, $"([{repos.specialCharsText}])");
    }
}
