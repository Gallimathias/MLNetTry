using Microsoft.ML.Runtime.Api;
using MLTetris.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.ML.Data
{
    public class GameData
    {
        public int Key { get; internal set; }
        public bool IsKeyUp { get; internal set; }
        public string FigureType { get; internal set; }
        public float MaxX { get; internal set; }
        public float MaxY { get; internal set; }
        public float MinX { get; internal set; }
        public float MinY { get; internal set; }

        public GameData(BaseFigure figure)
        {
            FigureType = figure.GetType().Name;
            MaxX = figure.BrickPositions.Max(p => p.X);
            MaxY = figure.BrickPositions.Max(p => p.Y);
            MinX = figure.BrickPositions.Min(p => p.X);
            MinY = figure.BrickPositions.Min(p => p.Y);
        }
    }
}
