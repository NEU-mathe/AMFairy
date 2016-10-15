using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMFairy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScreenshotHelper sh = new ScreenshotHelper();
            List<Point> pts = sh.getAnswerPoints();
            foreach(Point pt in pts)
            {
                MessageBox.Show("X:" + pt.X.ToString() + ", Y:" + pt.Y.ToString());
            }
        }
    }
}
