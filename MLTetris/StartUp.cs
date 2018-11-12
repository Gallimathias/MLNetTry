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
    public partial class StartUp : Form
    {
        public StartUp()
        {
            InitializeComponent();
        }

        private void TrainButtonClick(object sender, EventArgs e) 
            => Show(new GameForm());

        private void Show(Form form)
        {
            
            Visible = false;
            form.ShowDialog();
            Visible = true;
        }

        private void BuildButtonClick(object sender, EventArgs e)
        {
            var thea = new Thea();
            thea.Build(@"current.dat");
            MessageBox.Show("Build successfull");
        }

        private void PlayButtonClick(object sender, EventArgs e)
        {
            var thea = new Thea();
            thea.Load(@"");
            var theaForm = new TheaGameForm(thea);
            Show(theaForm);
        }
    }
}
