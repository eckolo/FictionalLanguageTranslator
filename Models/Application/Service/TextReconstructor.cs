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
        public static string ToFictional(this string origin, SpecificCharRepository repos)
            => origin.IsNotEmpty()
            ? origin
            .SeparateSpecialChars(repos)
            .Select(text => text.Translate(repos))
            .Aggregate((text1, text2) => $"{text1}{text2}")
            : origin;
        static string Translate(this string origin, SpecificCharRepository repos)
        {
            if(origin.IsSpecialChars(repos))
                return origin;

            var result = origin.ToCodePoint().ToText(repos);
            return result;
        }
        static string ToText(this ulong onceCode, SpecificCharRepository repos)
        {
            var vowels = repos.vowels;
            var consonants = repos.consonants;

            var vowelIndex = (int)(onceCode % (ulong)vowels.Count());

            var remainingCode = (onceCode - (ulong)vowelIndex) / (ulong)vowels.Count();
            var consonantIndex = (int)(remainingCode % (ulong)consonants.Count());

            var vowel = vowels[vowelIndex];
            var consonant = consonants[consonantIndex];

            var nextCode = (remainingCode - (ulong)consonantIndex) / (ulong)consonants.Count();
            var nextChar = nextCode <= 0 ? "" : nextCode.ToText(repos);
            return $"{consonant}{vowel}{nextChar}";
        }
    }
}
