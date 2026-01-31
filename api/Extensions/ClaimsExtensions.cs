using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsExtensions
    {
        // public static string GetUsername(this ClaimsPrincipal user)
        // {
        //     return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap/ws/2005/05/identity/claims/givenname")).Value;
        // }
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"CLAIM => {claim.Type} = {claim.Value}");
            }
            var username = user.FindFirstValue(ClaimTypes.GivenName)
               ?? throw new UnauthorizedAccessException("GivenName claim not found");
            return username;
        }

    }
}