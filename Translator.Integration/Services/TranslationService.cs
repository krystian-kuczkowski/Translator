using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Translator.Application.Helpers;
using Translator.Application.Services;

namespace Translator.Integration.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ILanguageSettings _languageSettings;
        private readonly HttpClient _httpClient;

        public TranslationService(ILanguageSettings languageSettings)
        {
            _languageSettings = languageSettings;
            _httpClient = new HttpClient();
        }

        public string GetTranslation(string source)
        {
            source = Regex.Replace(source, "[!?,.]", " ");

            var requestUrl = GetRequestUrl(source);

            var response = _httpClient.GetAsync(requestUrl).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
                return null;

            var body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return ExtractTranslation(body);
        }

        private string ExtractTranslation(string body)
        {
            body = body.Substring(body.IndexOf("\"") + 1);

            return body.Substring(0, body.IndexOf("\""));
        }

        private string GetRequestUrl(string source)
        {
            var sourceLang = _languageSettings.SourceLanguage.ToString().ToLower();
            var targetLang = _languageSettings.TargetLanguage.ToString().ToLower();

            return $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLang}&tl={targetLang}&dt=t&q={source}";
        }
    }


}
