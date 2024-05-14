using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkoutReservations.Application.DTOs.Auth;
using WorkoutReservations.Application.DTOs.User;
using WorkoutReservations.Application.Services;
using WorkoutReservations.Domain.Entities;
using WorkoutReservations.Infrastructure.Database;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WorkoutReservationsDbContext _workoutReservationsDbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityService _identityService;
        public AuthController(WorkoutReservationsDbContext workoutReservationsDbContext, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager, IdentityService identityService)
        {
            _workoutReservationsDbContext = workoutReservationsDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            var user = new User {
                Email = registerUser.Email,
                UserName = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Gender = registerUser.Gender,
                RoleId = Guid.Parse(registerUser.Role.Id)
            };
            var createdUser = await _userManager.CreateAsync(user, registerUser.Password);

            var newClaims = new List<Claim>
            {
                new("FirstName", registerUser.FirstName),
                new("LastName", registerUser.LastName)
            };

            await _userManager.AddClaimsAsync(user, newClaims);

            //if (registerUser.Role.Name == "Admin")
            //{
            //    var searchedRole = await _roleManager.FindByNameAsync("Admin");
            //    if (searchedRole != null)
            //    {
            //        searchedRole = new Role("Admin");
            //        await _roleManager.CreateAsync(role);
            //    }
            //    await _userManager.AddToRoleAsync(user, "Admin");

            //    newClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
            //}
            //else
            //{
            //    var searchedRole = await _roleManager.FindByNameAsync("User");
            //    if (searchedRole != null)
            //    {
            //        searchedRole = new Role("User");
            //        await _roleManager.CreateAsync(searchedRole);
            //    }
            //    await _userManager.AddToRoleAsync(user, "User");

            //    newClaims.Add(new Claim(ClaimTypes.Role, "User"));
            //}

            Role role = await _roleManager.FindByIdAsync(registerUser.Role.Id);
            if (role == null)
            {
                //role = new Role { Name = registerUser.Role.Name};
                //await _roleManager.CreateAsync(role);
                throw new ArgumentException($"Role with id: {registerUser.Role.Id} does not exist.");

            }

            //await _userManager.AddToRoleAsync(user, registerUser.Role.Id!);
            await _userManager.AddToRoleAsync(user, role.Name!);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Email ?? throw new InvalidOperationException()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException())
            });

            claimsIdentity.AddClaims(newClaims);

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            var response = new AuthResultDto(_identityService.WriteToken(token));
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user!, loginUser.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest("Couldn't sign in.");
            }

            var claims = await _userManager.GetClaimsAsync(user!);   

            var roles = await _userManager.GetRolesAsync(user!);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user!.Email ?? throw new InvalidOperationException()),
                new(JwtRegisteredClaimNames.Email, user.Email ?? throw new InvalidOperationException())
            });

            claimsIdentity.AddClaims(claims);

            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            var response = new AuthResultDto(_identityService.WriteToken(token));
            return Ok(response);
        }
    }
}
