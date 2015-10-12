using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public RetrieveProjectRatingsResult RetrieveProjectRatings(string projectId)
        {
            var r = new RetrieveProjectRatingsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + projectId + "/rating?public_key={0}&secret_key={1}",KeyPublic, KeySecret);

                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<RetrieveProjectRatingsResult>(json);
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


    public struct RetrieveProjectRatingsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public RetrieveProjectRatingsType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct RetrieveProjectRatingsType
    {
        [JsonProperty(PropertyName = "type")]
        public string Type;
        [JsonProperty(PropertyName = "rate")]
        public int Rate;
        [JsonProperty(PropertyName = "remarks")]
        public string Remarks;
        [JsonProperty(PropertyName = "date")]
        public string Date;
    }


}
