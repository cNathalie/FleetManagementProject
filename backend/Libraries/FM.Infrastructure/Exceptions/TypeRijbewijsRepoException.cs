using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    internal class TypeRijbewijsRepoException : Exception
    {
        public TypeRijbewijsRepoException()
        {
        }

        public TypeRijbewijsRepoException(string? message) : base(message)
        {
        }

        public TypeRijbewijsRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected TypeRijbewijsRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}