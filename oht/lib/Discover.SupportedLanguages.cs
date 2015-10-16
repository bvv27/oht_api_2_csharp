using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ISupportedLanguagesProvider
    {
        string Get(string url, WebProxy proxy, string publicKey);
    }
    public class SupportedLanguagesProvider : ISupportedLanguagesProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/discover/languages?public_key={0}", publicKey);
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public ISupportedLanguagesProvider SupportedLanguagesProvider;
        /// <summary>
        /// Discover which languages are supported by OHT
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public SupportedLanguagesResult SupportedLanguages(string publicKey)
        {
            var r = new SupportedLanguagesResult();
            try
            {

                if (SupportedLanguagesProvider == null)
                    SupportedLanguagesProvider = new SupportedLanguagesProvider();
                var json = SupportedLanguagesProvider.Get(Url, _proxy, KeyPublic);
                r = JsonConvert.DeserializeObject<SupportedLanguagesResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

        public SupportedLanguagesResult SupportedLanguages()
        {
            return SupportedLanguages(KeyPublic);
        }
    }


    public struct SupportedLanguagesResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public SupportedLanguagesResultType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct SupportedLanguagesResultType
    {
        /// <summary>
        /// for example, English ,French etc.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string LanguageName;
        /// <summary>
        /// See Language Codes
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string LanguageCode;


    }

}
