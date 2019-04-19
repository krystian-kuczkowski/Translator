using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Helpers;
using System.Configuration;

namespace Translator.Persistence.Helpers
{
    public class StorageSettings : BaseSettings, IStorageSettings
    {
        public string DirectoryName {
            get {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Translations");
            }
        }

        private string _currentFileName;

        public string CurrentFileName {
            get {
                return _currentFileName;
            }
            set {
                if (!GetFileNames().Contains(value))
                    throw new ApplicationException("No file with given name in the directory.");

                _currentFileName = value;

                SaveInConfig("file", _currentFileName);
            }
        }

        public StorageSettings()
        {
            var file = GetFromConfig("file");

            _currentFileName = String.IsNullOrWhiteSpace(file) ? null : file;
        }

        private IEnumerable<string> GetFileNames()
        {
            var files = Directory.GetFiles(DirectoryName)
                .Select(p => Path.GetFileNameWithoutExtension(p));

            return files;
        }
    }
}
