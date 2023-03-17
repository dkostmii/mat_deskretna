using mat_deskretna.Extensions;
using mat_deskretna.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal class BiconditionalFullStrategy : BiconditionalStrategy
    {
        public BiconditionalFullStrategy(IEnumerable<string> boolOperators) : base(boolOperators)
        { }

        public override string HandleSentence(string sentence)
        {
            var result = SplitSentence(sentence);

            var xorIndices = FindXorIndices(result);

            if (xorIndices.Length == 0)
                return sentence;
            else if (xorIndices.Length == 1)
            {
                result = result
                    .Prepend(BooleanExpression.GroupStart.Surround(" "))
                    .Prepend(BooleanExpression.NOT)
                    .Append(BooleanExpression.GroupEnd.Surround(" ")).ToArray();

                return string.Join("", result).Sanitize();
            }

            for (var i = 0; i < xorIndices.Length - 1; i++)
            {
                result = result
                    .Take(xorIndices[i + 1])
                    .Prepend(BooleanExpression.GroupStart.Surround(" "))
                    .Prepend(BooleanExpression.NOT)
                    .Append(BooleanExpression.GroupEnd.Surround(" "))
                    .Concat(result.Skip(xorIndices[i + 1])).ToArray();

                var newXorIndices = FindXorIndices(result);

                if (newXorIndices.Length != xorIndices.Length)
                    throw new Exception($"Expected newXorIndicesLength to be {xorIndices.Length}. Got {newXorIndices.Length}.");

                xorIndices = FindXorIndices(result);
            }

            return string.Join("", result).Sanitize();

            // NOT (A OR B XOR C AND D) XOR E AND F OR G XOR D
        }
    }
}
