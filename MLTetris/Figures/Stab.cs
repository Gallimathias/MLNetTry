using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.Figures
{
    public class Stab : BaseFigure
    {
        public Stab()
        {
            for (int i = 0; i < 4; i++)
            {
                var b = new Brick { X = 0, Y = i };
                Bricks.Add(b);
                if (i == 1)
                    RotationBrick = b;
            }
        }
    }
}
