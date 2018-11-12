using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLTetris.Figures;

namespace MLTetris.ML
{
    public class Thea
    {
        public Thea()
        {
        }

        internal void Start() => throw new NotImplementedException();

        internal void AddKeyEvent(Keys down, Action<Keys> keyDown, Action<Keys> keyUp)
        {
            throw new NotImplementedException();
        }

        internal void GameOver() => throw new NotImplementedException();
        internal void SetScore(int score) => throw new NotImplementedException();
        internal void SetPlayingObject(List<BaseFigure> figures, BaseFigure currentFigure) => throw new NotImplementedException();
    }
}
