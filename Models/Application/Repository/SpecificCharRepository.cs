using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Repository
{
    public class SpecificCharRepository
    {
        /// <summary>
        /// 子音
        /// </summary>
        public List<string> consonants => new List<string>
        {
            "",
            "b", "br",
            "c", "ch", "chs",
            "d", "dt",
            "f",
            "g",
            "h",
            "j",
            "k",
            "l",
            "m",
            "n", "ng",
            "p", "pf",
            "qu",
            "r", "rh",
            "s", "sch", "sp", "ss", "st", "B",
            "t", "th", "ti", "ts", "tz", "tsch",
            "v",
            "w",
            "x",
            "z",
        };
        /// <summary>
        /// 母音
        /// </summary>
        public List<string> vowels => new List<string>
        {
            "a", "aa", "ai", "ar", "au", "ay",
            "e", "ee", "eer", "ei", "er", "eu", "eur", "ey",
            "i", "ie", "ier", "ig", "ir",
            "o", "oo", "or",
            "u", "ur",
            "y",
            "A", "Ar", "Au",
            "O", "Or",
            "U", "Ur",
        };
        /// <summary>
        /// 記号
        /// </summary>
        public IEnumerable<char> specialLetter => new[]
        {
            '\r', '\n',
            '!', '?', '！', '？',
            '「', '」', '（', '）', '\'', '\'',
            ':', ';',
            '…',
            ',', '.', '、', '。',
            ' ', '　',
        };
        /// <summary>
        /// 記号を繋げた文字列
        /// </summary>
        public string specialLetterText
            => specialLetter
            .Select(letter => letter.ToString())
            .Aggregate((letter1, letter2) => $"{letter1}{letter2}");
    }
}
