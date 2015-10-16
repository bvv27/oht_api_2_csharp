using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IGetQuoteProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string resources, string wordcount, string sourceLanguage, string targetLanguage
            , StringService service = StringService.None, string expertise = "", StringProofreading proofreading = StringProofreading.None, StringCurrency stringCurrency = StringCurrency.None);
    }
    public class GetQuoteProvider : IGetQuoteProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string resources, string wordcount, string sourceLanguage, string targetLanguage
            , StringService service = StringService.None, string expertise = "", StringProofreading proofreading = StringProofreading.None, StringCurrency stringCurrency = StringCurrency.None)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/tools/quote?public_key={0}&secret_key={1}&resources={2}&wordcount={3}&source_language={4}&target_language={5}&service={6}&expertise={7}&proofreading={8}&currency={9}"
                    , publicKey, secretKey, resources, wordcount, sourceLanguage, targetLanguage, service.GetStringValue(), expertise, proofreading.GetStringValue(), stringCurrency.GetStringValue());
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IGetQuoteProvider GetQuoteProvider;
        /// <summary>
        /// Get the summary of an order
        /// </summary>
        /// <param name="resources">a comma (,) separated list of resource_uuid</param>
        /// <param name="wordcount">word count </param>
        /// <param name="sourceLanguage">See the list of Language Codes</param>
        /// <param name="targetLanguage">See the list of Language Codes</param>
        /// <param name="service">[Optional] translation | proofreading | transproof | transcription (defaults to translation)</param>
        /// <param name="expertise">[Optional] See the list of Expertise Codes</param>
        /// <param name="proofreading">[Optional] 0 | 1</param>
        /// <param name="stringCurrency">[Optional] USD | EUR</param>
        /// <returns></returns>
        public GetQuoteResult GetQuote(string resources, string wordcount, string sourceLanguage, string targetLanguage
            , StringService service = StringService.None, string expertise = "", StringProofreading proofreading = StringProofreading.None, StringCurrency stringCurrency = StringCurrency.None)
        {
            var r = new GetQuoteResult();
            try
            {
                if (GetQuoteProvider == null)
                    GetQuoteProvider = new GetQuoteProvider();
                var json = GetQuoteProvider.Get(Url, _proxy, KeyPublic, KeySecret, resources, wordcount, sourceLanguage, targetLanguage, service, expertise, proofreading, stringCurrency);
                r = JsonConvert.DeserializeObject<GetQuoteResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

    }

    public struct GetQuoteResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetQuoteResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetQuoteResultType
    {
        /// <summary>
        /// currency selected by user (or default)
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public string Currency;
        [JsonProperty(PropertyName = "account_username")]
        public string AccountUsername;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
        /// <summary>
        /// Array of results per resource
        /// </summary>
        [JsonProperty(PropertyName = "resources")]
        public GetQuoteResources[] Resources;
        /// <summary>
        /// Array of response params
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public GetQuoteTotal Total;
    }

    public struct GetQuoteResources
    {
        /// <summary>
        /// price of the resource
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal Price;
        /// <summary>
        /// UUID of the resource in list
        /// </summary>
        [JsonProperty(PropertyName = "resource")]
        public string Resource;
        /// <summary>
        /// word count of the resource
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        /// <summary>
        /// credits worth of the resource
        /// </summary>
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }
    public struct GetQuoteTotal
    {
        /// <summary>
        /// price in selected currency, based on credits and discounts
        /// </summary>
        [JsonProperty(PropertyName = "net_price")]
        public decimal NetPrice;
        /// <summary>
        /// price in selected currency, based on fee from payment vendors
        /// </summary>
        [JsonProperty(PropertyName = "transaction_fee")]
        public decimal TransactionFee;
        /// <summary>
        /// total price in selected currency, based on net price and transaction fee.
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public decimal Price;
        /// <summary>
        /// total words count
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        /// <summary>
        /// sum of credits to charge
        /// </summary>
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }
}
