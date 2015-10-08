using System;
using System.Net;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi : IDisposable
    {
        
        public Ohtapi(string publicKey, string secretKey, bool usingSandbox)
        {
            KeyPublic = publicKey;
            KeySecret = secretKey;
            Url = usingSandbox
                ? "https://sandbox.onehourtranslation.com/api/2" : "https://www.onehourtranslation.com/api/2";
        }

        string Url { get; set; }
        string KeyPublic { get; set; }
        string KeySecret { get; set; }

        private WebProxy _proxy;
        public void SetProxy(string address, string user,  string password)
        {
            _proxy = new WebProxy
            {
                Address = new Uri(address),
                Credentials = new NetworkCredential(user, password),
                UseDefaultCredentials = false,
                BypassProxyOnLocal = false
            };
        }
        public void Dispose()
        {
            
        }
    }

    
    /// <summary>
    /// common type
    /// </summary>
    public struct StatusType
    {
        /// <summary>
        /// return code 0-ok;-1-error
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code;
        /// <summary>
        /// ok | errors message
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg;
    }








}
