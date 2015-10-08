﻿using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IAccountProvider
    {
        string Get(string url, WebProxy proxy, string secretKey, string publicKey);
    }
    public class AccountProvider : IAccountProvider
    {
        public string Get(string url, WebProxy proxy, string secretKey, string publicKey)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/account?secret_key={0}&public_key={1}", secretKey, publicKey);
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IAccountProvider AccountProvider;
        /// <summary>
        /// Get account details
        /// </summary>
        /// <returns></returns>
        public AccountResult Account()
        {
            var r = new AccountResult();
            try
            {
                if (AccountProvider == null)
                    AccountProvider = new AccountProvider();
                var json = AccountProvider.Get(Url, _proxy, KeySecret, KeyPublic);
                r = JsonConvert.DeserializeObject<AccountResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
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
    /// Get account details. Fetch basic account details and credits balance
    /// </summary>
    public struct AccountResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public AccountResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public AccountResultType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct AccountResultType
    {
        /// <summary>
        /// Unique account id in OHT
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public int AccountId;
        /// <summary>
        /// OHT username
        /// </summary>
        [JsonProperty(PropertyName = "account_username")]
        public string AccountUsername;
        /// <summary>
        /// Currently available credits balance
        /// </summary>
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }

}