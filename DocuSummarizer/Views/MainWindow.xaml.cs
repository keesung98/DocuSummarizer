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
        private string _imagePath = null;
        private BitmapImage _loadedImage = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        // 이미지 열기 버튼 클릭
        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp"
            };

            if (ofd.ShowDialog() == true)
            {
                LoadImage(ofd.FileName);
            }
        }

        // 이미지 드래그 앤 드롭
        private void ImageDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && File.Exists(files[0]))
                {
                    LoadImage(files[0]);
                }
            }
        }

        // 이미지 로딩 및 미리보기
        private void LoadImage(string path)
        {
            try
            {
                _imagePath = path;
                _loadedImage = new BitmapImage(new Uri(path));

                PreviewImage.Source = _loadedImage;
                OriginalTextBox.Clear();
                SummaryTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("이미지 로딩 실패: " + ex.Message);
            }
        }

        // OCR 실행
        private void RunOCR_Click(object sender, RoutedEventArgs e)
        {
            if (_imagePath == null)
            {
                MessageBox.Show("이미지를 먼저 선택해주세요.");
                return;
            }

            // TODO: 전처리 적용
            bool grayscale = (FindName("Grayscale") as CheckBox)?.IsChecked ?? false;
            bool blur = (FindName("Blur") as CheckBox)?.IsChecked ?? false;
            bool deskew = (FindName("Deskew") as CheckBox)?.IsChecked ?? false;

            // TODO: OpenCvSharp 전처리 적용
            // TODO: Tesseract OCR 호출

            // 현재는 임시 텍스트
            OriginalTextBox.Text = "이 영역은 OCR 결과 텍스트가 출력되는 곳입니다.\n(향후 Tesseract 적용)";
        }

        // 요약 실행
        private void RunSummary_Click(object sender, RoutedEventArgs e)
        {
            var originalText = OriginalTextBox.Text;
            if (string.IsNullOrWhiteSpace(originalText))
            {
                MessageBox.Show("OCR 결과가 비어 있습니다.");
                return;
            }

            // TODO: ONNX 요약 모델 연동
            SummaryTextBox.Text = "이 영역은 요약된 결과가 표시되는 곳입니다.\n(향후 HuggingFace 모델 적용)";
        }

        // 요약 복사
        private void CopySummary_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SummaryTextBox.Text))
            {
                Clipboard.SetText(SummaryTextBox.Text);
                MessageBox.Show("요약 텍스트가 클립보드에 복사되었습니다.");
            }
        }

        // 요약 저장
        private void SaveSummary_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SummaryTextBox.Text))
            {
                MessageBox.Show("요약 결과가 없습니다.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Text File (*.txt)|*.txt|Markdown (*.md)|*.md",
                FileName = "summary"
            };

            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, SummaryTextBox.Text, Encoding.UTF8);
                MessageBox.Show("파일이 저장되었습니다.");
            }
        }
    }
}
