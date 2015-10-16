
using System;
using System.Reflection;

namespace oht.lib
{
    public enum StringCurrency
    {
        [StringValue("")] None,
        [StringValue("USD")] Usd,
        [StringValue("EUR")] Eur
    }

    public enum StringAvailability
    {
        [StringValue("")] None,
        [StringValue("high")] High,
        [StringValue("medium")] Medium,
        [StringValue("low")] Low
    }

    public enum StringProjectType
    {
        [StringValue("")] None,
        [StringValue("Translation")] Translation,
        [StringValue("Expert Translation")] ExpertTranslation,
        [StringValue("Proofreading")] Proofreading,
        [StringValue("Transcription")] Transcription,
        [StringValue("Translation + Proofreading")] TranslationProofreading
    }

    public enum StringProjectStatusCode
    {
        [StringValue("")]
        None,
        [StringValue("Pending")]
        Pending,
        [StringValue("in_progress")]
        InProgress,
        [StringValue("submitted")]
        Submitted,
        [StringValue("signed")]
        Signed,
        [StringValue("completed")]
        Completed,
        [StringValue("canceled")]
        Canceled
    }

    public enum StringCommenterRole
    {
        [StringValue("")]
        None,
        [StringValue("admin")]
        Admin,
        [StringValue("customer")]
        Customer,
        [StringValue("provider")]
        Provider,
        [StringValue("potential-provider")]
        PotentialProvider
    }

    public enum StringType
    {
        [StringValue("")]
        None,
        [StringValue("Customer")]
        Customer,
        [StringValue("Service")]
        Service
    }

    public enum StringTypeFile
    {
        [StringValue("")]
        None,
        [StringValue("text")]
        Text,
        [StringValue("file")]
        File
    }

    public enum StringService
    {
        [StringValue("")]
        None,
        [StringValue("translation")]
        Translation,
        [StringValue("proofreading")]
        Proofreading,
        [StringValue("transproof")]
        Transproof,
        [StringValue("transcription")]
        Transcription
    }

    public enum StringProofreading
    {
        [StringValue("")]
        None,
        [StringValue("0")]
        No,
        [StringValue("1")]
        Yes
    }

    public class StringValue : Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
    public static class StringEnum
    {
        public static string GetStringValue<TEnum>(this TEnum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs != null && attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

}
