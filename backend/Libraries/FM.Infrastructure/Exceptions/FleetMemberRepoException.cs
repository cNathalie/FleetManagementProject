using System.Runtime.Serialization;

namespace FM.Infrastructure.Exceptions
{
    [Serializable]
    public class FleetMemberRepoException : Exception
    {
        public FleetMemberRepoException()
        {
        }

        public FleetMemberRepoException(string? message) : base(message)
        {
        }

        public FleetMemberRepoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        //protected FleetMemberRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{
        //}
    }
}