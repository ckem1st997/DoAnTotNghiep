using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Authentication
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>
    {
        public readonly IUserSevice _context;
        public RolesAuthorizationHandler(IUserSevice context)
        {
            _context = context;
        }
        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolesAuthorizationRequirement requirement)
        {

            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var validRole = false;
            if (requirement.AllowedRoles == null || !requirement.AllowedRoles.Any())
                validRole = true;
            else
            {
                var claims = context.User.Claims;
               // var id = claims.FirstOrDefault(c => c.Type == "id").Value;
                // quyền nhập vào
                var roles = requirement.AllowedRoles;
                //  Console.WriteLine(_context);
                var user =await _context.GetUser();
                validRole = roles.Contains(user.Role);

            }

            if (validRole)
                context.Succeed(requirement);

            else
                context.Fail();
            return Task.CompletedTask;
        }

    }
}