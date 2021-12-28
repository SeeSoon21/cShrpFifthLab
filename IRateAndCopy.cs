using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace just_try
{
    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();

    }
}
