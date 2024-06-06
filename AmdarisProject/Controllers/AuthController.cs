﻿using AmdarisProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WorkoutReservations.Application.DTOs.Auth;
using WorkoutReservations.Application.DTOs.User;
using WorkoutReservations.Application.Services;
using WorkoutReservations.Domain.Constants;
using WorkoutReservations.Domain.Entities;

namespace AmdarisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IdentityService _identityService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IdentityService identityService, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _identityService = identityService;
            _contextAccessor = contextAccessor;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUser)
        {
            var user = new User {
                Id = Guid.NewGuid(),
                Email = registerUser.Email,
                UserName = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                Gender = registerUser.Gender,
                RoleId = Guid.Parse(registerUser.Role.Id),
            };
            var createdUser = await _userManager.CreateAsync(user, registerUser.Password);

            if (!createdUser.Succeeded)
            {
                return BadRequest();
            }

            var newClaims = new List<Claim>
            {
                new(CustomClaimTypes.FirstName, registerUser.FirstName),
                new(CustomClaimTypes.LastName, registerUser.LastName),
                new(CustomClaimTypes.Id, user.Id.ToString())
            };

            await _userManager.AddClaimsAsync(user, newClaims);

            Role role = await _roleManager.FindByIdAsync(registerUser.Role.Id);
            if (role == null)
            {
                throw new ArgumentException($"Role with id: {registerUser.Role.Id} does not exist.");

            }

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

        [HttpPost("edit")]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileDto editProfile)
        {
            var userId = HttpContext.GetUserIdExtension();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in token.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.Age = editProfile.Age;
            user.Bio = editProfile.Bio;
            user.Weight = editProfile.Weight;
            user.Height = editProfile.Height;
            user.PhoneNumber = editProfile.PhoneNumber;
            user.Picture = editProfile.Picture;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to update profile.");
            }

            return Ok("Profile updated successfully.");
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDtos = roles.Select(role => new RoleInfoDto
            {
                Id = role.Id.ToString(),
                Name = role.Name!
            }).ToList();

            return Ok(roleDtos);
        }
    }
}
