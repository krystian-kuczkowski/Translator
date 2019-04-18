using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.Application.Domain;
using Translator.Application.Services;
using Translator.Application.Storage;

namespace Translator.Application.Business
{
    public class TranslationsProvider
    {
        private readonly ITranslationService _translationService;
        private readonly ITranslationsStorage _translationsStorage;

        public TranslationsProvider(ITranslationService translationService, ITranslationsStorage translationsStorage)
        {
            _translationService = translationService;
            _translationsStorage = translationsStorage;
        }

        private string Translate(string source)
        {
            var target = _translationService.GetTranslationAsync(source).GetAwaiter().GetResult();

            var data = new TranslationData
            {
                Source = source,
                Target = target
            };

            _translationsStorage.AppendTranslationData(data);

            return target;
        }

        public Task<string> TranslateAsync(string source)
        {
            return Task.Run(() => Translate(source));
        }
    }
}
