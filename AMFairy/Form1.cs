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
        private const int HotKeyID = 1; //热键ID（自定义）
        private const int WM_HOTKEY = 0x312; //窗口消息：热键
        private const int WM_CREATE = 0x1; //窗口消息：创建
        private const int WM_DESTROY = 0x2; //窗口消息：销毁

        protected override void WndProc(ref Message msg)
        {
            base.WndProc(ref msg);
            switch (msg.Msg)
            {
                case WM_HOTKEY: //窗口消息：热键
                    int tmpWParam = msg.WParam.ToInt32();
                    if (tmpWParam == HotKeyID)
                    {
                        do_work();
                    }
                    break;
                case WM_CREATE: //窗口消息：创建
                    SystemHotKey.RegHotKey(this.Handle, HotKeyID, SystemHotKey.KeyModifiers.Ctrl, Keys.L);
                    break;
                case WM_DESTROY: //窗口消息：销毁
                    SystemHotKey.UnRegHotKey(this.Handle, HotKeyID); //销毁热键
                    break;
                default:
                    break;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            do_work();
        }

        private void do_work()
        {
            notifyIcon1.BalloonTipText = "Caught.";
            notifyIcon1.ShowBalloonTip(1000);
            try
            {
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
