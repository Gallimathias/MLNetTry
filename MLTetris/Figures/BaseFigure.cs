using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLTetris.Figures
{
    public abstract class BaseFigure
    {
        public int Width => Bricks.Max(b => b.X) - Bricks.Min(b => b.X);
        public int Height => Bricks.Max(b => b.Y) - Bricks.Min(b => b.Y);
        public bool IsActive { get; set; }
        public List<Point> BrickPositions => Bricks.Select(x => new Point(x.X, x.Y)).ToList();
        public Color Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Relative Position</param>
        /// <param name="y">Relative Position</param>
        /// <returns>Return Brick at Relative Position</returns>
        public Brick this[int x, int y]
        {
            get
            {
                var originX = RotationBrick.X - 1;
                var originY = RotationBrick.Y - 1;

                return Bricks.FirstOrDefault(o => o.X == x + originX && o.Y == y + originY);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point">Absolute Position</param>
        /// <returns>Brick at Absolute Position</returns>
        public Brick this[Point point] => Bricks.FirstOrDefault(b => b.X == point.X && b.Y == point.Y);

        protected Brick RotationBrick;
        protected List<Brick> Bricks;

        private Random random;

        public BaseFigure()
        {
            random = new Random();
            Bricks = new List<Brick>();
            IsActive = true;
            Color = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        }

        public virtual void Rotate()
        {
            var relative = new List<Brick>();

            var x = RotationBrick.X;
            var y = RotationBrick.Y;
            Bricks.ForEach(b =>
            {
                b.X -= x;
                b.Y -= y;
                relative.Add(b);
            });

            relative.ForEach(b =>
            {
                var X = b.X;
                var Y = b.Y;
                b.X = -Y;
                b.Y = X;
                b.X += x;
                b.Y += y;
            });
        }
        public virtual void CounterRotate()
        {
            for (int i = 0; i < 3; i++)
                Rotate();
        }

        public void Draw(Graphics graphics, int cellWidth, int cellHeight) =>
           Bricks.ForEach(b => b.Draw(graphics, cellWidth, cellHeight, Color));

        public void Move(int x, int y)
        {
            Bricks.ForEach(b => b.SetRelativePosition(x, y));

        }

        public virtual bool Intersect(int x, int y) => Bricks.Any(b => b.Intersect(x, y));
        public virtual bool Intersect(Brick brick) => Bricks.Any(b => b.Intersect(brick.X, brick.Y));

        public virtual bool Intersect(BaseFigure figure) => figure.Bricks.Any(b => Intersect(b));

        public virtual void DeleteBrickAtPosition(Point p) => Bricks.Remove(this[p]);

        public virtual void MoveBricksAboveY(int y) => Bricks.Where(x => x.Y < y).ToList().ForEach(x => x.SetRelativePosition(0, 1));

    }
}
