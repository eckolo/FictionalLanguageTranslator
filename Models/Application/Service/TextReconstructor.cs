using FictionalLanguageTranslator.Models.Application.Entity;
using FictionalLanguageTranslator.Models.Application.Repository;
using Microsoft.EntityFrameworkCore;
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
        public TextReconstructor(SpecificCharRepository specificCharRepository, TextDecomposer textDecomposer, TranslationContext context)
        {
            this.specificCharRepository = specificCharRepository;
            this.textDecomposer = textDecomposer;
            this.context = context;
        }

        readonly SpecificCharRepository specificCharRepository;
        readonly TextDecomposer textDecomposer;
        readonly TranslationContext context;

        public async Task<string> TranslateToJapanese(string fictionalText)
        {
            if(!fictionalText.IsNotEmpty())
                return fictionalText;

            var japaneseText = textDecomposer.SeparateSpecialChars(fictionalText)
                .Select(fictional => ReTranslate(fictional))
                .Aggregate((japanese1, japanese2) => $"{japanese1}{japanese2}");

            return japaneseText;
        }
        public async Task<string> TranslateToFictional(string japaneseText)
        {
            if(!japaneseText.IsNotEmpty())
                return japaneseText;

            var transPairList = textDecomposer.SeparateSpecialChars(japaneseText)
                .Select(japanese => (japanese, fictional: Translate(japanese)))
                .ToList();

            var fictionalText = transPairList
                .Select(pair => pair.fictional)
                .Aggregate((fictional1, fictional2) => $"{fictional1}{fictional2}");

            return fictionalText;
        }

        string Translate(string japanese)
        {
            if(textDecomposer.IsSpecialChars(japanese))
                return japanese;

            var codePoint = japanese.ToCodePoint();
            var fictional = PickConsonant(codePoint);

            return fictional;
        }
        string ReTranslate(string fictional)
        {
            if(textDecomposer.IsSpecialChars(fictional))
                return fictional;

            var japanese = context.records
                .SingleOrDefault(record => record.fictional == fictional)?
                .japanese
                ?? string.Empty;

            return japanese;
        }
        async Task<int> SetTransRecord(string japanese, string fictional)
        {
            if(japanese == fictional)
                return 0;
            if(textDecomposer.IsSpecialChars(japanese))
                return 0;

            var exsistJapaneseQuery = context.records.Where(record => record.japanese == japanese);
            var exsistFictionalQuery = context.records.Where(record => record.fictional == fictional);
            context.records.RemoveRange(exsistJapaneseQuery);
            context.records.RemoveRange(exsistFictionalQuery);

            var newRecord = new TranslationRecord { japanese = japanese, fictional = fictional };
            context.records.Add(newRecord);

            return await context.SaveChangesAsync();
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
