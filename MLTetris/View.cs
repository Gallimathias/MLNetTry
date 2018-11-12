using MLTetris.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTetris
{
    public partial class View : UserControl, INotifyPropertyChanged
    {
        public int CellCountX => 10;
        public int CellCountY => 20;

        public int CellWidth => Width / CellCountX;
        public int CellHeight => Height / CellCountY;

        public int Score => game.Score;

        private Timer timer;
        private Game game;
        private readonly Observer observer;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnGameOver;

        protected void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs prop)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => OnPropertyChanged(prop)));
                return;
            }
            PropertyChanged?.Invoke(this, prop);
        }
        public View()
        {
            observer = new Observer();

            game = new Game(10, 20)
            {
                CellHeight = CellHeight,
                CellWidth = CellWidth
            };
            game.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(e.PropertyName);
            };

            InitializeComponent();

            game.OnGameOver += GameOnGameOver;

            timer = new Timer()
            {
                Interval = 16
            };

            PreviewKeyDown +=  (s, e) => e.IsInputKey = true;
            KeyDown += ViewKeyDown;
            KeyUp += ViewKeyUp;

            timer.Tick += (s, e) => Invalidate();

            timer.Start();
        }

        internal void SaveModel()
        {
            observer.Write(@".\data_" + DateTime.Now.ToString("ddMMYY_hhmm") + ".dat");
        }

        private void ViewKeyUp(object sender, KeyEventArgs e)
        {
            observer.AddKeyEvent(e.KeyData, false, game.CurrentFigure);
            game.MoveBrick(e, false);
        }

        private void ViewKeyDown(object sender, KeyEventArgs e)
        {
            observer.AddKeyEvent(e.KeyData, true, game.CurrentFigure);
            game.MoveBrick(e, true);
        }

        private void GameOnGameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Game Over");
            game.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();

            game.CellHeight = CellHeight;
            game.CellWidth = CellWidth;

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            using (var pen = new Pen(new SolidBrush(Color.Black)))
            {
                for (int x = 0; x < CellCountX; x++)
                {
                    for (int y = 0; y < CellCountY; y++)
                    {
                        e.Graphics.DrawRectangle(pen, new Rectangle(
                            x * CellWidth, y * CellHeight, CellWidth, CellHeight));
                    }
                }
            }

            game.OnDraw(e.Graphics);

            base.OnPaint(e);
        }
        
    }
}
