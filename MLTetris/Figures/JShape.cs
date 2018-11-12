using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.Figures
{
    internal class JShape : BaseFigure
    {
        public JShape()
        {
            Bricks.Add(new Brick { X = 2, Y = 0 });
            Bricks.Add(new Brick { X = 1, Y = 0 });
            Bricks.Add(new Brick { X = 1, Y = 2 });
            RotationBrick = new Brick { X = 1, Y = 1 };
            Bricks.Add(RotationBrick);
        }
    }
}
