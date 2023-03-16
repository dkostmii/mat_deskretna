using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ValueOf;

namespace mat_deskretna
{
    /// <summary>
    /// This class parses boolean sentence in Polish language
    /// by transforming it into string, valid for <see cref="BooleanExpression"/>.
    /// <br></br>
    /// <br></br>
    /// Call <see cref="ValueOf.ValueOf{TValue, TThis}.From(TValue)"/> method of this class to parse sentence
    /// and pass the result of this method to same method under <see cref="BooleanExpression"/>.
    /// </summary>
    internal class BooleanSentence : ValueOf<string, BooleanSentence>
    {
        private readonly IDictionary<string, string> wordMap;
        private readonly Regex sentencePattern;

        private string _transformed;
        private string[] _parameters;

        public BooleanSentence()
        {
            wordMap = new Dictionary<string, string>()
            {
                { "nie", BooleanExpression.NOT },
                { "i", BooleanExpression.AND },
                { "lub", BooleanExpression.OR },
                { "albo", BooleanExpression.OR },
                { "jeśli", BooleanExpression.NOT },
                { "jesli", BooleanExpression.NOT },
                { "to", BooleanExpression.OR },
                { "wtedy i tylko wtedy", BooleanExpression.XOR }
            }.Select(kv =>
            {
                return KeyValuePair.Create(kv.Key.Surround(" "), kv.Value.Surround(" "));
            })
            .ToDictionary(kv => kv.Key, kv => kv.Value);

            sentencePattern = new Regex(@"^[\p{L}\p{N}\p{P}\s\(\)]+$");

            _transformed = "";
            _parameters = Array.Empty<string>();
        }

        private string HandleXorTokens(string transformed)
        {
            var split = transformed.SplitAndKeep(wordMap.Values.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            var xorIndices = split.FindAllIndices(token => token.Contains(BooleanExpression.XOR));

            if (xorIndices.Length == 0)
                return transformed;

            var result = Array.Empty<string>();

            foreach (var id in xorIndices)
            {
                var before = split
                    .Take(id - 1)
                    .Concat(new[] { BooleanExpression.NOT.Surround(" "), split[id - 1] });

                var after = new[] { split[id], BooleanExpression.NOT.Surround(" "), split[id + 1] }
                    .Concat(split.Skip(id + 2));

                result = before.Concat(after).ToArray();
            }

            return string.Join("", result);
        }

        /// <summary>
        /// Represents a string, valid for <see cref="BooleanExpression"/>'s
        /// <see cref="ValueOf.ValueOf{TValue, TThis}.From(TValue)"/> method.
        /// </summary>
        public string Transformed
        {
            get
            {
                if (string.IsNullOrEmpty(_transformed))
                {
                    _transformed = Value
                        .Sanitize()
                        .RemovePunctuation()
                        .ToLower()
                        .ReplaceAll(wordMap);

                    _transformed = HandleXorTokens(_transformed);
                }

                return _transformed;
            }
        }

        public string[] Parameters
        {
            get
            {
                if (_parameters.Length == 0)
                {
                    _parameters = Value
                        .Sanitize()
                        .RemovePunctuation()
                        .ToLower()
                        .Split(wordMap.Keys.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => p.Trim())
                        .Where(p => p.Length > 0)
                        .ToArray();
                }

                return _parameters;
            }
        }

        protected override void Validate()
        {
            var sanitized = Value.Sanitize().RemovePunctuation().ToLower();

            if (!sentencePattern.IsMatch(sanitized))
                throw new InvalidBooleanSentenceException(sanitized);
        }
    }

    internal class InvalidBooleanSentenceException : Exception
    {
        public InvalidBooleanSentenceException(string sentence) : base(
            $"Expression \"{sentence}\" is not valid boolean sentence.")
        { }
    }
}
