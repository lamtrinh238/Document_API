using Document_API.Models;
using Document_API.Utilities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Collections;

using System.Linq;

namespace Document_API.Filters
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public JwtAuthenticationAttribute(params EnumRole[] roles)
        {
            Roles = roles;
            if (Roles == null || Roles.Length == 0)
            {
                Roles.SetValue(EnumRole.All,0);
            }
        }

        //public EnumRole Role { get; set; }
        public EnumRole[] Roles { get; set; }

        public bool AllowMultiple => throw new NotImplementedException();

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            // unauthen
            if (principal == null) 
            { 
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
                return;
            }

            // check role
            if (Roles.Contains(EnumRole.All) || principal.IsInRole(EnumRole.Admin.ToString())) {
                context.Principal = principal;
                return;
            }
            foreach (var item in Roles)
            {
                if (principal.IsInRole(item.ToString()))
                {
                    context.Principal = principal;
                    return;
                }
            }

            // More validate to check whether username 
            //TO DO
            context.ErrorResult = new AuthenticationFailureResult("Unauthorized", request);
            return;
        }

        private static bool ValidateToken(string token, out User user)
        {
            user = new User();
            var simplePrinciple = JwtManager.GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            user.Username = usernameClaim?.Value;
            var userRoleClaim = identity.FindFirst(ClaimTypes.Role);
            Enum.TryParse(userRoleClaim?.Value, out EnumRole roleVal);
            user.Role = roleVal;

            if (string.IsNullOrEmpty(user.Username))
                return false;

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            bool isValidToken = ValidateToken(token, out User user);
            if (isValidToken)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal userPrincipal = new ClaimsPrincipal(identity);

                return Task.FromResult(userPrincipal);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
