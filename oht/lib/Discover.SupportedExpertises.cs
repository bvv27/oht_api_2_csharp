using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public SupportedExpertisesResult SupportedExpertises(string source_language, string target_language)
        {
            var r = new SupportedExpertisesResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/discover/expertise?public_key={0}&source_language={1}&target_language={2}"
                        , KeyPublic, source_language, target_language);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<SupportedExpertisesResult>(json);
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


    public struct SupportedExpertisesResult
    {

        public StatusType status;
        public SupportedExpertisesResultType[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct SupportedExpertisesResultType
    {

        public string name;

        public string code;
    }

}
