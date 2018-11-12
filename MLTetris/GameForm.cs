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
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
            ScoreLabel.DataBindings.Add(new Binding("Text", View, "Score"));
            exitButton.Click += (s, e) => Close();
            saveButton.Click += (s, e) => View.SaveModel();
        }
    }
}
