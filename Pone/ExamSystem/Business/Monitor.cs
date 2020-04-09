/************************************************************/
//【项目】：远程监控
//【创建】：2005年10月
//【作者】：SmartKernel
//【邮箱】：smartkernel@126.com
//【QQ  】：120018689
//【MSN 】：smartkernel@hotmail.com
//【网站】：www.SmartKernel.com
/************************************************************/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace SmartKernel.Net1
{
    public class Monitor : System.MarshalByRefObject
    {
        #region 常量
        private const uint MOUSEEVENTF_MOVE = 0x0001; //系统消息：鼠标移动
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002; //系统消息：左键按下
        private const uint MOUSEEVENTF_LEFTUP = 0x0004; //系统消息：左键放开
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008; //系统消息：右键按下
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010; //系统消息：右键放开
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020; //系统消息：中间健按下
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040; //系统消息：中间健放开
        private const uint MOUSEEVENTF_WHEEL = 0x0800; //系统消息：滚动滚轮
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000; //指定鼠标坐标系统中的一个绝对位置
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001; //一个扩展键
        private const uint KEYEVENTF_KEYUP = 0x0002; //模拟松开一个键
        private const uint INPUT_MOUSE = 0;      //模拟鼠标事件
        private const uint INPUT_KEYBOARD = 1;      //模拟键盘事件
        private static byte[] PreviousBitmapBytes = null;
        #endregion

        #region 构造函数
        public Monitor()
        {

        }
        #endregion

        #region Win32API方法包装
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt
        (
            IntPtr hdcDest, //指向目标设备环境的句柄
            int nXDest, //指定目标矩形区域克上角的X轴逻辑坐标
            int nYDest, //指定目标矩形区域左上角的Y轴逻辑坐标
            int nWidth, //指定源和目标矩形区域的逻辑宽度
            int nHeight, //指定源和目标矩形区域的逻辑高度
            IntPtr hdcSrc, //指向源设备环境句柄
            int nXSrc, //指定源矩形区域左上角的X轴逻辑坐标
            int nYSrc, //指定源矩形区域左上角的Y轴逻辑坐标
            System.Int32 dwRop //指定光栅操作代码。这些代码将定义源矩形区域的颜色数据，如何与目标矩形区域的颜色数据组合以完成最后的颜色
        );

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        private static extern uint SendInput
        (
            uint nInputs,
            ref INPUT input,
            int cbSize
        );

        [DllImport("user32.dll")]
        private static extern void SetCursorPos(int x, int y);
        #endregion

        #region Win32结构包装
        struct MOUSE_INPUT
        {
            public uint dx;
            public uint dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public uint dwExtraInfo;
        }

        struct KEYBD_INPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public uint dwExtraInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUT
        {
            [FieldOffset(0)]
            public uint type;

            [FieldOffset(4)]
            public MOUSE_INPUT mi;

            [FieldOffset(4)]
            public KEYBD_INPUT ki;
        }
        #endregion

        #region 获得屏幕的大小
        public Size GetDesktopBitmapSize()
        {
            return new Size(GetSystemMetrics(0), GetSystemMetrics(1));
        }
        #endregion

        #region 模拟鼠标、键盘操作
        public void PressOrReleaseMouseButton(bool Press, bool Left, int X, int Y)
        {
            //    INPUT input = new INPUT();

            //    input.type = INPUT_MOUSE;
            //    input.mi.dx = (uint)X;
            //    input.mi.dy = (uint)Y;
            //    input.mi.mouseData = 0;
            //    input.mi.dwFlags = 0;
            //    input.mi.time = 0;
            //    input.mi.dwExtraInfo = 0;

            if (Left)
            {
                //按下鼠标左键
                MouseHookHelper.mouse_event(Press ? MouseHookHelper.MOUSEEVENTF_LEFTDOWN : MouseHookHelper.MOUSEEVENTF_LEFTUP,
                            X,
                            Y, 0, 0);
            }
            else
            {
                MouseHookHelper.mouse_event(Press ? MouseHookHelper.MOUSEEVENTF_RIGHTDOWN : MouseHookHelper.MOUSEEVENTF_RIGHTUP,
                    X,
                    Y, 0, 0);
            }
        }






        public void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public void SendKeystroke(byte VirtualKeyCode, byte ScanCode, bool KeyDown, bool ExtendedKey)//发送键盘事件
        {
            //INPUT input = new INPUT();

            //input.type = INPUT_KEYBOARD;
            //input.ki.wVk = VirtualKeyCode;
            //input.ki.wScan = ScanCode;
            //input.ki.dwExtraInfo = 0;
            //input.ki.time = 0;

            //if (!KeyDown)
            //{
            //    input.ki.dwFlags |= KEYEVENTF_KEYUP;
            //}

            //if (ExtendedKey)
            //{
            //    input.ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
            //}

            //SendInput(1, ref input, Marshal.SizeOf(input));
            if (!KeyDown)
            {
                // input.ki.dwFlags |= KEYEVENTF_KEYUP;
                KeyBoard.keyPress(VirtualKeyCode);
            }

            if (ExtendedKey)
            {
                //input.ki.dwFlags |= KEYEVENTF_EXTENDEDKEY;
                KeyBoard.keyPress(VirtualKeyCode);
            }
        }
        #endregion

        #region 获得屏幕截图
        private Bitmap GetDesktopBitmap()
        {
            Size DesktopBitmapSize = GetDesktopBitmapSize();
            Graphics Graphic = Graphics.FromHwnd(GetDesktopWindow());//从窗口的指定句柄创建新的 Graphics 对象
            Bitmap MemImage = new Bitmap(DesktopBitmapSize.Width, DesktopBitmapSize.Height, Graphic);//生成图像
            Graphics MemGraphic = Graphics.FromImage(MemImage);//从指定的 Image 对象创建新 Graphics 对象
            IntPtr dc1 = Graphic.GetHdc();//获取与此 Graphics 对象关联的设备上下文的句柄
            IntPtr dc2 = MemGraphic.GetHdc();
            BitBlt(dc2, 0, 0, DesktopBitmapSize.Width, DesktopBitmapSize.Height, dc1, 0, 0, 0xCC0020);
            Graphic.ReleaseHdc(dc1);//释放通过以前对此 Graphics 对象的 GetHdc 方法的调用获得的设备上下文句柄
            MemGraphic.ReleaseHdc(dc2);
            Graphic.Dispose();
            MemGraphic.Dispose();
            return MemImage;
        }
        #endregion

        #region 判断两个图的二进制数组是否相同
        private static bool BitmapsAreEqual(ref byte[] a, ref byte[] b)
        {
            bool Result = (a != null && b != null && a.Length == b.Length);

            if (Result)
            {
                for (int i = 0; Result && i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        Result = false;
                    }
                }
            }
            return Result;
        }
        #endregion

        #region 获得图像二进制的数组
        public byte[] GetDesktopBitmapBytes()
        {
            Bitmap CurrentBitmap = GetDesktopBitmap();
            MemoryStream MS = new MemoryStream();
            CurrentBitmap.Save(MS, ImageFormat.Jpeg);//将图片写入流
            CurrentBitmap.Dispose();
            MS.Seek(0, SeekOrigin.Begin);
            byte[] CurrentBitmapBytes = new byte[MS.Length];
            int NumBytesToRead = (int)MS.Length;
            int NumBytesRead = 0;

            while (NumBytesToRead > 0)
            {
                int n = MS.Read(CurrentBitmapBytes, NumBytesRead, NumBytesToRead);
                if (n == 0)
                {
                    break;
                }
                NumBytesRead += n;
                NumBytesToRead -= n;
            }
            MS.Close();

            byte[] Result = new byte[0];

            if (!BitmapsAreEqual(ref CurrentBitmapBytes, ref PreviousBitmapBytes))
            {
                Result = CurrentBitmapBytes;
                PreviousBitmapBytes = CurrentBitmapBytes;
            }
            return Result;
        }
        #endregion
    }


    public class MouseHookHelper
    {

        #region 根据句柄寻找窗体并发送消息

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //参数1:指的是类名。参数2，指的是窗口的标题名。两者至少要知道1个
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        #endregion

        #region 获取窗体位置
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }
        #endregion

        #region 设置窗体显示形式

        public enum nCmdShow : uint
        {
            SW_NONE,//初始值
            SW_FORCEMINIMIZE,//：在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
            SW_MIOE,//：隐藏窗口并激活其他窗口。
            SW_MAXIMIZE,//：最大化指定的窗口。
            SW_MINIMIZE,//：最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
            SW_RESTORE,//：激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
            SW_SHOW,//：在窗口原来的位置以原来的尺寸激活和显示窗口。
            SW_SHOWDEFAULT,//：依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
            SW_SHOWMAXIMIZED,//：激活窗口并将其最大化。
            SW_SHOWMINIMIZED,//：激活窗口并将其最小化。
            SW_SHOWMINNOACTIVATE,//：窗口最小化，激活窗口仍然维持激活状态。
            SW_SHOWNA,//：以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
            SW_SHOWNOACTIVATE,//：以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
            SW_SHOWNOMAL,//：激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
        }

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion

        #region 控制鼠标移动

        //移动鼠标 
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        #endregion

        #region 获取坐标钩子

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //安装钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //调用下一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

    }

    public class KeyBoard
    {
        public const byte vKeyLButton = 0x1;    // 鼠标左键
        public const byte vKeyRButton = 0x2;    // 鼠标右键
        public const byte vKeyCancel = 0x3;     // CANCEL 键
        public const byte vKeyMButton = 0x4;    // 鼠标中键
        public const byte vKeyBack = 0x8;       // BACKSPACE 键
        public const byte vKeyTab = 0x9;        // TAB 键
        public const byte vKeyClear = 0xC;      // CLEAR 键
        public const byte vKeyReturn = 0xD;     // ENTER 键
        public const byte vKeyShift = 0x10;     // SHIFT 键
        public const byte vKeyControl = 0x11;   // CTRL 键
        public const byte vKeyAlt = 18;         // Alt 键  (键码18)
        public const byte vKeyMenu = 0x12;      // MENU 键
        public const byte vKeyPause = 0x13;     // PAUSE 键
        public const byte vKeyCapital = 0x14;   // CAPS LOCK 键
        public const byte vKeyEscape = 0x1B;    // ESC 键
        public const byte vKeySpace = 0x20;     // SPACEBAR 键
        public const byte vKeyPageUp = 0x21;    // PAGE UP 键
        public const byte vKeyEnd = 0x23;       // End 键
        public const byte vKeyHome = 0x24;      // HOME 键
        public const byte vKeyLeft = 0x25;      // LEFT ARROW 键
        public const byte vKeyUp = 0x26;        // UP ARROW 键
        public const byte vKeyRight = 0x27;     // RIGHT ARROW 键
        public const byte vKeyDown = 0x28;      // DOWN ARROW 键
        public const byte vKeySelect = 0x29;    // Select 键
        public const byte vKeyPrint = 0x2A;     // PRINT SCREEN 键
        public const byte vKeyExecute = 0x2B;   // EXECUTE 键
        public const byte vKeySnapshot = 0x2C;  // SNAPSHOT 键
        public const byte vKeyDelete = 0x2E;    // Delete 键
        public const byte vKeyHelp = 0x2F;      // HELP 键
        public const byte vKeyNumlock = 0x90;   // NUM LOCK 键

        //常用键 字母键A到Z
        public const byte vKeyA = 65;
        public const byte vKeyB = 66;
        public const byte vKeyC = 67;
        public const byte vKeyD = 68;
        public const byte vKeyE = 69;
        public const byte vKeyF = 70;
        public const byte vKeyG = 71;
        public const byte vKeyH = 72;
        public const byte vKeyI = 73;
        public const byte vKeyJ = 74;
        public const byte vKeyK = 75;
        public const byte vKeyL = 76;
        public const byte vKeyM = 77;
        public const byte vKeyN = 78;
        public const byte vKeyO = 79;
        public const byte vKeyP = 80;
        public const byte vKeyQ = 81;
        public const byte vKeyR = 82;
        public const byte vKeyS = 83;
        public const byte vKeyT = 84;
        public const byte vKeyU = 85;
        public const byte vKeyV = 86;
        public const byte vKeyW = 87;
        public const byte vKeyX = 88;
        public const byte vKeyY = 89;
        public const byte vKeyZ = 90;

        //数字键盘0到9
        public const byte vKey0 = 48;    // 0 键
        public const byte vKey1 = 49;    // 1 键
        public const byte vKey2 = 50;    // 2 键
        public const byte vKey3 = 51;    // 3 键
        public const byte vKey4 = 52;    // 4 键
        public const byte vKey5 = 53;    // 5 键
        public const byte vKey6 = 54;    // 6 键
        public const byte vKey7 = 55;    // 7 键
        public const byte vKey8 = 56;    // 8 键
        public const byte vKey9 = 57;    // 9 键


        public const byte vKeyNumpad0 = 0x60;    //0 键
        public const byte vKeyNumpad1 = 0x61;    //1 键
        public const byte vKeyNumpad2 = 0x62;    //2 键
        public const byte vKeyNumpad3 = 0x63;    //3 键
        public const byte vKeyNumpad4 = 0x64;    //4 键
        public const byte vKeyNumpad5 = 0x65;    //5 键
        public const byte vKeyNumpad6 = 0x66;    //6 键
        public const byte vKeyNumpad7 = 0x67;    //7 键
        public const byte vKeyNumpad8 = 0x68;    //8 键
        public const byte vKeyNumpad9 = 0x69;    //9 键
        public const byte vKeyMultiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
        public const byte vKeyAdd = 0x6B;        // PLUS SIGN(+) 键
        public const byte vKeySeparator = 0x6C;  // ENTER 键
        public const byte vKeySubtract = 0x6D;   // MINUS SIGN(-) 键
        public const byte vKeyDecimal = 0x6E;    // DECIMAL POINT(.) 键
        public const byte vKeyDivide = 0x6F;     // DIVISION SIGN(/) 键


        //F1到F12按键
        public const byte vKeyF1 = 0x70;   //F1 键
        public const byte vKeyF2 = 0x71;   //F2 键
        public const byte vKeyF3 = 0x72;   //F3 键
        public const byte vKeyF4 = 0x73;   //F4 键
        public const byte vKeyF5 = 0x74;   //F5 键
        public const byte vKeyF6 = 0x75;   //F6 键
        public const byte vKeyF7 = 0x76;   //F7 键
        public const byte vKeyF8 = 0x77;   //F8 键
        public const byte vKeyF9 = 0x78;   //F9 键
        public const byte vKeyF10 = 0x79;  //F10 键
        public const byte vKeyF11 = 0x7A;  //F11 键
        public const byte vKeyF12 = 0x7B;  //F12 键

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void keyPress(byte keyName)
        {
            KeyBoard.keybd_event(keyName, 0, 0, 0);
            KeyBoard.keybd_event(keyName, 0, 2, 0);
        }
    }

}
