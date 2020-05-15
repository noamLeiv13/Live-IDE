using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace liveIde
{
    public partial class myPreloader : UserControl
    {
        private int dir = 1;
        public myPreloader()
        {
            InitializeComponent();
        }

        private void lineSpeed_Tick(object sender, EventArgs e)
        {
            if (CircleProgressbar.Value == 100)// the precent from full circle
            {
                dir = -1;

            }
            if (CircleProgressbar.Value ==0)// the precent from full circle
            {
                dir = 1;
            }
            CircleProgressbar.Value += dir;
        }
    }
}
