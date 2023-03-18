using mat_deskretna.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna.Strategies.BooleanExpression
{
    internal class SanitizeTransformedStrategy : ITransformedStrategy
    {
        private readonly IDictionary<string, string> _operatorMap;

        public SanitizeTransformedStrategy(IDictionary<string, string> operatorMap)
        {
            _operatorMap = operatorMap;
        }

        public string Handle(string transformed)
        {
            return transformed
                .Sanitize()
                .MergeRightAt(
                    new[] { ValueObjects.BooleanExpression.NOT, ValueObjects.BooleanExpression.GroupStart }
                        .Select(k => _operatorMap[k]).ToArray())
                .MergeLeftAt(_operatorMap[ValueObjects.BooleanExpression.GroupEnd]);
        }
    }
}
