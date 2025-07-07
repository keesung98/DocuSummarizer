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

namespace DocuSummarizer.UserControls
{
    /// <summary>
    /// CustomTitleBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomTitleBar : UserControl
    {
        public CustomTitleBar(string title)
        {
            InitializeComponent();

            TitleName.Text = title;
            // 버튼 이벤트
            MinimizeButton.Click += (s, e) =>
            {
                Window.GetWindow(this).WindowState = WindowState.Minimized;
            };

            MaximizeRestoreButton.Click += (s, e) =>
            {
                ToggleMaximizeRestore();
            };

            CloseButton.Click += (s, e) =>
            {
                Window.GetWindow(this).Close();
            };

            // 드래그 이동 및 더블클릭 최대화
            this.MouseLeftButtonDown += (s, e) =>
            {
                var win = Window.GetWindow(this);
                if (e.ClickCount == 2)
                {
                    ToggleMaximizeRestore();
                }
                else
                {
                    win.DragMove();
                }
            };
        }

        private void ToggleMaximizeRestore()
        {
            var win = Window.GetWindow(this);
            if (win.WindowState == WindowState.Maximized)
            {
                win.WindowState = WindowState.Normal;
                MaximizeRestoreButton.Content = "☐";
            }
            else
            {
                win.WindowState = WindowState.Maximized;
                MaximizeRestoreButton.Content = "🗗"; // 복원 아이콘처럼 보이는 글자
            }
        }
    }
}
