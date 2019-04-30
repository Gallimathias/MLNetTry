using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLNetTry
{
    public class ChatMessage
    {
        [LoadColumn(0)]
        public uint Label { get; set; }
        [LoadColumn(1)]
        public string Text { get; set; }
    }
}
