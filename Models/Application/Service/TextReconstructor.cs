using FictionalLanguageTranslator.Models.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Service
{
    /// <summary>
    /// 文字列の再構成処理クラス
    /// </summary>
    public static class TextReconstructor
    {
        public static string ToFictional(this string originText)
        {
            return originText.SeparateSpecialChars().Aggregate((text1, text2) => $"{text1},{text2}");
        }
        static IEnumerable<string> specialChars => SpecificCharactersRepository.specialChars;
        public static IEnumerable<string> SeparateSpecialChars(this string originText)
            => originText.Split(specialChars.ToArray(), StringSplitOptions.None);
    }
}
