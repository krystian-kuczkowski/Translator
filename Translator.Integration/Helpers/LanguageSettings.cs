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
        public Language SourceLanguage
        {
            get {
                Enum.TryParse(GetFromConfig("source"), out Language sourceLanguage);

                return sourceLanguage;
            }
            set {
                SaveInConfig("source", value.ToString());
            }
        }

        public Language TargetLanguage
        {
            get {
                Enum.TryParse(GetFromConfig("target"), out Language targetLanguage);

                return targetLanguage;
            }
            set {
                SaveInConfig("target", value.ToString());
            }
        }

        public LanguageSettings()
        {
            
        }
    }
}
