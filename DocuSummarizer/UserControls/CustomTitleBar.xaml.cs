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
        // 이전 윈도우 상태 저장용 변수들
        private double _restoreLeft;
        private double _restoreTop;
        private double _restoreWidth;
        private double _restoreHeight;
        private bool _isMaximized = false;

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
                else if (!_isMaximized)
                {
                    win.DragMove();
                }
            };
        }

        private void ToggleMaximizeRestore()
        {
            var win = Window.GetWindow(this);

            if (_isMaximized)
            {
                // 이전 상태로 복원
                RestoreWindow();
                MaximizeRestoreButton.Content = "☐";
            }
            else
            {
                // 현재 상태 저장 후 최대화
                SaveCurrentState();
                MaximizeToWorkArea();
                MaximizeRestoreButton.Content = "🗗";
            }
        }

        /// <summary>
        /// 현재 윈도우 상태를 저장
        /// </summary>
        private void SaveCurrentState()
        {
            var window = Window.GetWindow(this);
            _restoreLeft = window.Left;
            _restoreTop = window.Top;
            _restoreWidth = window.Width;
            _restoreHeight = window.Height;
        }

        /// <summary>
        /// 윈도우를 작업 영역 내에서 최대화
        /// </summary>
        private void MaximizeToWorkArea()
        {
            var workArea = SystemParameters.WorkArea;
            var window = Window.GetWindow(this);

            // 윈도우를 작업 영역에 맞게 설정
            window.Left = workArea.Left;
            window.Top = workArea.Top;
            window.Width = workArea.Width;
            window.Height = workArea.Height;

            _isMaximized = true;
        }

        /// <summary>
        /// 윈도우를 이전 상태로 복원
        /// </summary>
        private void RestoreWindow()
        {
            var window = Window.GetWindow(this);

            window.Left = _restoreLeft;
            window.Top = _restoreTop;
            window.Width = _restoreWidth;
            window.Height = _restoreHeight;

            _isMaximized = false;
        }
    }
}