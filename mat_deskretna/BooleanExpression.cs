using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ValueOf;

namespace mat_deskretna
{
    /// <summary>
    /// This class prepares boolean expression in string for usage in
    /// <see cref="DynamicExpresso.Interpreter"/>.
    /// <br></br>
    /// <br></br>
    /// Call <see cref="ValueOf.ValueOf{TValue, TThis}.From(TValue)"/> method of this class to parse expression.
    /// </summary>
    internal class BooleanExpression : ValueOf<string, BooleanExpression>
    {
        public const string AND = "AND";
        public const string OR = "OR";
        public const string XOR = "XOR";
        public const string NOT = "NOT";

        private readonly IDictionary<string, string> operatorMap;
        private readonly Regex exprPattern;

        private string _transformed;
        private string[] _parameters;

        public BooleanExpression()
        {
            operatorMap = new Dictionary<string, string>()
            {
                { AND, "&&" },
                { OR, "||" },
                { XOR, "^" },
                { NOT, "!" }
            };

            _transformed = "";
            _parameters = Array.Empty<string>();

            exprPattern = new Regex(@"^[A-Za-z\s\(\)]+$");
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
                {
                    _transformed = Value
                        .Sanitize()
                        .ToUpper()
                        .ReplaceAll(operatorMap);
                }

                return _transformed;
            }
        }

        public string[] Parameters
        {
            get
            {
                if (_parameters.Length == 0)
                {
                    _parameters = Value
                        .Sanitize()
                        .ToUpper()
                        .Split(operatorMap.Keys.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => p.Trim())
                        .Where(p => p.Length > 0)
                        .ToArray();
                }

                return _parameters;
            }
        }

        protected override void Validate()
        {
            var sanitized = Value.Sanitize().ToUpper();

            if (!exprPattern.IsMatch(sanitized))
                throw new InvalidBooleanExpressionException(sanitized);
        }
    }

    internal class InvalidBooleanExpressionException : Exception
    {
        public InvalidBooleanExpressionException(string expr) : base(
            $"Expression \"{expr}\" is not valid boolean expression.")
        { }
    }
}
