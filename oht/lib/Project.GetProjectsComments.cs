using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetProjectsCommentsResult GetProjectsComments(string  projectId)
        {
            var r = new GetProjectsCommentsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + projectId + "/comments?public_key={0}&secret_key={1}"
                        ,KeyPublic, KeySecret);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetProjectsCommentsResult>(json);
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


    public struct GetProjectsCommentsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetProjectsCommentsType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg + (Results != null ?  " " + String.Join("\r\n", Results.Select(a=>a.CommentContent)) : "") : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetProjectsCommentsType
    {
        [JsonProperty(PropertyName = "id")]
        public int Id;
        [JsonProperty(PropertyName = "date")]
        public string Date;
        [JsonProperty(PropertyName = "commenter_name")]
        public string CommenterName;
        [JsonProperty(PropertyName = "commenter_role")]
        public string CommenterRole;
        [JsonProperty(PropertyName = "comment_content")]
        public string CommentContent;
    }

}
