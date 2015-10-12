using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public SupportedLanguagePairsResult SupportedLanguagePairs()
        {
            var r = new SupportedLanguagePairsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/discover/language_pairs?public_key={0}", KeyPublic);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<SupportedLanguagePairsResult>(json);
                }
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
        [JsonProperty(PropertyName = "code")]
        public string Code;
    }
    public struct SupportedLanguagePairsTargets
    {
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "code")]
        public string Code;
        [JsonProperty(PropertyName = "availability")]
        public string Availability;
    }
}
