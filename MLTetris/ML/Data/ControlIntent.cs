using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.ML.Data
{
    public class ControlIntent
    {
        public float Key;

        public override string ToString() => Key.ToString();
    }
}
