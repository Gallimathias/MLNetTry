﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MLNetTry
{
    public class IntentResult
    {
        public Intent ResultIntent { get; }
        public float Score { get; }

        public IntentResult(int type, float score)
        {
            ResultIntent = (Intent)type;
            Score = score;
        }

        public override string ToString() => $"{ResultIntent} [{Score}]";

        public enum Intent
        {
            None,
            Greeting,
            Farewell
        }
    }
}
