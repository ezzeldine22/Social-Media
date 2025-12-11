using Application.Interfaces;
using System.Security.Claims;

namespace API
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContext(IHttpContextAccessor httpContext)

        {
            _httpContext = httpContext;
        }
        public string GetUserId()
        {
            var user = _httpContext.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }
    }
}
