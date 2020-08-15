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
    public class TextDecomposer
    {
        public TextDecomposer(SpecificCharRepository specificCharRepository)
        {
            this.specificCharRepository = specificCharRepository;
        }

        readonly SpecificCharRepository specificCharRepository;

        public IEnumerable<string> SeparateSpecialChars(string originText)
            => Regex
            .Split(originText, $"([{specificCharRepository.specialLetterText}])")
            .Where(text => text.IsNotEmpty());
        public List<string> BreakSyllables(string origin)
        {
            if(!origin.IsNotEmpty())
                return new List<string>();

            var syllables = specificCharRepository.consonants.Concat(specificCharRepository.vowels).Where(letter => letter.IsNotEmpty());

            var syllable = syllables
                .Where(letter => origin.StartsWith(letter))
                .OrderByDescending(letter => letter.Length)
                .FirstOrDefault();

            var originExcludedSyllable = origin.Substring(syllable?.Length ?? 1);
            var trailingList = BreakSyllables(originExcludedSyllable);

            var result = (syllable is string ? new[] { syllable } : new string[] { }).Concat(trailingList).ToList();
            return result;
        }
        public bool IsSpecialChars(string text)
            => !text.Any() || specificCharRepository.specialLetter.Any(letter => text.Contains(letter));
    }
}
