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
            .Where(text => text.IsNotEmpty());
        public static List<string> BreakSyllables(this string origin, SpecificCharRepository repos)
        {
            if(!origin.IsNotEmpty())
                return new List<string>();

            var syllables = repos.consonants.Concat(repos.vowels).Where(letter => letter.IsNotEmpty());

            var syllable = syllables
                .Where(letter => origin.StartsWith(letter))
                .OrderByDescending(letter => letter.Length)
                .First();

            var trailingList = origin.Substring(syllable.Length).BreakSyllables(repos);

            var result = new[] { syllable }.Concat(trailingList).ToList();
            return result;
        }
    }
}
