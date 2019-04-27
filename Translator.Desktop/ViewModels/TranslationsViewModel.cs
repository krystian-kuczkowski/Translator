using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Translator.Application.Business;
using Translator.Application.Helpers;
using Translator.Desktop.Commands;

namespace Translator.Desktop.ViewModels
{
    public class TranslationsViewModel : BaseViewModel
    {
        private readonly TranslationsProvider _translationsProvider;
        private readonly ILanguageSettings _languageSettings;

        public string SourceLanguage {
            get {
                return _languageSettings.SourceLanguage.ToString();
            }
        }

        public string TargetLanguage {
            get {
                return _languageSettings.TargetLanguage.ToString();
            }
        }

        public string SourceText { get; set; }
        public string TargetText { get; set; }

        public ICommand TranslateCommand { get; private set; }

        public TranslationsViewModel(TranslationsProvider translationsProvider, ILanguageSettings languageSettings)
        {
            _translationsProvider = translationsProvider;
            _languageSettings = languageSettings;

            TranslateCommand = new RelayCommand(param => {
                Translate();
            }, param => !String.IsNullOrWhiteSpace(SourceText));
        }

        private void Translate()
        {
            TargetText = _translationsProvider.TranslateAsync(SourceText).GetAwaiter().GetResult();
            OnPropertyChanged(nameof(TargetText));
        }
    }
}
