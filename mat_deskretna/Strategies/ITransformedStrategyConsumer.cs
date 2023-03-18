using System;
using System.Collections.Generic;
using System.Text;

namespace mat_deskretna.Strategies
{
    internal interface ITransformedStrategyConsumer
    {
        IEnumerable<ITransformedStrategy> TransformedStrategies { get; set; }
        string ApplyTransformedStrategies(string transformed);
    }
}
