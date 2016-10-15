using GraphicsHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMFairy
{
    class Problem
    {
        int x1, x2, x3, x4, x5;
        int y1, y2, y3, y4;
        string id;
        public Problem(int x1, int x2, int x3, int x4, int x5, int y1, int y2, int y3, int y4)
        {
            this.x1 = x1; this.x2 = x2; this.x3 = x3; this.x4 = x4; this.x5 = x5;
            this.y1 = y1; this.y2 = y2; this.y3 = y3; this.y4 = y4;
        }
        public Rectangle stem
        {
            get
            {
                return new Rectangle(x2 + 1, y1 + 1, x5 - x2 - 1, y2 - y1 - 1);
            }
        }
        public Rectangle branchA
        {
            get
            {
                return new Rectangle(x2 + 1, y2 + 1, x3 - x2 - 1, y3 - y2 - 1);
            }
        }
        public Rectangle branchB
        {
            get
            {
                return new Rectangle(x4 + 1, y2 + 1, x5 - x4 - 1, y3 - y2 - 1);
            }
        }
        public Rectangle branchC
        {
            get
            {
                return new Rectangle(x2 + 1, y3 + 1, x3 - x2 - 1, y4 - y3 - 1);
            }
        }
        public Rectangle branchD
        {
            get
            {
                return new Rectangle(x4 + 1, y3 + 1, x5 - x4 - 1, y4 - y3 - 1);
            }
        }
        public Point A
        {
            get
            {
                return new Point((x1 + x2) / 2, (y2 + y3) / 2);
            }
        }
        public Point B
        {
            get
            {
                return new Point((x3 + x4) / 2, (y2 + y3) / 2);
            }
        }
        public Point C
        {
            get
            {
                return new Point((x1 + x2) / 2, (y3 + y4) / 2);
            }
        }
        public Point D
        {
            get
            {
                return new Point((x3 + x4) / 2, (y3 + y4) / 2);
            }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
    class ScreenshotHelper
    {
        Bitmap baseRes;
        List<int> horLines = new List<int>();
        List<int> verLines = new List<int>();
        List<Point> strangePoints = new List<Point>();
        List<Problem> problemList = new List<Problem>();
        Point webBrowserReference;

        public ScreenshotHelper()
        {
            Image baseImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(baseImage);
            System.Drawing.Size sz = Screen.AllScreens[0].Bounds.Size;
            sz.Width = sz.Width;
            sz.Height = sz.Height;
            g.CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0), sz);
            g.Dispose();

            baseRes = new Bitmap(baseImage);

            Dictionary<int, int> verLines_raw = new Dictionary<int, int>();
            HashSet<int> horLines_raw = new HashSet<int>();

            baseRes = ImageAnalysis.binaryzation(baseRes);
            int[] baseVertical = ImageAnalysis.getVerticalHistogram(baseRes);
            int[] baseHorizontal = ImageAnalysis.getHorizontalHistogram(baseRes);

            //获取铅垂线
            for (int i = 0; i < baseRes.Width; i++)
            {
                if (baseVertical[i] > baseRes.Height / 4)
                    verLines_raw.Add(i, baseVertical[i]);
            }

            int tableWidth = verLines_raw.Max(item => item.Value) - verLines_raw.Min(item => item.Value);

            //获取水平线
            for (int j = 0; j < baseRes.Height; j++)
            {
                if (baseHorizontal[j] > tableWidth / 2)
                    horLines_raw.Add(j);
            }

            //合并连续区段
            Dictionary<int, int> verLines_dic = new Dictionary<int, int>();
            
            int temp = 0;
            foreach (int i in horLines_raw)
            {
                if (i - temp != 1)
                    horLines.Add(temp);
                temp = i;
            }
            horLines.Add(temp);
            horLines.Remove(0);
            horLines.Sort();
            KeyValuePair<int, int> kvtemp = new KeyValuePair<int, int>(0, 0);
            foreach (KeyValuePair<int, int> kv in verLines_raw)
            {
                if (kv.Key - kvtemp.Key != 1)
                    verLines_dic.Add(kvtemp.Key, kvtemp.Value);
                kvtemp = kv;
            }
            verLines_dic.Add(kvtemp.Key, kvtemp.Value);
            verLines_dic.Remove(0);

            //铅垂线只保留六根
            verLines_dic = verLines_dic.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
            int cnt = 0;
            foreach (KeyValuePair<int, int> kv in verLines_dic)
            {
                if (cnt >= 6)
                    break;
                verLines.Add(kv.Key);
                cnt++;
            }
            verLines.Sort();

            webBrowserReference = new Point(0, horLines.First());

            //寻找奇异顶点
            foreach (int j in horLines)
            {
                foreach (int i in verLines)
                {
                    int statistics = 0;
                    for (int k = 1; k <= 4; ++k)
                    {
                        //上面没有线 下面有线
                        if (j - k >= 0 && baseRes.GetPixel(i, j - k).R > 127
                            && j + k < baseRes.Height && baseRes.GetPixel(i, j + k).R < 127)
                            ++statistics;
                    }
                    if (statistics > 2)
                        strangePoints.Add(new System.Drawing.Point(i, j));
                }
            }

            //裁剪
            for (int i = 0; i + 2 < strangePoints.Count;)
            {
                if (strangePoints[i + 1].Y == strangePoints[i + 2].Y
                    && strangePoints[i + 1].Y - strangePoints[i].Y > 1
                    && verLines.Max() - strangePoints[i].X - 1 > 1)
                {
                    Problem pro = new Problem(verLines.FindLast(item => item < strangePoints[i].X),
                        strangePoints[i].X,
                        strangePoints[i+1].X,
                        strangePoints[i + 2].X,
                        verLines.Max(),
                        strangePoints[i].Y,
                        strangePoints[i + 1].Y,
                        horLines.Find(item => item > strangePoints[i + 1].Y),
                        horLines.Find(item => item > horLines.Find(item2 => item2 > strangePoints[i + 1].Y))
                        );
                    problemList.Add(pro);
                    i += 3;
                }
                else
                    i += 1;
            }
        }
        private Bitmap Crop(Rectangle rect)
        {
            return baseRes.Clone(rect, baseRes.PixelFormat);
        }
        private string findProblemId(Problem pro)
        {
            string[] files = Directory.GetFiles("Download");
            Dictionary<string, double> similarity = new Dictionary<string, double>();
            foreach (string file in files)
            {
                if(file.IndexOf('.') == -1 && file.IndexOf('_') >= 0)
                {
                    Bitmap bmp = new Bitmap(file);
                    string[] str = file.Split('_');
                    Debug.WriteLine(str[0] + str[1]);
                    if (str[3] == "0")
                        similarity.Add(string.Join("_", new string[] { str[0], str[1], str[2] }), ImageAnalysis.getVerticalSimilarity(bmp, Crop(pro.stem)));
                }
            }
#if DEBUG
            MessageBox.Show(similarity.First(item => item.Value == similarity.Values.Min()).Key);
#endif
            return similarity.First(item => item.Value == similarity.Values.Min()).Key;
        }

        private Point findAnswerPoint(Problem pro)
        {
            Bitmap[] bmps = {
                Crop(pro.branchA),
                Crop(pro.branchB),
                Crop(pro.branchC),
                Crop(pro.branchD),
            };
            Bitmap ans = new Bitmap(pro.Id + "_1");
            Dictionary<int, double> similarity = new Dictionary<int, double>();
            for(int i = 0; i < bmps.Count(); ++i)
            {
                double sim = ImageAnalysis.getVerticalSimilarity(bmps[i], ans);
#if DEBUG
                MessageBox.Show(sim.ToString());
#endif
                similarity.Add(i, sim);
            }
            int index = similarity.First(item => item.Value == similarity.Values.Min()).Key;
            switch (index)
            {
                case 0:
                    return pro.A;
                case 1:
                    return pro.B;
                case 2:
                    return pro.C;
                case 3:
                    return pro.D;
                default:
                    throw new Exception("Internal Error!");
            }
        }

        public List<Point> getAnswerPoints()
        {
            List<Point> lsp = new List<Point>();
            foreach(Problem pro in problemList)
            {
                pro.Id = findProblemId(pro);
                Point ansp = findAnswerPoint(pro);
                lsp.Add(new Point(
                    ansp.X - webBrowserReference.X,
                    ansp.Y - webBrowserReference.Y
                    ));
            }
            return lsp;
        }
    }
}
