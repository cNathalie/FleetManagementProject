using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_Domain.Validators
{
    public class RRNValidator
    {
        /// <summary>
        /// Check the checksum for a Rijksregister
        /// https://sandervandevelde.wordpress.com/2020/08/13/belgische-rijksregisternummer-checksum-testen-dutch/
        /// </summary>
        /// <param name="rrn">Rijksregister number</param>
        /// <returns>True means a correct RRN checksum</returns>
        public static bool CheckRijksRegisterNumberChecksum(string rrn)
        {
            var rrnChecksum = Convert.ToInt32(rrn.Substring(9, 2));

            // we pick the RRN part we want to recalculate the checksum for
            var partToCalculate = rrn.Substring(0, 9);
            var rrnInt = Int64.Parse(partToCalculate);

            // we calculate the expected checksum
            var checksum = 97 - (rrnInt % 97);

            // we compare the excisting checksum with the calculated
            if (rrnChecksum == checksum)
            {
                // we have a good checksum
                return true;
            }

            //// Checksum not yet ok. We check for a possible 1900/2000 situation;

            // we repeat the same test but now with the extra '2' added to the part
            partToCalculate = "2" + partToCalculate;
            rrnInt = Int64.Parse(partToCalculate);

            // we calculate the expected checksum. again
            checksum = 97 - (rrnInt % 97);

            // we compare the excisting checksum with the calculated, again
            if (rrnChecksum == checksum)
            {
                // we have a good checksum. Person born between 2000 and now
                return true;
            }
            else
            {
                // invalid number, even after 2000 check
                return false;
            }
        }
    }
}
