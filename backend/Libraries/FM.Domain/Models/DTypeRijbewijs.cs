﻿using FM.Domain.Exceptions;

namespace FM.Domain.Models
{
    public class DTypeRijbewijs
    {
        public int TypeRijbewijsId { get; set; }

        private string? _type;
        public string? Type 
        {
            get { return _type ?? throw new TypeRijbewijsException("'Type' was not set to a proper value"); } 
            set
            {
                if(!String.IsNullOrEmpty(value))
                {
                    _type = value.Trim();
                }
                else
                {
                    throw new TypeRijbewijsException("'Type' is Empty");
                }

            }
        }
    }
}
