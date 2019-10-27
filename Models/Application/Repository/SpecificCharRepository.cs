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
        public IEnumerable<string> consonants => new[]
        {
            "",
            "b",
            "c", "ch", "chs",
            "d", "dt",
            "f",
            "g",
            "h",
            "ig",
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
        public IEnumerable<string> vowels => new[]
        {
            "a", "aa", "au",
            "e", "ee", "ei", "er", "eu",
            "i", "ie",
            "o", "oo",
            "u",
            "a:", "a:u",
            "o:",
            "u:", "u:r",
        };
        /// <summary>
        /// 記号
        /// </summary>
        public IEnumerable<string> specialChars => new[]
        {
            Environment.NewLine,
            "!", "?", "！", "？",
            "「", "」",
            "…",
            ",", ".", "、", "。",
            " ", "　",
        };
        /// <summary>
        /// 記号を繋げた文字列
        /// </summary>
        public string specialCharsText => specialChars.Aggregate((char1, char2) => $"{char1}{char2}");
    }
}
