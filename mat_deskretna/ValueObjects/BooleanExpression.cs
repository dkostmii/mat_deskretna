using mat_deskretna.Extensions;
using mat_deskretna.Strategies;
using mat_deskretna.Strategies.BooleanExpression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ValueOf;

using static mat_deskretna.Extensions.StringExtensions;

namespace mat_deskretna.ValueObjects
{
    /// <summary>
    /// This class prepares boolean expression in string for usage in
    /// <see cref="DynamicExpresso.Interpreter"/>.
    /// <br></br>
    /// <br></br>
    /// Call <see cref="ValueOf{TValue, TThis}.From(TValue)"/> method of this class to parse expression.
    /// </summary>
    internal class BooleanExpression : ValueOf<string, BooleanExpression>, ITransformedStrategyConsumer
    {
        public const string AND = "AND";
        public const string OR = "OR";
        public const string XOR = "XOR";
        public const string NOT = "NOT";
        public const string GroupStart = "(";
        public const string GroupEnd = ")";

        private readonly IDictionary<string, string> operatorMap;
        private readonly Regex exprPattern;

        private string _transformed;
        private string[] _parameters;

        public IEnumerable<ITransformedStrategy> TransformedStrategies { get; set; }

        public BooleanExpression()
        {
            operatorMap = new Dictionary<string, string>()
            {
                { AND, "&&" },
                { OR, "||" },
                { XOR, "^" },
                { NOT, "!" },
                { GroupStart, GroupStart },
                { GroupEnd, GroupEnd }
            };

            TransformedStrategies = Enumerable.Empty<ITransformedStrategy>();

            _transformed = "";
            _parameters = Array.Empty<string>();

            exprPattern = new Regex(@"^[\p{L}\p{N}\p{P}\s\(\)]+$");
        }

        public string ApplyTransformedStrategies(string transformed)
        {
            return TransformedStrategies
                .Aggregate(
                    transformed,
                    (acc, val) => val.Handle(acc)
                );
        }

        private void EvalTransformed()
        {
            _transformed = Value
                        .Sanitize()
                        .ToUpper()
                        .ReplaceAll(operatorMap);

            _transformed = ApplyTransformedStrategies(_transformed);
        }

        private void EvalParameters()
        {
            _parameters = Value
                       .Sanitize()
                       .ToUpper()
                       .Split(operatorMap.Keys.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                       .Select(p => p.Trim())
                       .Where(p => p.Length > 0)
                       .ToArray();
        }

        /// <summary>
        /// Represents boolean expression, that <see cref="DynamicExpresso.Interpreter"/> can parse.
        /// <br></br>
        /// <br></br>
        /// This expression is a valid C# code.
        /// </summary>
        public string Transformed
        {
            get
            {
                if (string.IsNullOrEmpty(_transformed))
                    EvalTransformed();

                return _transformed;
            }
        }

        public string[] Parameters
        {
            get
            {
                if (_parameters.Length == 0)
                    EvalParameters();

                return _parameters;
            }
            set
            {
                if (value.Length != _parameters.Length)
                    throw new Exception("Expected value to have same length as _parameters.");

                _parameters = value;
            }
        }

        protected override void Validate()
        {
            var sanitized = Value.Sanitize().ToUpper();

            if (!exprPattern.IsMatch(sanitized))
                throw new InvalidBooleanExpressionException(sanitized);

            EvalParameters();

            TransformedStrategies = new List<ITransformedStrategy>()
            {
                new ReplaceParametersUseAlphabeticStrategy(this),
                new SanitizeTransformedStrategy(operatorMap),
            };

            EvalTransformed();
        }
    }

    internal class InvalidBooleanExpressionException : Exception
    {
        public InvalidBooleanExpressionException(string expr) : base(
            $"Expression \"{expr}\" is not valid boolean expression.")
        { }
    }
}
