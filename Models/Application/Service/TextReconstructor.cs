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

            var result = origin.ToCodePoint().PickConsonant(repos);
            return result;
        }
        static string PickConsonant(this ulong originCode, SpecificCharRepository repos)
        {
            var consonants = repos.consonants;

            var consonantIndex = (int)(originCode % (ulong)consonants.Count());
            var consonant = consonants[consonantIndex];

            var nextCode = (originCode - (ulong)consonantIndex) / (ulong)consonants.Count();
            var nextChar = nextCode > 0 ? nextCode.PickVowels(repos) : "";

            return $"{nextChar}{consonant}";
        }
        static string PickVowels(this ulong originCode, SpecificCharRepository repos)
        {
            var vowels = repos.vowels;

            var vowelIndex = (int)(originCode % (ulong)vowels.Count());
            var vowel = vowels[vowelIndex];

            var nextCode = (originCode - (ulong)vowelIndex) / (ulong)vowels.Count();
            var nextChar = nextCode > 0 ? nextCode.PickConsonant(repos) : "";
            return $"{nextChar}{vowel}";
        }
    }
}
