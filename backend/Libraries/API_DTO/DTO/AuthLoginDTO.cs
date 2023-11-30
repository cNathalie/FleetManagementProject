using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DTO
{
    public class AuthLoginDTO
    {
        public string Email { get; set; } = null!;
        public string Wachtwoord { get; set; } = null!;
    }
}
