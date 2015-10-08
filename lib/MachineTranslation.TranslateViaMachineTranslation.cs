using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public TranslateViaMachineTranslationResult TranslateViaMachineTranslation(string source_language, string target_language, string source_content)
        {
            var r = new TranslateViaMachineTranslationResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/mt/translate/text?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&source_content={4}"
                        , KeyPublic, KeySecret, source_language, target_language, source_content);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<TranslateViaMachineTranslationResult>(json);
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


    public struct TranslateViaMachineTranslationResult
    {

        public StatusType status;
        public TranslateViaMachineTranslationResultType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg + (status.Code == 0 ? " " + results.TranslatedText : "");
        }
    }
    public struct TranslateViaMachineTranslationResultType
    {

        public string TranslatedText;

        
    }

}
