using System.Security.Claims;
using WorkoutReservations.Domain.Constants;

namespace AmdarisProject.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserIdExtension(this HttpContext context)
        {
            return context.User.FindFirstValue(CustomClaimTypes.Id);
        }
    }
}
