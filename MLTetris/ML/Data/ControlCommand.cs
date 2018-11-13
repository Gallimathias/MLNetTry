using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTetris.ML.Data
{
    public class ControlCommand
    {
        public Keys Key { get; internal set; }
        public bool IsKeyUp { get; internal set; }
        public Type FigureType { get; internal set; }
        public int MaxX { get; internal set; }
        public int MaxY { get; internal set; }
        public int MinX { get; internal set; }
        public int MinY { get; internal set; }

        public override string ToString()
        {
            return
                ((int)Key).ToString() + ';' +
                (IsKeyUp ? "1" : "0") + ';' +
                FigureType.Name.ToString() + ';' +
                MaxX.ToString() + ';' +
                MaxY.ToString() + ';' +
                MinX.ToString() + ';' +
                MinY.ToString();
        }
    }
}
