using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetProjectsCommentsResult GetProjectsComments(string  project_id)
        {
            var r = new GetProjectsCommentsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + project_id + "/comments?public_key={0}&secret_key={1}"
                        ,KeyPublic, KeySecret);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetProjectsCommentsResult>(json);
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


    public struct GetProjectsCommentsResult
    {

        public StatusType status;
        public GetProjectsCommentsType[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg + (results != null ?  " " + String.Join("\r\n", results.Select(a=>a.comment_content)) : "") : status.Code + " " + status.Msg;
        }
    }
    public struct GetProjectsCommentsType
    {
        public int id;
        public string date;
        public string commenter_name;
        public string commenter_role;
        public string comment_content;
    }

}
