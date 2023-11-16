using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_Domain
{
    public class Tankkaart
    {
        public int TankkaartId {  get; set; }
        public int Kaartnummer {  get; set; }
        public DateTime Geldigheidsdatum { get; set; }
        public int Pincode { get; set; }
        public string Brandstoftype { get; set; }
        public bool? IsActief { get; set; }
    }
}
