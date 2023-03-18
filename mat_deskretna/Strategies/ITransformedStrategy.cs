using System;
using System.Collections.Generic;
using System.Text;

namespace mat_deskretna.Strategies
{
    internal interface ITransformedStrategy
    {
        string Handle(string transformed);
    }
}
