using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Domain.Validators
{
    public class GasCardNumberValidator
    {
        public const int RequiredLength = 9;
        public static bool IsValidCardNumber(int cardNumber)
        {
            var numberString = cardNumber.ToString();
            if (numberString.Length == RequiredLength) return true;
            return false;
        }
    }
}
