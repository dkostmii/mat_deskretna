using mat_deskretna.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal class BiconditionalNearestStrategy : BiconditionalStrategy
    {
        public BiconditionalNearestStrategy(IEnumerable<string> boolOperators) : base(boolOperators)
        { }

        public override string Handle(string transformed)
        {
            var result = SplitSentence(transformed);

            var xorIndices = FindXorIndices(result);

            if (xorIndices.Length == 0)
                return transformed;

            for (var i = 0; i < xorIndices.Length; i++)
            {
                var id = xorIndices[i];

                var before = result
                    .Take(id - 1)
                    .Concat(new[] { ValueObjects.BooleanExpression.NOT + ValueObjects.BooleanExpression.GroupStart.Surround(" "), result[id - 1] });

                var after = new[] { result[id], result[id + 1] + ValueObjects.BooleanExpression.GroupEnd.Surround(" ") }
                    .Concat(result.Skip(id + 2));

                result = before.Concat(after).ToArray();

                xorIndices = FindAndValidateXorIndices(result, xorIndices);
            }

            return string.Join("", result).Sanitize();
        }
    }
}
