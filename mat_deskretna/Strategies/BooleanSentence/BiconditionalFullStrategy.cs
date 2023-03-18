using mat_deskretna.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal class BiconditionalFullStrategy : BiconditionalStrategy
    {
        public BiconditionalFullStrategy(IEnumerable<string> boolOperators) : base(boolOperators)
        { }

        private IEnumerable<string> Group(IEnumerable<string> split)
        {
            return split
                    .Prepend(ValueObjects.BooleanExpression.GroupStart.Surround(" "))
                    .Append(ValueObjects.BooleanExpression.GroupEnd.Surround(" "));
        }

        private IEnumerable<string> Negate(IEnumerable<string> split)
        {
            var splitCount = split.Count();

            if (splitCount > 1)
                split = Group(split);

            return split
                    .Prepend(ValueObjects.BooleanExpression.NOT);
        }

        public override string Handle(string transformed)
        {
            var result = SplitSentence(transformed);

            var xorIndices = FindXorIndices(result);

            if (xorIndices.Length == 0)
                return transformed;

            for (var i = 0; i <= xorIndices.Length - 1; i++)
            {
                var resultCount = result.Count();

                var currentXorId = xorIndices[i];
                var nextXorId = xorIndices.Length == 1 ? resultCount - 1 : xorIndices[i + 1];

                var tokensBefore = currentXorId;
                var tokensAfter = nextXorId - currentXorId;

                var resultBefore = result
                    .Take(tokensBefore)
                    .ApplyIf(tokensBefore > 1, Group)
                    .ToArray();

                var resultAfter = result
                    .Skip(tokensBefore + 1)
                    .ApplyIf(tokensAfter > 1, Group)
                    .ToArray();

                result = resultBefore.Concat(resultAfter.Prepend(result[currentXorId])).ToArray();

                xorIndices = FindAndValidateXorIndices(result, xorIndices);

                resultCount = result.Count();
                nextXorId = xorIndices.Length == 1 ? resultCount - 1 : xorIndices[i + 1];

                result = Negate(
                        result.Take(nextXorId))
                    .Concat(
                        result.Skip(nextXorId))
                    .ToArray();

                xorIndices = FindAndValidateXorIndices(result, xorIndices);
            }
                
            // A OR B XOR C

            return string.Join("", result).Sanitize();
        }
    }
}
