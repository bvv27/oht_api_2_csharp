using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ISupportedLanguagePairsProvider
    {
        string Get(string url, WebProxy proxy, string publicKey);
    }
    public class SupportedLanguagePairsProvider : ISupportedLanguagePairsProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/discover/language_pairs?public_key={0}", publicKey);
                return client.DownloadString(web);
            }
        }
    }
    partial class Ohtapi
    {
        public ISupportedLanguagePairsProvider SupportedLanguagePairsProvider;
        /// <summary>
        /// Discover which language pairs are supported by OHT
        /// </summary>
        /// <returns></returns>
        public SupportedLanguagePairsResult SupportedLanguagePairs()
        {
            var r = new SupportedLanguagePairsResult();
            try
            {
                if (SupportedLanguagePairsProvider == null)
                    SupportedLanguagePairsProvider = new SupportedLanguagePairsProvider();
                var json = SupportedLanguagePairsProvider.Get(Url, _proxy, KeyPublic);
                r = JsonConvert.DeserializeObject<SupportedLanguagePairsResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }
    }


    public struct SupportedLanguagePairsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetQuoteResultType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct SupportedLanguagePairsResultType
    {
        [JsonProperty(PropertyName = "source")]
        public SupportedLanguagePairsSource[] Source;
        [JsonProperty(PropertyName = "targets")]
        public SupportedLanguagePairsTargets[] Targets;
    }
    public struct SupportedLanguagePairsSource
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        /// <summary>
        /// ●	code - See Language Codes
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code;
    }
    public struct SupportedLanguagePairsTargets
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        /// <summary>
        /// ●	code - See Language Codes
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public string Code;
        /// <summary>
        ///availability - high | medium | low Details:	high = work is expected to start within an hour on business hours medium = work is expected to start within one day	low = work is expected to start within a week
        /// </summary>
        [JsonProperty(PropertyName = "availability")]
        public string Availability;
    }
}
