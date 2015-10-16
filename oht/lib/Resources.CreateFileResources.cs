using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateFileResourceProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string upload, string fileName = "", string fileMime = "", string fileContent = "");
    }
    public class CreateFileResourceProvider : ICreateFileResourceProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string upload, string fileName = "", string fileMime = "", string fileContent = "")
        {
            using (var client = new WebClient())
            {
                    if (proxy != null)
                    client.Proxy = proxy;

                client.Encoding = Encoding.UTF8;
                if (String.IsNullOrWhiteSpace(fileContent))
                {
                    var web = url + String.Format("/resources/file?public_key={0}&secret_key={1}&file_name={2}&file_mime={3}", publicKey, secretKey, fileName, fileMime);
                    byte[] json = client.UploadFile(web, upload);
                    return Encoding.UTF8.GetString(json);
                }
                else
                {
                    var web = url + String.Format("/resources/file?public_key={0}&secret_key={1}&file_name={2}&file_mime={3}", publicKey, secretKey, fileName, fileMime);

                    var values = new System.Collections.Specialized.NameValueCollection
                        {
                            {"file_content", fileContent}
                        };
                    return Encoding.Default.GetString(client.UploadValues(web, values));
                }
            }
        }
    }

    partial class Ohtapi
    {
        public ICreateFileResourceProvider CreateFileResourceProvider;
        /// <summary>
        /// Create a new file entity from supported formats.
        /// </summary>
        /// <param name="upload">File content to upload, submitted via multipart/form-data request</param>
        /// <param name="fileName">Replace the original file's name on One Hour Translation</param>
        /// <param name="fileMime">Replace the default mime value for the file</param>
        /// <param name="fileContent">Content of the new file, works only with "file_name" not empty. If used, actual upload is skipped.</param>
        /// <returns></returns>
        public CreateFileResourceResult CreateFileResources(string upload, string fileName = "", string fileMime = "", string fileContent = "")
        {
            var r = new CreateFileResourceResult();
            try
            {
                if (CreateFileResourceProvider == null)
                    CreateFileResourceProvider = new CreateFileResourceProvider();
                var json = CreateFileResourceProvider.Get(Url, _proxy, KeyPublic, KeySecret, upload, fileName, fileMime, fileContent);
                r = JsonConvert.DeserializeObject<CreateFileResourceResult>(json);
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
