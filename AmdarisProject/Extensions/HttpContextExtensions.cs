using System.Security.Claims;

namespace AmdarisProject.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserIdExtension(this HttpContext context)
        {
            return context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
