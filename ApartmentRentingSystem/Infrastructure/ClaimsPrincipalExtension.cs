using System.Security.Claims;
using static ApartmentRentingSystem.Utilities.Constants.WebConstants;

namespace ApartmentRentingSystem.Infrastructure
{
    public static class ClaimsPrincipalExtension
    {
      
            public static string GetId(this ClaimsPrincipal user)
            {
                return user.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            public static bool IsAdmin(this ClaimsPrincipal user)
            {
                return user.IsInRole(AdminRoleName);
            }
        
    }
}