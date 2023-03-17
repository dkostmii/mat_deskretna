using mat_deskretna.Extensions;
using mat_deskretna.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal class BiconditionalNearestStrategy : IMappingStrategy
    {
        private readonly string[] _boolOperators;

        public BiconditionalNearestStrategy(IEnumerable<string> boolOperators)
        {
            _boolOperators = boolOperators.ToArray();
        }

        public string HandleSentence(string sentence)
        {
            var split = sentence.SplitAndKeep(_boolOperators, StringSplitOptions.RemoveEmptyEntries);

            var xorIndices = split.FindAllIndices(token => token.Contains(BooleanExpression.XOR));

            if (xorIndices.Length == 0)
                return sentence;

            var result = Array.Empty<string>();

            foreach (var id in xorIndices)
            {
                var before = split
                    .Take(id - 1)
                    .Concat(new[] { BooleanExpression.NOT + BooleanExpression.GroupStart.Surround(" "), split[id - 1] });

                var after = new[] { split[id], split[id + 1] + BooleanExpression.GroupEnd.Surround(" ") }
                    .Concat(split.Skip(id + 2));

                result = before.Concat(after).ToArray();
            }

            return string.Join("", result).Sanitize();
        }
    }
}
