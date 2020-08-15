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
    public class TextReconstructor
    {
        public TextReconstructor(SpecificCharRepository specificCharRepository, TextDecomposer textDecomposer)
        {
            this.specificCharRepository = specificCharRepository;
            this.textDecomposer = textDecomposer;
        }

        readonly SpecificCharRepository specificCharRepository;
        readonly TextDecomposer textDecomposer;

        public string TranslateToFictional(string origin)
        {
            if(!origin.IsNotEmpty())
                return origin;

            return textDecomposer.SeparateSpecialChars(origin)
                .Select(text => Translate(text))
                .Aggregate((text1, text2) => $"{text1}{text2}");
        }

        string Translate(string origin)
        {
            if(textDecomposer.IsSpecialChars(origin))
                return origin;
            var codePoint = origin.ToCodePoint();
            var result = PickConsonant(codePoint);
            return result;
        }
        string PickConsonant(ulong originCode)
        {
            var consonants = specificCharRepository.consonants;

            var consonantIndex = (int)(originCode % (ulong)consonants.Count());
            var consonant = consonants[consonantIndex];

            var nextCode = (originCode - (ulong)consonantIndex) / (ulong)consonants.Count();
            var nextChar = nextCode > 0 ? PickVowels(nextCode) : "";

            return $"{nextChar}{consonant}";
        }
        string PickVowels(ulong originCode)
        {
            var vowels = specificCharRepository.vowels;

            var vowelIndex = (int)(originCode % (ulong)vowels.Count());
            var vowel = vowels[vowelIndex];

            var nextCode = (originCode - (ulong)vowelIndex) / (ulong)vowels.Count();
            var nextChar = nextCode > 0 ? PickConsonant(nextCode) : "";
            return $"{nextChar}{vowel}";
        }
    }
}
