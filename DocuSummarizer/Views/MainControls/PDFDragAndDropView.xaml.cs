using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace DocuSummarizer.Views.MainControls
{
    /// <summary>
    /// PDFDragAndDropView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PDFDragAndDropView : UserControl
    {
        public PDFDragAndDropView()
        {
            InitializeComponent();
        }

        // PDF 파일이 선택되었을 때 발생하는 이벤트
        public event EventHandler<string> PDFFileSelected;

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Any(f => f.ToLower().EndsWith(".pdf")))
                {
                    e.Effects = DragDropEffects.Copy;
                    ShowDragOverState();
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Any(f => f.ToLower().EndsWith(".pdf")))
                {
                    e.Effects = DragDropEffects.Copy;
                    ShowDragOverState();
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            // 마우스가 실제로 컨트롤 영역을 벗어났는지 확인
            Point position = e.GetPosition(this);
            if (position.X < 0 || position.Y < 0 ||
                position.X > this.ActualWidth || position.Y > this.ActualHeight)
            {
                HideDragOverState();
            }
            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            HideDragOverState();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var pdfFile = files?.FirstOrDefault(f => f.ToLower().EndsWith(".pdf"));

                if (pdfFile != null)
                {
                    ProcessSelectedFile(pdfFile);
                }
                else
                {
                    MessageBox.Show("PDF 파일만 지원됩니다.", "파일 형식 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            e.Handled = true;
        }

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF 파일 (*.pdf)|*.pdf",
                Title = "PDF 파일 선택",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProcessSelectedFile(openFileDialog.FileName);
            }
        }

        private void ProcessSelectedFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("파일을 찾을 수 없습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var fileInfo = new FileInfo(filePath);

                // 파일 정보 표시
                FileNameText.Text = fileInfo.Name;
                FileSizeText.Text = $"{FormatFileSize(fileInfo.Length)} • {fileInfo.LastWriteTime:yyyy-MM-dd}";
                FileInfoPanel.Visibility = Visibility.Visible;

                // 선택된 파일 경로를 ViewModel 또는 이벤트를 통해 전달
                PDFFileSelected?.Invoke(this, filePath);

                // DataContext가 PDFDragAndDropViewModel인 경우 파일 경로 설정
                if (DataContext is ViewModels.MainControls.PDFDragAndDropViewModel viewModel)
                {
                    // ViewModel에 파일 경로 설정하는 프로퍼티나 메서드가 있다면 여기서 호출
                    // 예: viewModel.SetSelectedFile(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 처리 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowDragOverState()
        {
            DragDropBorder.Tag = "DragOver";
            DragOverlay.Visibility = Visibility.Visible;
        }

        private void HideDragOverState()
        {
            DragDropBorder.Tag = null;
            DragOverlay.Visibility = Visibility.Collapsed;
        }

        private string FormatFileSize(long bytes)
        {
            const int scale = 1024;
            string[] orders = { "B", "KB", "MB", "GB", "TB" };

            if (bytes == 0)
                return "0 B";

            long max = (long)Math.Pow(scale, orders.Length - 1);
            foreach (string order in orders)
            {
                if (bytes > max)
                    return $"{decimal.Divide(bytes, max):##.#} {order}";
                max /= scale;
            }
            return "0 B";
        }
    }
}