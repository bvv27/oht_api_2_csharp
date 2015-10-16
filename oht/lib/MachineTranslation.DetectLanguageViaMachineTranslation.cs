using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IDetectLanguageViaMachineTranslationProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceContent);
    }
    public class DetectLanguageViaMachineTranslationProvider : IDetectLanguageViaMachineTranslationProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceContent)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/mt/detect/text?public_key={0}&secret_key={1}&source_content={2}", publicKey, secretKey, sourceContent);
                return client.DownloadString(web);
            }
        }
    }
    partial class Ohtapi
    {
        public IDetectLanguageViaMachineTranslationProvider DetectLanguageViaMachineTranslationProvider;
        /// <summary>
        /// Detect language via machine translation
        /// </summary>
        /// <param name="sourceContent">Text for translation</param>
        /// <returns></returns>
        public DetectLanguageViaMachineTranslationResult DetectLanguageViaMachineTranslation(string sourceContent)
        {
            var r = new DetectLanguageViaMachineTranslationResult();
            try
            {
                if (DetectLanguageViaMachineTranslationProvider == null)
                    DetectLanguageViaMachineTranslationProvider = new DetectLanguageViaMachineTranslationProvider();
                var json = DetectLanguageViaMachineTranslationProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceContent);
                r = JsonConvert.DeserializeObject<DetectLanguageViaMachineTranslationResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }
    }


    public struct DetectLanguageViaMachineTranslationResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public DetectLanguageViaMachineTranslationResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg + " " + Result.Language : Status.Code + " " + Status.Msg;
        }
    }
    public struct DetectLanguageViaMachineTranslationResultType
    {
        /// <summary>
        /// See Language Codes
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language;
    }

}
