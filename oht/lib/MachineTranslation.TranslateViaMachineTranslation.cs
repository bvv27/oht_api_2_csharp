using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ITranslateViaMachineTranslationProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string targetLanguage, string sourceContent);
    }
    public class TranslateViaMachineTranslationProvider : ITranslateViaMachineTranslationProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string targetLanguage, string sourceContent)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/mt/translate/text?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&source_content={4}"
                    , publicKey, secretKey, sourceLanguage, targetLanguage, sourceContent);
                return client.DownloadString(web);
            }
        }
    }
    partial class Ohtapi
    {
        public ITranslateViaMachineTranslationProvider TranslateViaMachineTranslationProvider;
        /// <summary>
        /// Translate via machine translation
        /// </summary>
        /// <param name="sourceLanguage">See Language Codes</param>
        /// <param name="targetLanguage">See Language Codes</param>
        /// <param name="sourceContent">Text for translation</param>
        /// <returns></returns>
        public TranslateViaMachineTranslationResult TranslateViaMachineTranslation(string sourceLanguage, string targetLanguage, string sourceContent)
        {
            var r = new TranslateViaMachineTranslationResult();
            try
            {
                if (TranslateViaMachineTranslationProvider == null)
                    TranslateViaMachineTranslationProvider = new TranslateViaMachineTranslationProvider();
                var json = TranslateViaMachineTranslationProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceLanguage, targetLanguage, sourceContent);
                r = JsonConvert.DeserializeObject<TranslateViaMachineTranslationResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }
    }


    public struct TranslateViaMachineTranslationResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public TranslateViaMachineTranslationResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg + (Status.Code == 0 ? " " + Result.TranslatedText : "") : Status.Code + " " + Status.Msg;
        }
    }
    public struct TranslateViaMachineTranslationResultType
    {
        /// <summary>
        /// The translated content in the original format
        /// </summary>
        [JsonProperty(PropertyName = "TranslatedText")]
        public string TranslatedText;     
    }

}
