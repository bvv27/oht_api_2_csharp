using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetProjectDetailsResult GetProjectDetails(string project_id)
        {
            var r = new GetProjectDetailsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + project_id + "?public_key={0}&secret_key={1}"
                        ,KeyPublic, KeySecret);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetProjectDetailsResult>(json);
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


    public struct GetProjectDetailsResult
    {

        public StatusType status;
        public GetProjectDetailsType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct GetProjectDetailsType
    {
        public int project_id;
        public string project_type;
        public string project_status;
        public string project_status_code;
        public string source_language;
        public string target_language;
        public GetProjectDetailsResources resources;
        public int wordcount;
        public int length;
        public string custom;

    }

    public struct GetProjectDetailsResources
    {
        public string[] sources;
        public string[] translations;
        public string proofs;
        public string transcriptions;

    }



}
