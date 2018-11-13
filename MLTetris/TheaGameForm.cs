using MLTetris.ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MLTetris
{
    public partial class TheaGameForm : Form
    {
        private TheaView theaView;
        private AiView aiView;

        public TheaGameForm(Thea thea)
        {
            aiView = new AiView(thea);
            theaView = new TheaView(aiView);

            InitializeComponent();
            InitializeTheaView();

            aiView.Start();
        }

        private void InitializeTheaView()
        {
            SuspendLayout();
            theaView.Dock = DockStyle.Bottom;
            theaView.Location = new Point(0, 33);
            theaView.Name = "TheaView";
            theaView.Size = new Size(362, 520);
            theaView.TabIndex = 0;
            Controls.Add(theaView);
            PerformLayout();
        }
    }
}
