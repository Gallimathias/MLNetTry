using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using MLTetris.ML;

namespace MLTetris
{
    public partial class TheaView : UserControl, INotifyPropertyChanged
    {
        public int CellCountX => 10;
        public int CellCountY => 20;

        public int CellWidth => Width / CellCountX;
        public int CellHeight => Height / CellCountY;

        private AiView internalView;
        private Timer timer;

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
        public TheaView(AiView aiView)
        {
            internalView = aiView;

            timer = new Timer()
            {
                Interval = 16
            };
                      
            timer.Tick += (s, e) => Invalidate();

            timer.Start();
        }
                

        private void GameOnGameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Game Over");
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();


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

            internalView.OnDraw(e.Graphics);

            base.OnPaint(e);
        }
    }
}
