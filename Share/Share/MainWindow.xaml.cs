using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;



namespace Share
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private 
            bool block = false;
            string dataTime = "";
        private DispatcherTimer showtimer;

        public MainWindow()
        {
            InitializeComponent();
            this.Topmost = true;
            this.ShowInTaskbar = false;
            test.Opacity -= 0.1;
            window.Opacity -= 0.1;
            var hwnd = new WindowInteropHelper(this).Handle;
            WindowsServices.GetWindowextendedStyle(hwnd);

            showtimer = new System.Windows.Threading.DispatcherTimer();
            showtimer.Tick += new EventHandler(ShowTimer);
            showtimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            showtimer.Start();
            
        }



        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            //ComboBox setComboBox = new ComboBox();
            test.Opacity -= 0.1;
            window.Opacity -= 0.1;
            
        }


        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage lkBitmapImage = new BitmapImage();
            lkBitmapImage.BeginInit();
            if(block)
            {
                lkBitmapImage.UriSource = new Uri("./Image/Unlocked.ico", UriKind.Relative);
                test.IsHitTestVisible = true;
                base.OnSourceInitialized(e);
                var hwnd = new WindowInteropHelper(this).Handle;
                WindowsServices.SetWindowExTransparent(hwnd, block);
                block = false;
            }
            else
            {
                lkBitmapImage.UriSource = new Uri("/Image/Locked.ico", UriKind.Relative);
                test.IsHitTestVisible = false;

                test.IsHitTestVisible = true;
                

                block = true;
            }
            lkBitmapImage.EndInit();
            lkImage.Source = lkBitmapImage;
        }


        //鼠标拖拽窗体
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            
　　        if (e.LeftButton == MouseButtonState.Pressed)
　　        {
　　　　        if (!block)
 　　　　       {
　　　　　           this.DragMove();
　              }
            }
        }

        private new void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.LWin) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                MessageBox.Show("您按下了Control键");
            }
        }

        private void CanPenetrate()
        {
            

        }
        //文件拖入-上传共享
        private void SendList_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var str in files)
            {
                char[] arr = str.ToCharArray();
                Array.Reverse(arr);
                string result = new string(arr);
                result = result.Split(new char[] { '\\' }, 2)[0];
                arr = result.ToCharArray();
                Array.Reverse(arr);
                result = new string(arr);
                /*
                 * 拓展修复
                 */
                sendList.Items.Add(new { N = "1", File = result, Size = CountSize(GetFileSize(str)), Date = dataTime });
            }
        }
        //获取文件大小
        public static long GetFileSize(string sFullName)
        {
            long lSize = 0;
            if (File.Exists(sFullName))
                lSize = new FileInfo(sFullName).Length;
            return lSize;
        }
        //转换为通识大小
        public static string CountSize(long Size)
        {
            string m_strSize = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F2") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F2") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " G";
            return m_strSize;
        }

        public void ShowTimer(object sender, EventArgs e)
        {
            
            //获得年月日 
            dataTime += DateTime.Now.ToString("yyyy年MM月dd日");   //yyyy年MM月dd日 
            //this.TM.Text += " ";
            //获得时分秒
            //this.TM.Text += DateTime.Now.ToString("HH:mm:ss:ms");
            //获得星期
            //this.TM.Text = DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn"));
            //this.TM.Text += " ";
        }
    }

    //窗体透明类
    public static class WindowsServices
    {
        const int WS_EX_TRANSPARENT = 0x00000020;
        const int GWL_EXSTYLE = (-20);

        

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        public static int GetWindowextendedStyle(IntPtr hwnd)
        {
            return GetWindowLong(hwnd, GWL_EXSTYLE);
        }

        public static void SetWindowExTransparent(IntPtr hwnd, bool block)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            if (block)
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_TRANSPARENT);
            }
            else
            {
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle);
            }

        }

    }
    


}
