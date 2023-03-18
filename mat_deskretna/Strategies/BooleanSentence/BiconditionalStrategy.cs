using mat_deskretna.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal abstract class BiconditionalStrategy : ITransformedStrategy
    {
        private readonly string[] _boolOperators;

        protected BiconditionalStrategy(IEnumerable<string> boolOperators)
        {
            _boolOperators = boolOperators.ToArray();
        }

        public abstract string Handle(string transformed);

        protected string[] SplitSentence(string sentence)
        {
            return sentence.SplitAndKeep(_boolOperators, StringSplitOptions.RemoveEmptyEntries);
        }

        protected int[] FindXorIndices(string[] split)
        {
            return split.FindAllIndices(token => token.Contains(ValueObjects.BooleanExpression.XOR));
        }

        protected void ValidateXorIndices(int[] newXorIndices, int[] prevXorIndices)
        {
            if (newXorIndices.Length != prevXorIndices.Length)
                throw new Exception($"Expected newXorIndicesLength to be {prevXorIndices.Length}. Got {newXorIndices.Length}.");
        }

        protected int[] FindAndValidateXorIndices(string[] split, int[] prevXorIndices)
        {
            var newXorIndices = FindXorIndices(split);

            ValidateXorIndices(newXorIndices, prevXorIndices);

            return newXorIndices;
        }
    }
}
