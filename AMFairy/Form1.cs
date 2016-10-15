using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            notifyIcon1.BalloonTipText = "Caught.";
            notifyIcon1.ShowBalloonTip(1000);
            Thread th = new Thread(delegate () { 
                ScreenshotHelper sh = new ScreenshotHelper();
                Dictionary<Point, double> pts = sh.getAnswerPoints();
                MessageHelper.sendMessage(pts.Keys.ToList());
                string str = "Completed. Confidence: ";
                foreach (double p in pts.Values)
                {
                    str += (p * 100).ToString("G3") + "%, ";
                }
                notifyIcon1.BalloonTipText = str;
                notifyIcon1.ShowBalloonTip(2000);
            });
            th.IsBackground = true;
            try
            {
                th.Start();
            }
            catch
            {
                notifyIcon1.BalloonTipText = "Exception occured.";
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageHelper.sendMessage(new List<Point>() { new Point(1,1)});
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = true;
        }
    }
}
