
using Microsoft.ML.Data;
using System.Collections.Generic;

namespace MLNetTry
{
    public class ChatIntentPrediction
    {
        public uint PredictedLabel { get; set; }
        public float[] Score { get; set; }
    }
}
