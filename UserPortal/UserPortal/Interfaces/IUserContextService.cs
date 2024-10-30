using System.Security.Claims;

namespace UserPortal.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }

        public string GetUserEmail();
    }
}
