using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Domain.Extensions
{
    public static class StringExtensionMethods
    {
        public static string ToSentenceCase(this string stringValue)
        {
            stringValue.Trim();
            return stringValue[0].ToString().ToUpper() + stringValue.Substring(1);
        }

    }
}
