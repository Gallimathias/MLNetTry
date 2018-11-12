using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.Figures
{
    public class Square : BaseFigure
    {
        public Square()
        {
            for (int i = 0; i < 2; i++)
                for (int o = 0; o < 2; o++)
                    Bricks.Add(new Brick { X = o, Y = i });
        }

        public override void Rotate()
        {
        }
        public override void CounterRotate() => Rotate();

    }
}
