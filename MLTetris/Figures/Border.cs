using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.Figures
{
    public class Border : BaseFigure
    {
        public Border(int x, int y, int width, int height)
        {
            IsActive = false;
            for (int i = x; i < x + width; i++)
                for (int o = y; o < y + height; o++)
                    Bricks.Add(new Brick { X = i, Y = o });
        }

        public override void Rotate()
        {
        }

        public override void CounterRotate() => Rotate();

        public new void Draw(Graphics graphics, int cellWidth, int cellHeight)
        {

        }
    }
}
