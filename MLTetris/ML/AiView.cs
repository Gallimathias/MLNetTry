using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTetris.ML
{
    public class AiView
    {
        public int CellCountX => 10;
        public int CellCountY => 20;

        public int CellWidth => 362 / CellCountX; //362; 
        public int CellHeight => 520 / CellCountY; //520

        public int Score => game.Score;

        private Timer timer;
        private Game game;
        private readonly Thea thea;

        public event EventHandler OnGameOver;


        public AiView(Thea thea)
        {
            this.thea = thea;
            game = new Game(10, 20)
            {
                CellHeight = CellHeight,
                CellWidth = CellWidth
            };
            game.PropertyChanged += (s, e) =>
            {
                thea.SendScore(Score);
            };


            game.OnGameOver += GameOnGameOver;

            timer = new Timer()
            {
                Interval = 16
            };


            timer.Tick += (s, e) => Calculate();

            thea.AddKeyEvent(Keys.Down, KeyDown, KeyUp);
            thea.AddKeyEvent(Keys.Left, KeyDown, KeyUp);
            thea.AddKeyEvent(Keys.Right, KeyDown, KeyUp);
            thea.AddKeyEvent(Keys.Up, KeyDown, KeyUp);
        }

        public void Start()
        {
            thea.Start();
            timer.Start();
            game.Start();
        }

        public void KeyDown(Keys key)
        {
            game.MoveBrick(new KeyEventArgs(key), true);
        }

        public void KeyUp(Keys key)
        {
            game.MoveBrick(new KeyEventArgs(key), false);
        }

        private void Calculate()
        {
            var figures = game.AllFigures;
            var currentFigure = game.CurrentFigure;
            thea.SetPlayingObject(figures, currentFigure);
        }

        private void GameOnGameOver(object sender, EventArgs e)
        {
            thea.GameOver();
        }


    }
}
