using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLNetTry
{
    public class ChatIntentPrediction
    {        
        [ColumnName("Score")]
        public float[] Score { get; set; }
    }
}
