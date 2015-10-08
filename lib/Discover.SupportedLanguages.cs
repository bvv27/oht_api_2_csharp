using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public SupportedLanguagesResult SupportedLanguages(string public_key)
        {
            var r = new SupportedLanguagesResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/discover/languages?public_key={0}", public_key);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<SupportedLanguagesResult>(json);
                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }

        public SupportedLanguagesResult SupportedLanguages()
        {
            return SupportedLanguages(KeyPublic);
        }
    }


    public struct SupportedLanguagesResult
    {

        public StatusType status;
        public SupportedLanguagesResultType[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
    public struct SupportedLanguagesResultType
    {
        [JsonProperty(PropertyName = "name")]
        public string LanguageName;
        [JsonProperty(PropertyName = "code")]
        public string LanguageCode;


    }

}
