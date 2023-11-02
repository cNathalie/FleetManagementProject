using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_API.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; } = null!;

        public string Wachtwoord { get; set; } = null!;
    }
}
