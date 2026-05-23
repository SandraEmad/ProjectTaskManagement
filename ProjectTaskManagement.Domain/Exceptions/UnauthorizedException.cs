

namespace ProjectTaskManagement.Domain.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException()
            : base("You are not authorized to perform this action.") { }
        public UnauthorizedException(string message = "You are not authorized to perform this action.")
            : base(message) { }

    }
}
