namespace mat_deskretna.Strategies.BooleanExpression
{
    internal abstract class ReplaceParametersStrategy : ITransformedStrategy
    {
        private readonly ValueObjects.BooleanExpression _expr;

        protected bool CanUseAlphabet(string[] parameters)
        {
            return parameters.Length <= ('z' - 'a' + 1);
        }

        protected string[] Parameters
        {
            get => _expr.Parameters;
            set
            {
                _expr.Parameters = value;
            }
        }

        public ReplaceParametersStrategy(ValueObjects.BooleanExpression expr)
        {
            _expr = expr;
        }

        public abstract string Handle(string transformed);
    }
}
