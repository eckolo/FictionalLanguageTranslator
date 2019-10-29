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

            var result = origin.BreakSyllables(repos).RaiseSyllablesLetters(repos);
            return result;
        }
        static string RaiseSyllablesLetters(this List<string> syllableList, SpecificCharRepository repos, bool isFirst = true, string lastSyllable = "")
        {
            if(syllableList is null || !syllableList.Any())
                return "";

            var head = syllableList.First();
            var headIsConsonants = repos.consonants.Contains(head);

            var consonant = headIsConsonants ? head : "";
            var vowel = headIsConsonants ? (syllableList.Skip(1).FirstOrDefault() ?? "") : head;
            var trailingSyllables = syllableList.Skip(headIsConsonants ? 2 : 1).ToList();

            var isLast = !trailingSyllables.Any();
            var nextSyllable = trailingSyllables.FirstOrDefault();
            var longVowel = (!isLast && repos.consonants.Contains(nextSyllable) && nextSyllable.Length == 1)
                ? "ー"
                : "";
            var nextLastSyllable = vowel.Any() ? vowel : consonant;

            var nowSyllable = (consonant, vowel)
                .ToSyllable(
                isFirst: isFirst,
                isLast: isLast,
                lastSyllable: lastSyllable,
                nextSyllable: nextSyllable,
                longVowel: longVowel,
                vowels: repos.vowels);

            var nextNowSyllable = trailingSyllables.RaiseSyllablesLetters(repos, false, nextLastSyllable);

            return $"{nowSyllable}{nextNowSyllable}";
        }
    }
}
