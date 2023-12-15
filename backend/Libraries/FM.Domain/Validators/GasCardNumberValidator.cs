using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Domain.Validators
{
    public class GasCardNumberValidator
    {
        private const int _requiredLength = 9;
        public static bool IsValidCardNumber(int cardNumber)
        {
            var numberString = cardNumber.ToString();
            if (numberString.Length == _requiredLength) return true;
            return false;
        }
    }
}
