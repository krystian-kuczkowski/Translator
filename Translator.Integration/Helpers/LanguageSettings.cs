using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Domain.Enum;
using Translator.Application.Helpers;

namespace Translator.Integration.Helpers
{
    public class LanguageSettings : BaseSettings, ILanguageSettings
    {
        private Language _sourceLanguage;

        public Language SourceLanguage
        {
            get {
                return _sourceLanguage;
            }
            set {
                _sourceLanguage = value;

                SaveInConfig("source", _sourceLanguage.ToString());
            }
        }

        private Language _targetLanguage;

        public Language TargetLanguage
        {
            get {
                return _targetLanguage;
            }
            set {
                _targetLanguage = value;

                SaveInConfig("target", _targetLanguage.ToString());
            }
        }

        public LanguageSettings()
        {
            Enum.TryParse(GetFromConfig("target"), out _targetLanguage);
            Enum.TryParse(GetFromConfig("source"), out _sourceLanguage);
        }
    }
}
