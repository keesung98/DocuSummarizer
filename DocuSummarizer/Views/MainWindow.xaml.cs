using DocuSummarizer.UserControls;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DocuSummarizer.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Title 이름 추가
            var TitleBar = new CustomTitleBar("PDF 요약 AI");
            TitleBarGrid.Children.Add(TitleBar);
        }
    }
}
