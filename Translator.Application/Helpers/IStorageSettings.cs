using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Application.Helpers
{
    public interface IStorageSettings
    {
        string DirectoryName { get; }
        string CurrentFileName { get; set; }
    }
}
