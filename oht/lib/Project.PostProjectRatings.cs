using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public PostProjectRatingsResult PostProjectRatings(int projectId, string type,int rate, string remarks="")
        {
            var r = new PostProjectRatingsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + projectId + "/rating?public_key={0}&secret_key={1}&type={2}&rate={3}&remarks={4}"
                        ,KeyPublic, KeySecret, type, rate, remarks);
                    
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<PostProjectRatingsResult>(json);
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


    public struct PostProjectRatingsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }

}
