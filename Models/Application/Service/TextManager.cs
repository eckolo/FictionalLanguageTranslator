﻿using FictionalLanguageTranslator.Models.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Service
{
    public static class TextManager
    {
        public static bool IsSpecialChars(this string text, SpecificCharRepository repos)
            => !text.Any() || repos.specialLetter.Any(letter => text.Contains(letter));

        public static bool IsNotEmpty(this string text) => text?.Any() ?? false;
    }
}
