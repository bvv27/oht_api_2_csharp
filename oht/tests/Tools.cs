using System;

namespace oht
{
    public class Tools
    {
        public static readonly string TestSecretKey = Environment.GetEnvironmentVariable("SecretKey");
        public static readonly string TestPublicKey = Environment.GetEnvironmentVariable("PublicKey");
        public const string ExceptedUrl = "https://sandbox.onehourtranslation.com/api/2";
    }
}
