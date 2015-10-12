using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public DetectLanguageViaMachineTranslationResult DetectLanguageViaMachineTranslation(string sourceContent)
        {
            var r = new DetectLanguageViaMachineTranslationResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/mt/detect/text?public_key={0}&secret_key={1}&source_content={2}"
                        , KeyPublic, KeySecret, sourceContent);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<DetectLanguageViaMachineTranslationResult>(json);
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


    public struct DetectLanguageViaMachineTranslationResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public DetectLanguageViaMachineTranslationResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? "0 " + Status.Msg + " " + Result.Language : Status.Code + " " + Status.Msg;
        }
    }
    public struct DetectLanguageViaMachineTranslationResultType
    {
        [JsonProperty(PropertyName = "language")]
        public string Language;
    }

}
