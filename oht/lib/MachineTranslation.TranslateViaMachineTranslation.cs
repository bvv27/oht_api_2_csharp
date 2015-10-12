using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public TranslateViaMachineTranslationResult TranslateViaMachineTranslation(string sourceLanguage, string targetLanguage, string sourceContent)
        {
            var r = new TranslateViaMachineTranslationResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/mt/translate/text?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&source_content={4}"
                        , KeyPublic, KeySecret, sourceLanguage, targetLanguage, sourceContent);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<TranslateViaMachineTranslationResult>(json);
                }
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct TranslateViaMachineTranslationResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public TranslateViaMachineTranslationResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code + " " + Status.Msg + (Status.Code == 0 ? " " + Result.TranslatedText : "");
        }
    }
    public struct TranslateViaMachineTranslationResultType
    {
        [JsonProperty(PropertyName = "TranslatedText")]
        public string TranslatedText;

        
    }

}
