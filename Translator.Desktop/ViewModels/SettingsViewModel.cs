﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Translator.Application.Domain.Enum;
using Translator.Application.Helpers;
using Translator.Desktop.Commands;

namespace Translator.Desktop.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ILanguageSettings _languageSettings;
        private double _gridContentLength = 300;

        public IEnumerable<Language> Languages
        {
            get
            {
                return Enum.GetValues(typeof(Language)).Cast<Language>();
            }
        }

        public Language SourceLanguage { get; set; }
        public Language TargetLanguage { get; set; }

        public ICommand ConfirmCommand { get; private set; }
        public ICommand ViewLoadedCommand { get; private set; }

        public GridLength GridContentLength
        {
            get
            {
                return new GridLength(_gridContentLength);
            }
        }

        public SettingsViewModel(ILanguageSettings languageSettings)
        {
            _languageSettings = languageSettings;

            SourceLanguage = _languageSettings.SourceLanguage;
            TargetLanguage = _languageSettings.TargetLanguage;

            ConfirmCommand = new RelayCommand(param => {
                _languageSettings.SourceLanguage = SourceLanguage;
                _languageSettings.TargetLanguage = TargetLanguage;
            });

            ViewLoadedCommand = new RelayCommand(param => {
                SourceLanguage = _languageSettings.SourceLanguage;
                TargetLanguage = _languageSettings.TargetLanguage;
                OnPropertyChanged(nameof(SourceLanguage));
                OnPropertyChanged(nameof(TargetLanguage));
            });
        }
    }
}