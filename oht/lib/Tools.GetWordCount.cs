using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetWordCountResult GetWordCount(string resources)
        {
            var r = new GetWordCountResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/tools/wordcount?public_key={0}&secret_key={1}&resources={2}"
                        ,KeyPublic, KeySecret, resources);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetWordCountResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
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


    public struct GetWordCountResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetWordCountType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetWordCountType
    {
        [JsonProperty(PropertyName = "resources")]
        public GetWordCountResources[] Resources;
        [JsonProperty(PropertyName = "total")]
        public GetWordCountTotal Total;

    }

    public struct GetWordCountResources
    {
        [JsonProperty(PropertyName = "resource")]
        public string Resource;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
    }
    public struct GetWordCountTotal
    {
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
    }
}
