using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetResourceResult GetResource(string resourceUuid, int projectId=0, string fetch="")
        {
            var r = new GetResourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/resources/" + resourceUuid + "?secret_key={0}&public_key={1}&project_id={2}&fetch={3}", KeySecret, KeyPublic, projectId, fetch);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetResourceResult>(json);
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

    public struct GetResourceResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetResourceResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetResourceResultType
    {
        [JsonProperty(PropertyName = "type")]
        public string Type;
        [JsonProperty(PropertyName = "length")]
        public Int64 Length;
        [JsonProperty(PropertyName = "file_name")]
        public string FileName;
        [JsonProperty(PropertyName = "file_mime")]
        public string FileMime;
        [JsonProperty(PropertyName = "download_url")]
        public string DownloadUrl;
        [JsonProperty(PropertyName = "content")]
        public string Content;
    }
}
