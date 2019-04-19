using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Domain;

namespace Translator.Application.Storage
{
    public interface ITranslationsStorage
    {
        void AddFile(string fileName);
        void SetFile(string fileName);
        void AppendTranslationData(TranslationData translationData);
        IEnumerable<string> GetFiles();
    }
}
