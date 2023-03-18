using System;
using System.Linq;
using static mat_deskretna.Extensions.StringExtensions;

namespace mat_deskretna.Strategies.BooleanExpression
{
    internal class ReplaceParametersUseAlphabeticStrategy : ReplaceParametersStrategy
    {
        public ReplaceParametersUseAlphabeticStrategy(ValueObjects.BooleanExpression expr) : base(expr)
        { }

        public override string Handle(string transformed)
        {
            var result = transformed;
            var parameters = Parameters;

            if (!CanUseAlphabet(parameters))
                throw new Exception("Cannot use ReplaceParametersUseAlphabetStrategy.");

            var offset = 0;

            for (var i = 0; i < parameters.Length; i++)
            {
                var letter = NthLatinLetter(i + offset).ToUpper();

                while (parameters.Any(p => p == letter))
                {
                    offset++;
                    if (offset > 25)
                        throw new Exception("No free letters left.");

                    letter = NthLatinLetter(i + offset).ToUpper();
                }

                result = result.Replace(parameters[i], letter);
                parameters[i] = letter;
            }

            Parameters = parameters;

            return result;
        }
    }
}
