using System.Security.Claims;
using UserPortal.Exceptions;
using UserPortal.Interfaces;

namespace UserPortal.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccesor;
        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccesor = contextAccessor;
        }

        public ClaimsPrincipal User => _contextAccesor.HttpContext.User;

        public string GetUserEmail()
        {
            var claim = User.FindFirst(c => c.Type == ClaimTypes.Email) ?? throw new NotFoundException("User not found");
            return claim.Value;
        }
    }
}
