namespace FM.Domain.Extensions
{
    public static class StringExtensionMethods
    {
        public static string ToSentenceCase(this string stringValue)
        {
            if (String.IsNullOrWhiteSpace(stringValue)) throw new ArgumentNullException("String is null or empty.");
            stringValue.Trim();
            return stringValue[0].ToString().ToUpper() + stringValue.Substring(1);
        }

    }
}
