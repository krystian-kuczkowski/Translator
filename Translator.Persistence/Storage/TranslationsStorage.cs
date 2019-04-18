using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Domain;
using Translator.Application.Helpers;
using Translator.Application.Storage;

namespace Translator.Persistence.Storage
{
    public class TranslationsStorage : ITranslationsStorage
    {
        private readonly IStorageSettings _storageSettings;

        public TranslationsStorage(IStorageSettings storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public void AddFile(string fileName)
        {
            fileName += ".txt";

            var path = Path.Combine(_storageSettings.DirectoryName, fileName);

            if (File.Exists(path))
                return;

            File.Create(path);
        }

        public void AppendTranslationData(TranslationData translationData)
        {
            var path = Path.Combine(_storageSettings.DirectoryName, _storageSettings.CurrentFileName = ".txt");

            File.AppendAllLines(_storageSettings.CurrentFileName,
                new string[] { translationData.ToString() });
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(_storageSettings.DirectoryName)
                .Select(p => Path.GetFileNameWithoutExtension(p));
        }
    }
}
