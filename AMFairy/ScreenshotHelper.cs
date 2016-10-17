using GraphicsHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        const int border = 0;
        public Problem(int x1, int x2, int x3, int x4, int x5, int y1, int y2, int y3, int y4)
        {
            this.x1 = x1; this.x2 = x2; this.x3 = x3; this.x4 = x4; this.x5 = x5;
            this.y1 = y1; this.y2 = y2; this.y3 = y3; this.y4 = y4;
        }
        public Rectangle stem
        {
            get
            {
                return new Rectangle(x2 + border, y1 + border, x5 - x2 + 1 - 2 * border, y2 - y1 + 1 - 2 * border);
            }
        }
        public Rectangle branchA
        {
            get
            {
                return new Rectangle(x2 + border, y2 + border, x3 - x2 + 1 - 2 * border, y3 - y2 + 1 - 2 * border);
            }
        }
        public Rectangle branchB
        {
            get
            {
                return new Rectangle(x4 + border, y2 + border, x5 - x4 + 1 - 2 * border, y3 - y2 + 1 - 2 * border);
            }
        }
        public Rectangle branchC
        {
            get
            {
                return new Rectangle(x2 + border, y3 + border, x3 - x2 + 1 - 2 * border, y4 - y3 + 1 - 2 * border);
            }
        }
        public Rectangle branchD
        {
            get
            {
                return new Rectangle(x4 + border, y3 + border, x5 - x4 + 1 - 2 * border, y4 - y3 + 1 - 2 * border);
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

            HashSet<int> verLines_raw = new HashSet<int>();
            HashSet<int> horLines_raw = new HashSet<int>();

            baseRes = ImageAnalysis.binaryzation(baseRes);
            int[] baseVertical = new int[baseRes.Width];
            for (int i = 0; i < baseRes.Width; i++)
            {
                baseVertical[i] = 0;
                int succession = 0;
                for (int j = 0; j < baseRes.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = baseRes.GetPixel(i, j);
                    if (color.R == 0)
                        ++succession;
                    else
                    {
                        baseVertical[i] += succession * succession;
                        succession = 0;
                    }
                }
                baseVertical[i] += succession * succession;
            }
            int[] baseHorizontal = new int[baseRes.Height];
            for (int j = 0; j < baseRes.Height; j++)
            {
                baseHorizontal[j] = 0;
                int succession = 0;
                for (int i = 0; i < baseRes.Width; i++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = baseRes.GetPixel(i, j);
                    if (color.R == 0)
                        ++succession;
                    else
                    {
                        baseHorizontal[j] += succession * succession;
                        succession = 0;
                    }
                }
                baseHorizontal[j] += succession * succession;
            }

            //获取铅垂线
            for (int i = 0; i < baseRes.Width; i++)
            {
                if (baseVertical[i] > baseRes.Height * baseRes.Height / 32)
                    verLines_raw.Add(i);
            }

            int tableWidth = verLines_raw.Max() - verLines_raw.Min();

            //获取水平线
            for (int j = 0; j < baseRes.Height; j++)
            {
                if (baseHorizontal[j] > tableWidth * tableWidth / 4)
                    horLines_raw.Add(j);
            }

            //合并连续区段
            
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
            temp = 0;
            foreach (int i in verLines_raw)
            {
                if (i - temp != 1)
                    verLines.Add(temp);
                temp = i;
            }
            verLines.Add(temp);
            verLines.Remove(0);

            //顶点矩阵并筛选铅垂线
            int[,] vertexs = new int[verLines.Count, horLines.Count];
            for(int i = verLines.Count - 1; i >= 0; --i)
            {
                int sum = 0;
                for(int j = horLines.Count - 1; j >= 0; --j)
                {
                    int x = verLines[i], y = horLines[j];
                    int cnt = 0;
                    if (x - 1 >= 0 && y - 1 >= 0 && x + 1 < baseRes.Width && y + 1 < baseRes.Height)
                    {
                        if (baseRes.GetPixel(x - 1, y).B == 0)
                            ++cnt;
                        if (baseRes.GetPixel(x + 1, y).B == 0)
                            ++cnt;
                        if (baseRes.GetPixel(x, y - 1).B == 0)
                            ++cnt;
                        if (baseRes.GetPixel(x, y + 1).B == 0)
                            ++cnt;
                    }
                    if (cnt > 2)
                    {
                        vertexs[i, j] = 1;
                        ++sum;
                    }
                    else
                        vertexs[i, j] = 0;
                }
                if (sum < horLines.Count / 4)
                {
                    verLines.RemoveAt(i);
                    for (int j = 0; j < horLines.Count; ++j)
                        vertexs[i, j] = 0;
                }
            }

            //标准化水平线
            for (int j = horLines.Count - 1; j >= 0; --j)
            {
                int sum = 0;
                for (int i = verLines.Count - 1; i >= 0; --i)
                {
                    if (vertexs[i, j] == 1)
                        ++sum;
                }
                if (sum < verLines.Count / 3)
                    horLines.RemoveAt(j);
            }

            webBrowserReference.X = 0;
            webBrowserReference.Y = horLines[0];

            //寻找奇异顶点
            foreach (int j in horLines)
            {
                foreach (int i in verLines)
                {
                    int statistics = 0;
                    for (int k = 1; k <= 2; ++k)
                    {
                        //上面没有线 下面有线
                        if (j - k >= 0 && baseRes.GetPixel(i, j - k).R > 127
                            && j + k < baseRes.Height && baseRes.GetPixel(i, j + k).R < 127)
                            ++statistics;
                    }
                    if (statistics > 1)
                        strangePoints.Add(new System.Drawing.Point(i, j));
                }
            }

#if DEBUG
            //调试输出BaseMap
            string horLineStr = "", verLineStr = "", strangePointStr = "";

            Bitmap baseMap = new Bitmap(baseRes.Width, baseRes.Height);
            foreach (int j in horLines)
            {
                for (int i = 0; i < baseRes.Width; ++i)
                {
                    Color newColor = Color.FromArgb(255, 255, 255);
                    baseMap.SetPixel(i, j, newColor);
                }
                horLineStr += j.ToString() + ", ";
            }
            foreach (int i in verLines)
            {
                for (int j = 0; j < baseRes.Height; ++j)
                {
                    Color newColor = Color.FromArgb(255, 255, 255);
                    baseMap.SetPixel(i, j, newColor);
                }
                verLineStr += i.ToString() + ", ";
            }
            foreach (System.Drawing.Point pt in strangePoints)
            {
                Color newColor = Color.FromArgb(255, 0, 0);
                baseMap.SetPixel(pt.X, pt.Y, newColor);
                strangePointStr += "(" + pt.X.ToString() + ", " + pt.Y.ToString() + "), ";
            }

            //MessageBox.Show(horLineStr, "HorLines");
            //MessageBox.Show(verLineStr, "VerLines");
            //MessageBox.Show(strangePointStr, "strangePoints");

            baseRes.Save("baseImage.png", ImageFormat.Png);
            baseMap.Save("baseMap.png", ImageFormat.Png);
#endif

            //裁剪
            for (int i = 0; i + 2 < strangePoints.Count;)
            {
                if (strangePoints[i + 1].Y == strangePoints[i + 2].Y
                    && strangePoints[i + 1].Y - strangePoints[i].Y > 1
                    && verLines.Max() - strangePoints[i].X - 1 > 1
                    && horLines.Find(item => item > horLines.Find(item2 => item2 > strangePoints[i + 1].Y)) > 0)
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
            //MessageBox.Show(similarity.First(item => item.Value == similarity.Values.Min()).Key);
#endif
            return similarity.First(item => item.Value == similarity.Values.Min()).Key;
        }

        private KeyValuePair<Point,double> findAnswerPoint(Problem pro)
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
                //MessageBox.Show(sim.ToString());
#endif
                similarity.Add(i, sim);
            }
            KeyValuePair<int, double> index = similarity.First(item => item.Value == similarity.Values.Min());
            double sum = similarity.Sum(item => item.Value);
            switch (index.Key)
            {
                case 0:
                    return new KeyValuePair<Point, double>(pro.A, 1 - 3 * index.Value / (sum - index.Value));
                case 1:
                    return new KeyValuePair<Point, double>(pro.B, 1 - 3 * index.Value / (sum - index.Value));
                case 2:
                    return new KeyValuePair<Point, double>(pro.C, 1 - 3 * index.Value / (sum - index.Value));
                case 3:
                    return new KeyValuePair<Point, double>(pro.D, 1 - 3 * index.Value / (sum - index.Value));
                default:
                    throw new Exception("Internal Error!");
            }
        }

        public Dictionary<Point, double> getAnswerPoints()
        {
            Dictionary<Point, double> lsp = new Dictionary<Point, double>();
            foreach(Problem pro in problemList)
            {
                pro.Id = findProblemId(pro);
                KeyValuePair<Point, double> ansp = findAnswerPoint(pro);
                lsp.Add(
                    new Point(
                    ansp.Key.X - webBrowserReference.X,
                    ansp.Key.Y - webBrowserReference.Y
                    ),
                    ansp.Value);
            }
            return lsp;
        }
    }
}
