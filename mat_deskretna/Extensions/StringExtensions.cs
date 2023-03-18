using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using System;

namespace mat_deskretna.Extensions
{
    internal static class StringExtensions
    {
        public static string CollapseWhitespaces(this string s)
        {
            return new Regex(@"\s+").Replace(s, " ");
        }

        public static string Sanitize(this string s)
        {
            return s.CollapseWhitespaces().Trim();
        }

        /// <summary>
        /// Replaces all keys within <paramref name="tokenMap"/> in string with corresponding values.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="tokenMap"></param>
        /// <returns></returns>
        public static string ReplaceAll(this string s, IDictionary<string, string> tokenMap)
        {
            var result = s;

            // Replace with longer tokens first, end with shortest
            // For example, "abcd", then "abc", then "ab", and finally "a"
            var sortedKeys = tokenMap.Keys.OrderByDescending(s => s.Length);

            foreach (var token in sortedKeys)
                result = result.Replace(token, tokenMap[token]);

            return result;
        }

        /// <summary>
        /// Remove punctuation from string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemovePunctuation(this string s)
        {
            return new Regex(@"\p{P}").Replace(s, "");
        }

        /// <summary>
        /// Surrounds string with <paramref name="surrounding"/>.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="surrounding"></param>
        /// <returns></returns>
        public static string Surround(this string s, string surrounding)
        {
            if (s.Length == 0)
                return s;

            return surrounding + s + surrounding;
        }

        /// <summary>
        /// Splits a string, but keeps all <paramref name="separators"/> in place.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="separators"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string[] SplitAndKeep(this string s, string[] separators, StringSplitOptions options = StringSplitOptions.None)
        {
            separators = separators
                .Distinct()
                .OrderByDescending(s => s.Length)
                .ToArray();

            var arbitrarySep = "1234567890";

            var separated = s;

            var separatorPatterns = separators.Select(sep =>
            {
                var pattern = new Regex($"(?<!{Regex.Escape(arbitrarySep)}){Regex.Escape(sep)}(?!{Regex.Escape(arbitrarySep)})");
                return (sep, pattern);
            });

            foreach (var (sep, pattern) in separatorPatterns)
            {
                separated = pattern.Replace(separated, sep.Surround(arbitrarySep));
            }

            return separated
                .Split(arbitrarySep, options);
        }

        public static string MergeLeftAt(this string s, string substring)
        {
            return new Regex(@$"\s+{Regex.Escape(substring)}").Replace(s, substring);
        }

        public static string MergeRightAt(this string s, string substring)
        {
            return new Regex(@$"{Regex.Escape(substring)}\s+").Replace(s, substring);
        }

        public static string MergeAt(this string s, string substring)
        {
            return new Regex(@$"\s*{Regex.Escape(substring)}\s*").Replace(s, substring);
        }

        public static string MergeAt(this string s, string[] substrings)
        {
            return substrings.Aggregate(s, (acc, val) =>
                MergeAt(acc, val));
        }

        public static string MergeLeftAt(this string s, string[] substrings)
        {
            return substrings.Aggregate(s, (acc, val) =>
                MergeLeftAt(acc, val));
        }

        public static string MergeRightAt(this string s, string[] substrings)
        {
            return substrings.Aggregate(s, (acc, val) =>
                MergeRightAt(acc, val));
        }

        public static string NthLatinLetter(int id)
        {
            if (id < 0 & id > 25)
                throw new IndexOutOfRangeException("Expected id in range [0, 25]");

            var result = "";
            var ch = (char)('a' + id);

            result += ch;

            return result;
        }
    }
}
