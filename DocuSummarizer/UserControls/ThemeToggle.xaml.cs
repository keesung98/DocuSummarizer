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
    /// ThemeToggle.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ThemeToggle : UserControl
    {
        public ThemeToggle()
        {
            InitializeComponent();
        }

        private void LightRadio_Click(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Light");
        }

        private void DarkRadio_Click(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Dark");
        }

        private void SwitchTheme(string theme)
        {
            string themeUri = theme switch
            {
                "Dark" => "pack://application:,,,/Styles/Colors.Dark.xaml",
                "Light" => "pack://application:,,,/Styles/Colors.Light.xaml",
                _ => null
            };

            if (themeUri == null) return;

            var newThemeDict = new ResourceDictionary { Source = new Uri(themeUri) };

            // 기존 테마 딕셔너리 제거 후 교체
            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Colors."));

            if (oldTheme != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldTheme);

            Application.Current.Resources.MergedDictionaries.Add(newThemeDict);
        }

    }
}
