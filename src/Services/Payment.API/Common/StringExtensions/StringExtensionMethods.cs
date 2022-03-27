using System.Text;

namespace Payment.API.Common.StringExtensions
{
    public static class StringExtensionMethods
    {
        public static string AnonymizeCardholderNumberString(this string str)
        {
            if (str == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(str.Substring(0, 6));
            sb.Append("********");
            sb.Append(str.Substring(str.Length - 4, 4));

            return sb.ToString();
        }
    }
}
