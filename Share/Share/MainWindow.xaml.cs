using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Share
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private 
            bool block = false;
        public MainWindow()
        {
            InitializeComponent();
            initList();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBox setComboBox = new ComboBox(); 

        }

        private void LockButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage lkBitmapImage = new BitmapImage();
            lkBitmapImage.BeginInit();
            if(block)
            {
                lkBitmapImage.UriSource = new Uri("./Image/Unlocked.ico", UriKind.Relative);
                block = false;
            }
            else
            {
                lkBitmapImage.UriSource = new Uri("/Image/Locked.ico", UriKind.Relative);
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

        public void initList()
        {
            for (int i = 0; i < 30; i++)
            {
                olList.Items.Add(new { N = "1" + i, ID = "YxY", File = "text", Size = "1KB", Date = "2020"});
                sendList.Items.Add(new { N = "1" + i, File = "text", Size = "1KB", Date = "2020" });
            }
        }
    }
}
