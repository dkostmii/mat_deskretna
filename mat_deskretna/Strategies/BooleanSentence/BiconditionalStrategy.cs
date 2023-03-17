using mat_deskretna.Extensions;
using mat_deskretna.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal abstract class BiconditionalStrategy : IBooleanSentenceMappingStrategy
    {
        private readonly string[] _boolOperators;

        protected BiconditionalStrategy(IEnumerable<string> boolOperators)
        {
            _boolOperators = boolOperators.ToArray();
        }

        public abstract string HandleSentence(string sentence);

        protected string[] SplitSentence(string sentence)
        {
            return sentence.SplitAndKeep(_boolOperators, StringSplitOptions.RemoveEmptyEntries);
        }

        protected int[] FindXorIndices(string[] split)
        {
            return split.FindAllIndices(token => token.Contains(BooleanExpression.XOR));
        }
    }
}
