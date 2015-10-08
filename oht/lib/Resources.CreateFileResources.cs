using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateFileResourceResult Resources(string secret_key, string public_key, string upload, string file_name = "", string file_mime = "", string file_content = "")
        {
            var r = new CreateFileResourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    if (String.IsNullOrWhiteSpace(file_content))
                    {
                        var web = Url + String.Format("/resources/file?secret_key={0}&public_key={1}&file_name={2}&file_mime={3}", secret_key, public_key, file_name, file_mime);
                        byte[] json = client.UploadFile(web, upload);
                        r = JsonConvert.DeserializeObject<CreateFileResourceResult>(Encoding.UTF8.GetString(json));
                    }
                    else
                    {
                        var web = Url + String.Format("/resources/file?secret_key={0}&public_key={1}&file_name={2}&file_mime={3}", secret_key, public_key, file_name, file_mime);

                        var values = new System.Collections.Specialized.NameValueCollection();
                        values.Add("file_content", file_content);
                        string json = Encoding.Default.GetString(client.UploadValues(web, values));
                        r = JsonConvert.DeserializeObject<CreateFileResourceResult>(json);
                    }


                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }

        public CreateFileResourceResult Resources(string file_name, string file_mime, string upload, string file_content)
        {
            return Resources(KeySecret, KeyPublic, file_name, file_mime, upload, file_content);
        }
    }

    /// <summary>
    /// Create file resource. Create a new file entity on One Hour Translation
    /// </summary>
    public struct CreateFileResourceResult
    {

        public StatusType status;
        public string[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
}
