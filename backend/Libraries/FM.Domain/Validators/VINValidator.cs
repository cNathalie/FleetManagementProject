using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FM.Domain.Validators
{
    public class VINValidator
    {
        public static bool IsValidVIN(string vin)
        {
            string regexPattern = @"^[A-HJ-NPR-Z\d]{17}$"; //Allows a mixture of uppercase letters and single digits, length = 17
            return Regex.IsMatch(vin, regexPattern);
        }

    }
}
