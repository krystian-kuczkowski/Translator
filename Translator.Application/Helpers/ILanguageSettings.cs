using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Domain.Enum;

namespace Translator.Application.Helpers
{
    public interface ILanguageSettings
    {
        Language SourceLanguage { get; set; }
        Language TargetLanguage { get; set; }
    }
}
