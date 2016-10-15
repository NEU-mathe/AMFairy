using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMFairy
{
    class MessageHelper
    {
        static public void sendMessage(List<Point> pts)
        {
            foreach(Point pt in pts)
            {
                sendMessage(pt);
            }
        }
        static private void sendMessage(Point pt)
        {
            //获取测试程序的窗体句柄
            IntPtr mainWnd = FindWindow(null, "未来代码研究所");
            List<IntPtr> listWnd = new List<IntPtr>();
            //获取窗体上所有控件的句柄
            EnumChildWindows(mainWnd, new CallBack(delegate (IntPtr hwnd, int _lParam)
            {
                listWnd.Add(hwnd);
                StringBuilder className = new StringBuilder(126);
                StringBuilder title = new StringBuilder(200);
                GetWindowText(hwnd, title, 200);
                Rect clientRect;
                GetClientRect(hwnd, out clientRect);
                double controlWidth = clientRect.Width;
                double controlHeight = clientRect.Height;
                double x = 0, y = 0;
                IntPtr parerntHandle = GetParent(hwnd);
                if (parerntHandle != IntPtr.Zero)
                {
                    GetWindowRect(hwnd, out clientRect);
                    Rect rect;
                    GetWindowRect(parerntHandle, out rect);
                    x = clientRect.X - rect.X;
                    y = clientRect.Y - rect.Y;
                    Debug.WriteLine(x.ToString());
                    Debug.Print(y.ToString());
                }
                return true;
            }), 0);
            int looper = 0;
            foreach (IntPtr item in listWnd)
            {
                if (looper == 4)
                {
                    int x = (int)pt.X; // X coordinate of the click 
                    int y = (int)pt.Y; // Y coordinate of the click 
                    IntPtr handle = item;
                    //StringBuilder className = new StringBuilder(100);
                    //while (className.ToString() != "Internet Explorer_Server") // The class control for the browser 
                    //{
                    //    handle = GetWindow(handle, 5); // Get a handle to the child window 
                    //    GetClassName(handle, className, className.Capacity);
                    //}

                    IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
                    IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
                    const int downCode = 0x201; // Left click down code 
                    const int upCode = 0x202; // Left click up code 
                    SendMessage(handle, downCode, wParam, lParam); // Mouse button down 
                    SendMessage(handle, upCode, wParam, lParam); // Mouse button up
                }
                looper++;
            }
        }

        #region DLLImports

        [DllImport("user32")]
        public static extern bool GetClientRect(IntPtr hwnd, out Rect lpRect);
        [DllImport("user32")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect rect);
        public void SendChar(IntPtr hand, char ch, int SleepTime)
        {
            PostMessage(hand, WM_CHAR, ch, 0);
            System.Threading.Thread.Sleep(SleepTime);
        }

        public static int WM_CHAR = 0x102;
        public static int WM_CLICK = 0x00F5;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter,
            string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int AnyPopup();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int EnumThreadWindows(IntPtr dwThreadId, CallBack lpfn, int lParam);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessageA(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        #endregion
    }
}
