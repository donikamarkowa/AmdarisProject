using Microsoft.AspNetCore.Identity;
using WorkoutReservations.Application.Services.Interfaces;
using WorkoutReservations.Domain.Entities;

namespace WorkoutReservations.Application.Services
{
    public class AuthService : IAuthService
    {
        //private readonly UserManager<IdentityUser> _userManager;
        //public AuthService(UserManager<IdentityUser> userManager)
        //{
        //    _userManager = userManager;
        //}
        //public async Task<bool> RegisterUser(User user)
        //{
        //    var identityUser = new IdentityUser
        //    {
        //        Email = user.Email
        //    };

        //    var result = await _userManager.CreateAsync(identityUser, user.Password);

        //    return result.Succeeded;
        //}
    }
}
