using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public RetrieveProjectRatingsResult RetrieveProjectRatings(string project_id)
        {
            var r = new RetrieveProjectRatingsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + project_id + "/rating?public_key={0}&secret_key={1}",KeyPublic, KeySecret);

                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<RetrieveProjectRatingsResult>(json);
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


    public struct RetrieveProjectRatingsResult
    {

        public StatusType status;
        public RetrieveProjectRatingsType[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct RetrieveProjectRatingsType
    {
        public string type;
        public int rate;
        public string remarks;
        public string date;
    }


}
