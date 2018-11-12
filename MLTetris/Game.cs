using MLTetris.Figures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MLTetris
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        public BaseFigure CurrentFigure { get; set; }
        public List<BaseFigure> AllFigures { get; set; }
        public List<Type> BlockTypes { get; set; }
        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler OnGameOver;

        /// <summary>
        /// Used for Input. Check if Key is pressed
        /// </summary>
        private Dictionary<Keys, KeyValuePair<bool, sbyte>> KeyDictionary;
        private System.Timers.Timer timer;
        private readonly Border bottom;
        private readonly Border left;
        private readonly Border right;
        private Random random;
        private int score;
        private ulong frames = 0;

        public event PropertyChangedEventHandler PropertyChanged;



        public Game(int width, int height)
        {
            Width = width;
            Height = height;

            KeyDictionary = new Dictionary<Keys, KeyValuePair<bool, sbyte>>()
            {
                [Keys.Down] = new KeyValuePair<bool, sbyte>(false, 0),
                [Keys.Left] = new KeyValuePair<bool, sbyte>(false, -1),
                [Keys.Right] = new KeyValuePair<bool, sbyte>(false, 1),
                [Keys.Up] = new KeyValuePair<bool, sbyte>(false, 0)
            };

            left = new Border(-1, 0, 1, Height);
            right = new Border(Width, 0, 1, Height);
            bottom = new Border(0, Height, Width, 1);
            AllFigures = new List<BaseFigure> { bottom, left, right };
            BlockTypes = new List<Type> { typeof(Square), typeof(Stab), typeof(Tee), typeof(LShape), typeof(JShape), typeof(SShape), typeof(ZShape) };
            random = new Random();
            NewFigure();

            timer = new System.Timers.Timer()
            {
                Interval = 33
            };

            timer.Elapsed += TimerElapsed;

            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            frames++;
            if (frames % 4 == 0)
            {
                if (KeyDictionary[Keys.Up].Key)
                {
                    KeyDictionary[Keys.Up] = new KeyValuePair<bool, sbyte>(false, 0);
                    TryRotate(CurrentFigure);
                }
            }

            KeyDictionary.Where(
               x => x.Key == Keys.Right || x.Key == Keys.Left).Where(
               x => x.Value.Key).Select(
               x => x.Value.Value).ToList().ForEach(
               x => TryMove(
               x < 0 ? -1 : 1, 0, CurrentFigure));

            if (KeyDictionary[Keys.Down].Key || frames % 10 == 0)
                if (!TryMove(0, 1, CurrentFigure))
                {
                    NewFigure();
                }

            DeleteLine();
        }

        private void NewFigure()
        {
            if (CurrentFigure != null)
                CurrentFigure.IsActive = false;

            CurrentFigure = (BaseFigure)Activator.CreateInstance(BlockTypes[random.Next(0, BlockTypes.Count)]);

            if (Collision(CurrentFigure))
            {
                timer.Stop();
                OnGameOver?.Invoke(this, new EventArgs());
            }

            AllFigures.Add(CurrentFigure);
        }
        public void Start()
        {
            AllFigures.RemoveAll(x => !(x is Border));
            timer.Start();
            Score = 0;
            KeyDictionary = new Dictionary<Keys, KeyValuePair<bool, sbyte>>()
            {
                [Keys.Down] = new KeyValuePair<bool, sbyte>(false, 0),
                [Keys.Left] = new KeyValuePair<bool, sbyte>(false, -1),
                [Keys.Right] = new KeyValuePair<bool, sbyte>(false, 1),
                [Keys.Up] = new KeyValuePair<bool, sbyte>(false, 0)
            };
        }

        private void DeleteLine()
        {
            var temp = AllFigures
              .Where(x => !x.IsActive && !(x is Border)).ToList();

            var lines = temp
                .SelectMany(x => x.BrickPositions)
                .GroupBy(x => x.Y)
                .Where(x => x.Count() == Width)
                .ToList();

            foreach (var line in lines)
            {
                Score += 100;
                foreach (var lineBrick in line)
                {
                    temp.ForEach(x => x.DeleteBrickAtPosition(lineBrick));

                }
                temp.ForEach(x => x.MoveBricksAboveY(line.First().Y));
            }
        }

        public void OnDraw(Graphics graphics)
        {
            AllFigures.ForEach(f =>
            {
                if (!(f is Border))
                    f.Draw(graphics, CellWidth, CellHeight);
            });
        }

        public void MoveBrick(KeyEventArgs keyEventArgs, bool keyUp)
        {
            if (keyEventArgs.KeyCode == Keys.Up && !keyUp)
                return;
            if (KeyDictionary.TryGetValue(keyEventArgs.KeyCode, out var val))
                KeyDictionary[keyEventArgs.KeyCode] = new KeyValuePair<bool, sbyte>(keyUp, val.Value);
        }

        public bool Collision(BaseFigure figure) => AllFigures.Where(f => f != CurrentFigure).Any(b => CurrentFigure.Intersect(b));

        public bool TryMove(int x, int y, BaseFigure figure)
        {
            figure.Move(x, y);

            if (!Collision(figure))
                return true;

            figure.Move(-x, -y);
            return false;
        }

        public bool TryRotate(BaseFigure figure)
        {
            figure.Rotate();

            if (!Collision(figure))
                return true;

            figure.CounterRotate();
            return false;
        }

        protected void OnPropertyChanged([CallerMemberName]string propName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propName));
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs prop)
        {
            PropertyChanged?.Invoke(this, prop);
        }
    }
}
