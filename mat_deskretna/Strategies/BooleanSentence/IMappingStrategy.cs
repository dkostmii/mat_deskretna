﻿using System;
using System.Collections.Generic;
using System.Text;

namespace mat_deskretna.Strategies.BooleanSentence
{
    internal interface IMappingStrategy
    {
        string HandleSentence(string sentence);
    }
}