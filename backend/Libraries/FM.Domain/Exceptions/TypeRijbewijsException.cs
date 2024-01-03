using System.Runtime.Serialization;

namespace FM.Domain.Exceptions
{
    [Serializable]
    public class TypeRijbewijsException : Exception
    {
        public TypeRijbewijsException()
        {
        }

        public TypeRijbewijsException(string? message) : base(message)
        {
        }

        public TypeRijbewijsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected TypeRijbewijsException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}