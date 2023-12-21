using FM.Domain.Exceptions;
using FM.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.Domain.Models
{
    public class DTypeWagen
    {
        public int TypeWagenId { get; set; }

        private string? _type;
        public string? Type
        {
            get { return _type ?? throw new TypeWagenException("'Type' was not set to a proper value"); }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _type = value.Trim().ToSentenceCase();
                }
                else
                {
                    throw new TypeWagenException("'Type' is Empty");
                }
            }
        }
    }
}
