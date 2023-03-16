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

            exprPattern = new Regex(@"^[\p{L}\p{N}\p{P}\s\(\)]+$");
        }

        private string NthLatinLetter(int id)
        {
            if (id < 0 & id > 25)
                throw new IndexOutOfRangeException("Expected id in range [0, 25]");

            var result = "";
            var ch = (char)('a' + id);

            result += ch;

            return result;
        }

        private string ReplaceParameters(string transformed, bool useAlphabet = false)
        {
            var parameters = Parameters.OrderByDescending(p => p.Length).ToArray();
            var result = transformed;

            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters.Length < 26 && useAlphabet)
                {
                    var letter = NthLatinLetter(i).ToUpper();
                    result = result.Replace(parameters[i], letter);
                    parameters[i] = letter;
                }
                else
                {
                    var label = $"P{i}";
                    result = result.Replace(parameters[i], label);
                    parameters[i] = label;
                }
            }

            _parameters = parameters;

            return result;
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

                    _transformed = ReplaceParameters(_transformed, useAlphabet: true);
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
