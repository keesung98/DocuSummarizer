using DocuSummarizer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuSummarizer.ViewModels.MainControls
{
    internal class PDFDragAndDropViewModel:ViewModelBase
    {
        private string _selectedFilePath;
        private string _fileName;
        private long _fileSize;
        private bool _isFileSelected;

        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                _selectedFilePath = value;
                OnPropertyChanged();
                IsFileSelected = !string.IsNullOrEmpty(value);
            }
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        public long FileSize
        {
            get => _fileSize;
            set
            {
                _fileSize = value;
                OnPropertyChanged();
            }
        }

        public bool IsFileSelected
        {
            get => _isFileSelected;
            set
            {
                _isFileSelected = value;
                OnPropertyChanged();
            }
        }

        public void SetSelectedFile(string filePath)
        {
            SelectedFilePath = filePath;

            if (!string.IsNullOrEmpty(filePath))
            {
                var fileInfo = new System.IO.FileInfo(filePath);
                FileName = fileInfo.Name;
                FileSize = fileInfo.Length;
            }
            else
            {
                FileName = null;
                FileSize = 0;
            }
        }

        public void ClearSelection()
        {
            SelectedFilePath = null;
            FileName = null;
            FileSize = 0;
            IsFileSelected = false;
        }
    }
}
