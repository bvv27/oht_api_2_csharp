using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public PostNewProjectCommentResult PostNewProjectComment(string project_id, string content)
        {
            var r = new PostNewProjectCommentResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + project_id + "/comments?public_key={0}&secret_key={1}"
                        , KeyPublic, KeySecret);

                    var values = new System.Collections.Specialized.NameValueCollection();
                    values.Add("content", content);
                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<PostNewProjectCommentResult>(json);
                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct PostNewProjectCommentResult
    {

        public StatusType status;
        public string[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg  : status.Code + " " + status.Msg;
        }
    }

}
