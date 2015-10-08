using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public DetectLanguageViaMachineTranslationResult DetectLanguageViaMachineTranslation(string source_content)
        {
            var r = new DetectLanguageViaMachineTranslationResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/mt/detect/text?public_key={0}&secret_key={1}&source_content={2}"
                        , KeyPublic, KeySecret, source_content);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<DetectLanguageViaMachineTranslationResult>(json);
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


    public struct DetectLanguageViaMachineTranslationResult
    {

        public StatusType status;
        public DetectLanguageViaMachineTranslationResultType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? "0 " + status.Msg + " " + results.language : status.Code + " " + status.Msg;
        }
    }
    public struct DetectLanguageViaMachineTranslationResultType
    {

        public string language;


    }

}
