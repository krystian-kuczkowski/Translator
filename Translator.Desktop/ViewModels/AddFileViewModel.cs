using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Translator.Application.Storage;
using Translator.Desktop.Commands;

namespace Translator.Desktop.ViewModels
{
    public class AddFileViewModel : BaseViewModel
    {
        private readonly ITranslationsStorage _translationsStorage;

        public string FileName { get; set; }

        public ICommand AddFileCommand { get; private set; }

        public event EventHandler FileAdded;

        public AddFileViewModel(ITranslationsStorage translationsStorage)
        {
            _translationsStorage = translationsStorage;

            AddFileCommand = new RelayCommand(param => {
                _translationsStorage.AddFile(FileName);
                FileName = String.Empty;
                OnFileAdded();
            }, param => !String.IsNullOrWhiteSpace(FileName) && !_translationsStorage.GetFiles().Contains(FileName));
        }

        protected void OnFileAdded()
        {
            FileAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}
