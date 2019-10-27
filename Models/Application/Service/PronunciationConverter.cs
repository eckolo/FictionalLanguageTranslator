using FictionalLanguageTranslator.Models.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Service
{
    /// <summary>
    /// 文字列の音声化処理クラス
    /// </summary>
    public static class PronunciationConverter
    {
        public static string ToGermanStylePronunciation(this string text, SpecificCharRepository repos)
            => text.IsNotEmpty()
            ? text
            .SeparateSpecialChars(repos)
            .Select(text => text.Translate(repos))
            .Aggregate((text1, text2) => $"{text1}{text2}")
            : text;
        static string Translate(this string origin, SpecificCharRepository repos)
        {
            if(origin.IsSpecialChars(repos))
                return origin;

            var result = origin.BreakSyllables(repos).RaiseSyllablesLetters();
            return result;
        }
        static string RaiseSyllablesLetters(this List<string> syllableList)
        {
            return syllableList.Aggregate((text1, text2) => $"{text1},{text2}");
        }
    }
}
