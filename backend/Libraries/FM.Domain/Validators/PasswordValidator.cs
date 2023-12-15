using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Domain.Validators
{
    public class PasswordValidator
    {
        public static bool IsValidPassword(string password)
        {
            if(string.IsNullOrEmpty(password)) return false; 
            var pw = password.Trim();
            if(pw.Length < 4) return false;
            if(!pw.Any(char.IsDigit) ) return false;
            return true;
        }
    }
}
