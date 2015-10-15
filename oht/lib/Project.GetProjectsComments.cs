using System;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IGetProjectsCommentsProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId);
    }
    public class GetProjectsCommentsProvider : IGetProjectsCommentsProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/" + projectId + "/comments?public_key={0}&secret_key={1}", publicKey, secretKey);
                return client.DownloadString(web);
            }
        }
    }
    partial class Ohtapi
    {
        public IGetProjectsCommentsProvider GetProjectsCommentsProvider;
        public GetProjectsCommentsResult GetProjectsComments(string  projectId)
        {
            var r = new GetProjectsCommentsResult();
            try
            {
                if (GetProjectsCommentsProvider == null)
                    GetProjectsCommentsProvider = new GetProjectsCommentsProvider();
                var json = GetProjectsCommentsProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId);
                r = JsonConvert.DeserializeObject<GetProjectsCommentsResult>(json);
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
