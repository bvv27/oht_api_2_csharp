using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateFileResourceResult Resources(string upload, string fileName = "", string fileMime = "", string fileContent = "")
        {
            var r = new CreateFileResourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    if (String.IsNullOrWhiteSpace(fileContent))
                    {
                        var web = Url + String.Format("/resources/file?public_key={0}&secret_key={1}&file_name={2}&file_mime={3}", KeyPublic, KeySecret, fileName, fileMime);
                        byte[] json = client.UploadFile(web, upload);
                        r = JsonConvert.DeserializeObject<CreateFileResourceResult>(Encoding.UTF8.GetString(json));
                    }
                    else
                    {
                        var web = Url + String.Format("/resources/file?public_key={0}&secret_key={1}&file_name={2}&file_mime={3}", KeyPublic, KeySecret, fileName, fileMime);

                        var values = new System.Collections.Specialized.NameValueCollection
                        {
                            {"file_content", fileContent}
                        };
                        string json = Encoding.Default.GetString(client.UploadValues(web, values));
                        r = JsonConvert.DeserializeObject<CreateFileResourceResult>(json);
                    }


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

    /// <summary>
    /// Create file resource. Create a new file entity on One Hour Translation
    /// </summary>
    public struct CreateFileResourceResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
}
