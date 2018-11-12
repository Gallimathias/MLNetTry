using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MLTetris.Figures;
using MLTetris.ML.Data;

namespace MLTetris.ML
{
    public class Thea : Ai
    {
        private readonly Dictionary<Keys, (Action<Keys> Down, Action<Keys> Up)> keyEvents;
        public Thea()
        {
            keyEvents = new Dictionary<Keys, (Action<Keys> Down, Action<Keys> Up)>();
        }

        internal void Start()
        {
        }

        internal void AddKeyEvent(Keys key, Action<Keys> keyDown, Action<Keys> keyUp)
        {
            keyEvents.Add(key, (keyDown, keyUp));
        }

        internal void GameOver()
        {
        }

        internal void SetScore(int score)
        {
        }

        internal void SetPlayingObject(List<BaseFigure> figures, BaseFigure currentFigure)
        {
            if (currentFigure == null)
                return;

            var control = Predict(new GameData(currentFigure));

            if (currentFigure is Square)
                ;
        }
    }
}
