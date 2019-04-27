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
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Translations");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        public string CurrentFileName {
            get {
                var file = GetFromConfig("file");

                if (String.IsNullOrWhiteSpace(file))
                    file = null;

                if (!GetFileNames().Contains(file))
                    file = null;

                return file;
            }
            set {
                if (!GetFileNames().Contains(value))
                    return;

                SaveInConfig("file", value);
            }
        }

        public StorageSettings()
        {
            
        }

        private IEnumerable<string> GetFileNames()
        {
            var files = Directory.GetFiles(DirectoryName)
                .Select(p => Path.GetFileNameWithoutExtension(p));

            return files;
        }
    }
}
