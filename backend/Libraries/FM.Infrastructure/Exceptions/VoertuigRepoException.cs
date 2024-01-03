using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    internal class VoertuigRepoException : Exception
    {
        public VoertuigRepoException()
        {
        }

        public VoertuigRepoException(string? message) : base(message)
        {
        }

        public VoertuigRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected VoertuigRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}