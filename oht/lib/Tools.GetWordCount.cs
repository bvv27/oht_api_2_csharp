using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetWordCountResult GetWordCount(string resources)
        {
            var r = new GetWordCountResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/tools/wordcount?public_key={0}&secret_key={1}&resources={2}"
                        ,KeyPublic, KeySecret, resources);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetWordCountResult>(json);
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


    public struct GetWordCountResult
    {

        public StatusType status;
        public GetWordCountType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct GetWordCountType
    {

        public GetWordCountResources[] resources;
        public GetWordCountTotal total;

    }

    public struct GetWordCountResources
    {
        public string resource;
        public int wordcount;
    }
    public struct GetWordCountTotal
    {
        public int wordcount;
    }
}
