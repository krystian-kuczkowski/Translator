using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Application.Services
{
    public interface ITranslationService
    {
        string GetTranslation(string source);
    }
}
