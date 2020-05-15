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
    public partial class customLine : UserControl
    {
        public customLine()
        {
            InitializeComponent();
        }
        public customLine(Point start, Point end, Pen colorPen)
        {
            InitializeComponent();
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                g.DrawLine(colorPen, start, end);
            }

        }
    }
}
