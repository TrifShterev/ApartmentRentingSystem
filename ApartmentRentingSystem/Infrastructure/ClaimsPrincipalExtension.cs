using System.Security.Claims;

namespace ApartmentRentingSystem.Infrastructure
{
    public static class ClaimsPrincipalExtension
    {
      
            public static string GetId(this ClaimsPrincipal user)
            {
                return user.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        
    }
}