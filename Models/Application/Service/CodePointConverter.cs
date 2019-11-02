using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Service
{
    /// <summary>
    /// 文字コード値制御クラス
    /// </summary>
    public static class CodePointConverter
    {
        /// <summary>
        /// 文字列の圧縮コード化
        /// </summary>
        /// <param name="text">元となる文字列</param>
        /// <returns>圧縮文字コード</returns>
        public static ulong ToCodePoint(this string text)
            => text
            .Select(code => (ulong)code)
            .Select(code => ((code - (code % 0x100)) / 0x100) + (code % 0x100))
            .Aggregate((code1, code2) => code1 * 0x10 + code2);
    }
}
