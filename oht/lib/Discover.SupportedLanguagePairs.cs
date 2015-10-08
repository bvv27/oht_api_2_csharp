using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public SupportedLanguagePairsResult SupportedLanguagePairs()
        {
            var r = new SupportedLanguagePairsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/discover/language_pairs?public_key={0}", KeyPublic);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<SupportedLanguagePairsResult>(json);
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


    public struct SupportedLanguagePairsResult
    {

        public StatusType status;
        public GetQuoteResultType[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct SupportedLanguagePairsResultType
    {

        public SupportedLanguagePairsSource[] source;

        public SupportedLanguagePairsTargets[] targets;
    }
    public struct SupportedLanguagePairsSource
    {
        public string name;

        public string code;
    }
    public struct SupportedLanguagePairsTargets
    {
        public string name;
        public string code;
        public string availability;
    }
}
