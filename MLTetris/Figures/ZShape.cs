using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.Figures
{
    class ZShape : BaseFigure
    {
        public ZShape()
        {
            Bricks.Add(new Brick { X = 1, Y = 0 });
            Bricks.Add(new Brick { X = 0, Y = 1 });
            Bricks.Add(new Brick { X = 0, Y = 2 });
            RotationBrick = new Brick { X = 1, Y = 1 };
            Bricks.Add(RotationBrick);
        }
    }
}
