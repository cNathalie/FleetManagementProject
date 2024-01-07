using System.Runtime.Serialization;

namespace FM.Domain.Exceptions
{
    [Serializable]
    public class VoertuigException : Exception
    {
        public VoertuigException()
        {
        }

        public VoertuigException(string? message) : base(message)
        {
        }

        public VoertuigException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected VoertuigException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}