using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Translator.Application.Helpers;
using Translator.Application.Storage;
using Translator.Desktop.Commands;

namespace Translator.Desktop.ViewModels
{
    public class FilesViewModel : BaseViewModel
    {
        private readonly ITranslationsStorage _translationsStorage;
        private readonly IStorageSettings _storageSettings;

        public IEnumerable<string> Files {
            get {
                return _translationsStorage.GetFiles();
            }
        }

        public string CurrentFile {
            get {
                if (_storageSettings.CurrentFileName != null)
                    return _storageSettings.CurrentFileName;

                return "No file set";
            }
        }

        public string ListBoxTitle {
            get {
                if (_translationsStorage.GetFiles().Count() == 0)
                    return "No files";

                return "Your files";
            }
        }

        public string SelectedFile { get; set; }

        public ICommand AddFileCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ChangeFileCommand { get; private set; }

        public event EventHandler AddFileButtonClicked;

        public FilesViewModel(ITranslationsStorage translationsStorage, IStorageSettings storageSettings)
        {
            _translationsStorage = translationsStorage;
            _storageSettings = storageSettings;

            AddFileCommand = new RelayCommand(param => {
                OnAddFileButtonClicked();
            });

            RefreshCommand = new RelayCommand(param => {
                OnPropertyChanged(nameof(Files));
                OnPropertyChanged(nameof(ListBoxTitle));
                OnPropertyChanged(nameof(CurrentFile));
            });

            ChangeFileCommand = new RelayCommand(param => {
                _translationsStorage.SetFile(SelectedFile);
                OnPropertyChanged(nameof(CurrentFile));
            }, param => !String.IsNullOrWhiteSpace(SelectedFile) && !SelectedFile.Equals(CurrentFile));
        }

        protected void OnAddFileButtonClicked()
        {
            AddFileButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
