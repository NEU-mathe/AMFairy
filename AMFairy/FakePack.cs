using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExamSysWinform;
using ExamSysWinform.WebService;
using System.Data;


namespace fakepack
{
    class FakePack
    {
        static public bool Login(string subject, string stuNumber, string pwd)
        {
            StudentService service = new StudentService();
            ClientStudentModel selectModel = new ClientStudentModel
            {
                Key = "_3[#$%wd*",
                Version = "2.0.0",
                StudentNumber = stuNumber,
                Pwd = pwd,
                DataSource = subject
            };
            try
            {
                service.Login(selectModel);
            }
            catch(Exception)
            {
                MessageBox.Show("登录失败", "灾难性故障", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        static public string[] ExamTemplate(string subject, string stuNumber)
        {
            StudentService ws;
            ws = new StudentService();
            string[,] str = new String[100, 13];//用于迭代全部参数
            string[] strArray11 = new String[100];//全部考试名
            try
            {
                DataSet set = ws.getTemplate("_3[#$%wd*", stuNumber, "1", subject);
                DataTable dt = set.Tables[0];
                //MessageBox.Show(set.Tables[0].ToString());
                //            ChoiceSource = dt.Rows[i]["choiceSource"].ToString(),
                //            Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                //            Flag = int.Parse(dt.Rows[i]["flag"].ToString()),
                //            StartTime = DateTime.Parse(dt.Rows[i]["startTime"].ToString()),
                //            EndTime = DateTime.Parse(dt.Rows[i]["endTime"].ToString()),
                //            ExamTime = int.Parse(dt.Rows[i]["examTime"].ToString()),
                //            UsedTime = int.Parse(dt.Rows[i]["usedTime"].ToString()),
                //            Enable = bool.Parse(dt.Rows[i]["enable"].ToString()),
                //            Src = "PublishChoice/" + dt.Rows[i]["src"].ToString(),
                //            StudentAnwser = dt.Rows[i]["studentAnwser"].ToString(),
                //            Template = dt.Rows[i]["template"].ToString(),
                //            Name = dt.Rows[i]["Name"].ToString(),
                //            ResultRand = dt.Rows[i]["sortNumber"].ToString()
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str[i, 0] = dt.Rows[i]["choiceSource"].ToString();
                    str[i, 1] = dt.Rows[i]["Id"].ToString();
                    str[i, 2] = dt.Rows[i]["flag"].ToString();
                    str[i, 3] = dt.Rows[i]["startTime"].ToString();
                    str[i, 4] = dt.Rows[i]["endTime"].ToString();
                    str[i, 5] = dt.Rows[i]["examTime"].ToString();
                    str[i, 6] = dt.Rows[i]["usedTime"].ToString();
                    str[i, 7] = dt.Rows[i]["enable"].ToString();
                    str[i, 8] = "PublishChoice/" + dt.Rows[i]["src"].ToString();
                    str[i, 9] = dt.Rows[i]["studentAnwser"].ToString();
                    str[i, 10] = dt.Rows[i]["template"].ToString();
                    str[i, 11] = dt.Rows[i]["Name"].ToString();
                    str[i, 12] = dt.Rows[i]["sortNumber"].ToString();
                }
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    strArray11[i] = str[i, 11];
                }
            }
            catch(Exception)
            {
                MessageBox.Show("与服务器通信失败", "灾难性故障", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            return strArray11;

        }
        static public string[] ExamTemplate(string[] data)
        {
            StudentService ws;
            ws = new StudentService();
            //MessageBox.Show(set.Tables[0].ToString());
            string[,] str = new String[100, 13];//用于迭代全部参数
            string[][] strArray0 = new String[100][];//分割后的ChoiceSource[考试,题]
            try
            {
                DataSet set = ws.getTemplate("_3[#$%wd*", data[1], "1", data[0]);
                DataTable dt = set.Tables[0];
                //            ChoiceSource = dt.Rows[i]["choiceSource"].ToString(),
                //            Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                //            Flag = int.Parse(dt.Rows[i]["flag"].ToString()),
                //            StartTime = DateTime.Parse(dt.Rows[i]["startTime"].ToString()),
                //            EndTime = DateTime.Parse(dt.Rows[i]["endTime"].ToString()),
                //            ExamTime = int.Parse(dt.Rows[i]["examTime"].ToString()),
                //            UsedTime = int.Parse(dt.Rows[i]["usedTime"].ToString()),
                //            Enable = bool.Parse(dt.Rows[i]["enable"].ToString()),
                //            Src = "PublishChoice/" + dt.Rows[i]["src"].ToString(),
                //            StudentAnwser = dt.Rows[i]["studentAnwser"].ToString(),
                //            Template = dt.Rows[i]["template"].ToString(),
                //            Name = dt.Rows[i]["Name"].ToString(),
                //            ResultRand = dt.Rows[i]["sortNumber"].ToString()
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str[i, 0] = dt.Rows[i]["choiceSource"].ToString();
                    str[i, 1] = dt.Rows[i]["Id"].ToString();
                    str[i, 2] = dt.Rows[i]["flag"].ToString();
                    str[i, 3] = dt.Rows[i]["startTime"].ToString();
                    str[i, 4] = dt.Rows[i]["endTime"].ToString();
                    str[i, 5] = dt.Rows[i]["examTime"].ToString();
                    str[i, 6] = dt.Rows[i]["usedTime"].ToString();
                    str[i, 7] = dt.Rows[i]["enable"].ToString();
                    str[i, 8] = "PublishChoice/" + dt.Rows[i]["src"].ToString();
                    str[i, 9] = dt.Rows[i]["studentAnwser"].ToString();
                    str[i, 10] = dt.Rows[i]["template"].ToString();
                    str[i, 11] = dt.Rows[i]["Name"].ToString();
                    str[i, 12] = dt.Rows[i]["sortNumber"].ToString();
                    strArray0[i] = str[i, 0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            catch(Exception) 
            {
                MessageBox.Show("与服务器通信失败", "灾难性故障", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            try
            {
                return strArray0[int.Parse(data[2])];
            }
            catch (Exception)
            {
                MessageBox.Show("没有此次考试","警告",MessageBoxButtons.OK,MessageBoxIcon.Error);
                string[] failReturn = new String[1];
                failReturn[0] = "";
                return failReturn; 
            }

        }

        static public string[] ExamTemplate(string subject, string stuNumber, int ii)
        {
            StudentService ws;
            ws = new StudentService();
            //MessageBox.Show(set.Tables[0].ToString());
            string[,] str = new String[100, 13];//用于迭代全部参数
            string[][] strArray = new String[100][];//用于返回
            try
            {
                DataSet set = ws.getTemplate("_3[#$%wd*", stuNumber, "1", subject);
                DataTable dt = set.Tables[0];
                //            ChoiceSource = dt.Rows[i]["choiceSource"].ToString(),
                //            Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                //            Flag = int.Parse(dt.Rows[i]["flag"].ToString()),
                //            StartTime = DateTime.Parse(dt.Rows[i]["startTime"].ToString()),
                //            EndTime = DateTime.Parse(dt.Rows[i]["endTime"].ToString()),
                //            ExamTime = int.Parse(dt.Rows[i]["examTime"].ToString()),
                //            UsedTime = int.Parse(dt.Rows[i]["usedTime"].ToString()),
                //            Enable = bool.Parse(dt.Rows[i]["enable"].ToString()),
                //            Src = "PublishChoice/" + dt.Rows[i]["src"].ToString(),
                //            StudentAnwser = dt.Rows[i]["studentAnwser"].ToString(),
                //            Template = dt.Rows[i]["template"].ToString(),
                //            Name = dt.Rows[i]["Name"].ToString(),
                //            ResultRand = dt.Rows[i]["sortNumber"].ToString()
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str[i, 0] = dt.Rows[i]["choiceSource"].ToString();
                    str[i, 1] = dt.Rows[i]["Id"].ToString();
                    str[i, 2] = dt.Rows[i]["flag"].ToString();
                    str[i, 3] = dt.Rows[i]["startTime"].ToString();
                    str[i, 4] = dt.Rows[i]["endTime"].ToString();
                    str[i, 5] = dt.Rows[i]["examTime"].ToString();
                    str[i, 6] = dt.Rows[i]["usedTime"].ToString();
                    str[i, 7] = dt.Rows[i]["enable"].ToString();
                    str[i, 8] = "PublishChoice/" + dt.Rows[i]["src"].ToString();
                    str[i, 9] = dt.Rows[i]["studentAnwser"].ToString();
                    str[i, 10] = dt.Rows[i]["template"].ToString();
                    str[i, 11] = dt.Rows[i]["Name"].ToString();
                    str[i, 12] = dt.Rows[i]["sortNumber"].ToString();
                    strArray[i] = new String[4];
                    strArray[i][0] = subject;
                    strArray[i][1] = stuNumber;
                    strArray[i][2] = ii.ToString();
                    strArray[i][3] = str[i, 8];
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与服务器通信失败", "灾难性故障", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                return strArray[ii];
            }
            catch (Exception)
            {
                MessageBox.Show("没有此次考试", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string[] failReturn = new String[1];
                failReturn[0] = "";
                return failReturn;
            }

        }


    }
}
