using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public PostProjectRatingsResult PostProjectRatings(string public_key, string secret_key, int project_id, string type,int rate, string remarks="")
        {
            var r = new PostProjectRatingsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + project_id + "/rating?public_key={0}&secret_key={1}&type={2}&rate={3}&remarks={4}"
                        ,public_key, secret_key, type, rate, remarks);
                    
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<PostProjectRatingsResult>(json);
                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }

        public PostProjectRatingsResult PostProjectRatings(int project_id, string type, int rate, string remarks = "")
        {
            return PostProjectRatings(KeyPublic, KeySecret, project_id, type,rate, remarks);
        }
    }


    public struct PostProjectRatingsResult
    {

        public StatusType status;
        public string[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }

}
