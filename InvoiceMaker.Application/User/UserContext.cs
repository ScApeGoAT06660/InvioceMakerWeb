using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor?.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("No user womp womp.");
            }

            if (!user.Identity.IsAuthenticated || user.Identity == null)
            {
                return null;
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;

            return new CurrentUser(id, email);
        }
    }
}
