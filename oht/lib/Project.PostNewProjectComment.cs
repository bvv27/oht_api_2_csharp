using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public PostNewProjectCommentResult PostNewProjectComment(string projectId, string content)
        {
            var r = new PostNewProjectCommentResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + projectId + "/comments?public_key={0}&secret_key={1}"
                        , KeyPublic, KeySecret);

                    var values = new System.Collections.Specialized.NameValueCollection {{"content", content}};
                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<PostNewProjectCommentResult>(json);
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


    public struct PostNewProjectCommentResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg  : Status.Code + " " + Status.Msg;
        }
    }

}
