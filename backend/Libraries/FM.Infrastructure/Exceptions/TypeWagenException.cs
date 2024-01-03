using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    public class TypeWagenException : Exception
    {
        public TypeWagenException()
        {
        }

        public TypeWagenException(string? message) : base(message)
        {
        }

        public TypeWagenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected TypeWagenException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}