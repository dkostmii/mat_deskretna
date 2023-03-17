using mat_deskretna.Extensions;
using mat_deskretna.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal class BiconditionalNearestStrategy : BiconditionalStrategy
    {
        public BiconditionalNearestStrategy(IEnumerable<string> boolOperators) : base(boolOperators)
        { }

        public override string HandleSentence(string sentence)
        {
            var result = SplitSentence(sentence);

            var xorIndices = FindXorIndices(result);

            if (xorIndices.Length == 0)
                return sentence;

            foreach (var id in xorIndices)
            {
                var before = result
                    .Take(id - 1)
                    .Concat(new[] { BooleanExpression.NOT + BooleanExpression.GroupStart.Surround(" "), result[id - 1] });

                var after = new[] { result[id], result[id + 1] + BooleanExpression.GroupEnd.Surround(" ") }
                    .Concat(result.Skip(id + 2));

                result = before.Concat(after).ToArray();
            }

            return string.Join("", result).Sanitize();
        }
    }
}
