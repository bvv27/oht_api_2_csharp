using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ISupportedExpertisesProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string sourceLanguage, string targetLanguage);
    }
    public class SupportedExpertisesProvider : ISupportedExpertisesProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string sourceLanguage, string targetLanguage)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/discover/expertise?public_key={0}&source_language={1}&target_language={2}"
                    , publicKey, sourceLanguage, targetLanguage);
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public ISupportedExpertisesProvider SupportedExpertisesProvider;
        public SupportedExpertisesResult SupportedExpertises(string sourceLanguage, string targetLanguage)
        {
            var r = new SupportedExpertisesResult();
            try
            {
                if (SupportedExpertisesProvider == null)
                    SupportedExpertisesProvider = new SupportedExpertisesProvider();
                var json = SupportedExpertisesProvider.Get(Url, _proxy, KeyPublic, sourceLanguage, targetLanguage);
                r = JsonConvert.DeserializeObject<SupportedExpertisesResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

    }


    public struct SupportedExpertisesResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public SupportedExpertisesResultType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct SupportedExpertisesResultType
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "code")]
        public string Code;
    }

}
