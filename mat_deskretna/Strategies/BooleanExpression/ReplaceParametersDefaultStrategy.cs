namespace mat_deskretna.Strategies.BooleanExpression
{
    internal class ReplaceParametersDefaultStrategy : ReplaceParametersStrategy
    {
        public ReplaceParametersDefaultStrategy(ValueObjects.BooleanExpression expr) : base(expr)
        { }

        public override string Handle(string transformed)
        {
            var parameters = Parameters;
            var result = transformed;

            for (var i = 0; i < parameters.Length; i++)
            {
                var label = $"P{i}";
                result = result.Replace(parameters[i], label);
                parameters[i] = label;
            }

            Parameters = parameters;

            return result;
        }
    }
}
