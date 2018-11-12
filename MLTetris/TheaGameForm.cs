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
        private AiView aiView;

        public TheaGameForm(Thea thea)
        {
            InitializeComponent();
            aiView = new AiView(thea);
            aiView.Start();
        }
    }
}
