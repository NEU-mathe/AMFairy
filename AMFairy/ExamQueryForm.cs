using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fakepack;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Net;

namespace AMFairy
{
    public partial class ExamQueryForm : Form
    {
        string subject;
        Form1 fatherForm;
        public ExamQueryForm(Form1 fatherForm)
        {
            this.fatherForm = fatherForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            subject = comboBox2.Text;
            if (!FakePack.Login(subject, textBox1.Text, textBox2.Text))
                return;
            string[] str = FakePack.ExamTemplate(subject, textBox1.Text);
            foreach (string s in str)
                if (s != null && s != "")
                    comboBox1.Items.Add(s);
            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] str = FakePack.ExamTemplate(subject, textBox1.Text, comboBox1.SelectedIndex);
            string[] str2 = FakePack.ExamTemplate(str);
            string str_l = "";
            foreach (string s in str2)
                if (s != null && s != "")
                    str_l += s + ',';

            //从*.zip到*
            str_l = str_l.Replace(".zip", "");

            try
            {
                string templ = str_l;
                BackgroundWorker th = new BackgroundWorker();
                th.DoWork += delegate (object obj, DoWorkEventArgs arg)
                {
                    string[] strArray = templ.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] strArray2 = null;
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        strArray2 = strArray[i].Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                        if (!System.IO.Directory.Exists(System.IO.Directory.GetCurrentDirectory() + @"\Download"))
                        {
                            // 目录不存在，建立目录
                            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + @"\Download");
                        }
                        FileStream stream = new FileStream("./" + "Download" + "/" + strArray[i] + ".zip", FileMode.Create);
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://202.118.26.80/ChoiceSource/" + subject + "/" + strArray2[0] + "/" + strArray2[1] + "/" + strArray[i] + ".zip"));
                        request.Method = "RETR";
                        request.UseBinary = true;
                        request.Credentials = new NetworkCredential("LoginName", "Q191KPgC");
                        request.KeepAlive = false;
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        Stream responseStream = response.GetResponseStream();
                        long contentLength = response.ContentLength;
                        int count = 0x800;
                        byte[] buffer = new byte[count];
                        for (int si = responseStream.Read(buffer, 0, count); si > 0; si = responseStream.Read(buffer, 0, count))
                        {
                            stream.Write(buffer, 0, si);
                        }
                        stream.Close();
                        responseStream.Close();
                        response.Close();

                        ZipHelper.Zip.Extract(Directory.GetCurrentDirectory() + "\\Download\\" + strArray[i] + ".zip", Directory.GetCurrentDirectory() + "\\Download\\", 0x400);
                    }
                    fatherForm.showReadyMsg();
                };
                th.RunWorkerCompleted += delegate (object obj2, RunWorkerCompletedEventArgs arg2)
                {
                    this.Close();
                };
                this.Visible = false;
                th.RunWorkerAsync();
            }
            catch (Exception) { }

        }

        private void Th_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
