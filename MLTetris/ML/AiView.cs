using MLTetris.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

        public int Score => Game.Score;

        internal Game Game;

        private Timer timer;
        private BaseFigure[] AllFigures;
        private readonly object figureLock;
        private readonly Thea thea;

        public event EventHandler OnGameOver;


        public AiView(Thea thea)
        {
            figureLock = new object();
            this.thea = thea;
            Game = new Game(10, 20)
            {
                CellHeight = CellHeight,
                CellWidth = CellWidth
            };
            Game.PropertyChanged += (s, e) =>
            {
                thea.SetScore(Score);
            };


            Game.OnGameOver += GameOnGameOver;

            timer = new Timer()
            {
                Interval = 5000
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
            Game.Start();
        }

        public void KeyDown(Keys key)
        {
            Game.MoveBrick(new KeyEventArgs(key), true);
        }

        public void KeyUp(Keys key)
        {
            Game.MoveBrick(new KeyEventArgs(key), false);
        }

        private void Calculate()
        {
            lock (figureLock)
            {
                AllFigures = new BaseFigure[Game.AllFigures.Count];
                Game.AllFigures.CopyTo(AllFigures);
            }

            var currentFigure = Game.CurrentFigure;
            thea.SetPlayingObject(Game.AllFigures, currentFigure);
        }

        private void GameOnGameOver(object sender, EventArgs e)
        {
            thea.GameOver();
        }

        public void OnDraw(Graphics graphics)
        {
            if (AllFigures == null || AllFigures.Length < 1)
                return;


            BaseFigure[] figures;
            lock (figureLock)
            {
                figures = new BaseFigure[AllFigures.Length];
                Array.Copy(AllFigures, 0, figures, 0, AllFigures.Length);
            }

            foreach (var figure in figures)
            {
                if (!(figure is Border))
                    figure.Draw(graphics, CellWidth, CellHeight);
            }
        }


    }
}
