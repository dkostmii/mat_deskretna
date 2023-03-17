using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ValueOf;
using System.Data;
using mat_deskretna.Extensions;
using mat_deskretna.Strategies.BooleanSentence;

namespace mat_deskretna.ValueObjects
{
    /// <summary>
    /// This class parses boolean sentence in Polish language
    /// by transforming it into string, valid for <see cref="BooleanExpression"/>.
    /// <br></br>
    /// <br></br>
    /// Call <see cref="ValueOf{TValue, TThis}.From(TValue)"/> method of this class to parse sentence
    /// and pass the result of this method to same method under <see cref="BooleanExpression"/>.
    /// </summary>
    internal class BooleanSentence : ValueOf<string, BooleanSentence>
    {
        private readonly IDictionary<string, string> wordMap;
        private readonly IEnumerable<IMappingStrategy> mappingStrategies;
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
                KeyValuePair.Create(kv.Key.Surround(" "), kv.Value.Surround(" ")))
            .ToDictionary(kv => kv.Key, kv => kv.Value);

            mappingStrategies = new List<IMappingStrategy>
            {
                new BiconditionalNearestStrategy(wordMap.Values)
            };

            sentencePattern = new Regex(@"^[\p{L}\p{N}\p{P}\s\(\)]+$");

            _transformed = "";
            _parameters = Array.Empty<string>();
        }

        private string ApplyMappingStrategies(string transformed)
        {
            return mappingStrategies.Aggregate(transformed, (acc, val) =>
            {
                return val.HandleSentence(acc);
            });
        }

        /// <summary>
        /// Represents a string, valid for <see cref="BooleanExpression"/>'s
        /// <see cref="ValueOf{TValue, TThis}.From(TValue)"/> method.
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

                    _transformed = ApplyMappingStrategies(_transformed);
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
