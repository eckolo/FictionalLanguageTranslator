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
        static string RaiseSyllablesLetters(this List<string> wordList, SpecificCharRepository repos, bool isFirst = true, string lastWord = "", string lastSyllable = "")
        {
            if(wordList is null || !wordList.Any())
                return "";

            var head = wordList.First();
            var headIsConsonants = repos.consonants.Contains(head);

            var consonant = headIsConsonants ? head : "";
            var vowel = headIsConsonants ? (wordList.Skip(1).FirstOrDefault() ?? "") : head;
            var trailingWords = wordList.Skip(headIsConsonants ? 2 : 1).ToList();

            var isLast = !trailingWords.Any();
            var nextWord = trailingWords.FirstOrDefault();
            var nextLastWord = vowel.Any() ? vowel : consonant;

            var nowSyllable = (consonant, vowel)
                .ToSyllable(
                isFirst: isFirst,
                isLast: isLast,
                lastWord: lastWord,
                nextWord: nextWord,
                lastSyllable: lastSyllable,
                vowels: repos.vowels);

            var nextNowSyllable = trailingWords.RaiseSyllablesLetters(repos, false, nextLastWord, nowSyllable);

            return $"{nowSyllable}{nextNowSyllable}";
        }
    }
}
