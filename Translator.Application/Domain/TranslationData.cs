using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Application.Domain
{
    public class TranslationData
    {
        public string Source { get; set; }
        public string Target { get; set; }

        public TranslationData()
        {
            Source = string.Empty;
            Target = string.Empty;
        }

        public override string ToString()
        {
            return $"{Source} - {Target}";
        }
    }
}
