using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris
{
    public class Brick
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Brick()
        {
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetRelativePosition(int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Draw(Graphics graphics, int cellWidth, int cellHeight, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush,
                    new Rectangle(cellWidth * X, cellHeight * Y, cellWidth, cellHeight));
            }
        }

        public bool Intersect(int x, int y) => x == X && y == Y;

        public override string ToString() => $"X:{X} | Y:{Y}";
    }
}
