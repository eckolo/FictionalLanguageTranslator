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
    public class PronunciationConverter
    {
        public PronunciationConverter(SpecificCharRepository specificCharRepository, TextDecomposer textDecomposer)
        {
            this.specificCharRepository = specificCharRepository;
            this.textDecomposer = textDecomposer;
        }

        readonly SpecificCharRepository specificCharRepository;
        readonly TextDecomposer textDecomposer;

        public string Pronounce(string text)
            => text.IsNotEmpty()
            ? textDecomposer
            .SeparateSpecialChars(text)
            .Select(text => Translate(text))
            .Aggregate((text1, text2) => $"{text1}{text2}")
            : text;
        string Translate(string origin)
        {
            if(textDecomposer.IsSpecialChars(origin))
                return origin;

            var syllables = textDecomposer.BreakSyllables(origin);
            var result = RaiseSyllablesLetters(syllables);
            return result;
        }
        string RaiseSyllablesLetters(List<string> wordList, bool isFirst = true, string lastWord = "", string lastSyllable = "")
        {
            if(wordList is null || !wordList.Any())
                return "";

            var head = wordList.First();
            var headIsConsonants = specificCharRepository.consonants.Contains(head);

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
                vowels: specificCharRepository.vowels);

            var nextNowSyllable = RaiseSyllablesLetters(trailingWords, false, nextLastWord, nowSyllable);

            return $"{nowSyllable}{nextNowSyllable}";
        }
    }
}
