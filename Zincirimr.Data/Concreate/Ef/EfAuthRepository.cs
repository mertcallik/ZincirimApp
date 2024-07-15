using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate.Ef
{
    public class EfAuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public EfAuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> Register(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed(result.Errors.ToArray());
            }

            return IdentityResult.Success;
        }

        public async Task<SignInResult> Login(string email, string password, bool rememberMe, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(email ?? "");
            if (user == null)
            {
                return SignInResult.Failed;
            }

            await _signInManager.SignOutAsync();
            if (!user.EmailConfirmed)
            {
                return SignInResult.NotAllowed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    return SignInResult.NotAllowed;
                }
                if (result.IsLockedOut)
                {
                    return SignInResult.LockedOut;
                }

                return SignInResult.Failed;
            }

            await _userManager.ResetAccessFailedCountAsync(user);
            await _userManager.SetLockoutEndDateAsync(user, null);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                await _userManager.AddClaimsAsync(user, new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
                    new Claim(ClaimTypes.Surname, user.LastName ?? ""),
                    new Claim(ClaimTypes.UserData, "admin.png"),
                });
            }
            if (await _userManager.IsInRoleAsync(user, "user"))
            {
                await _userManager.AddClaimsAsync(user, new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id ?? ""),
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.GivenName, user.FirstName ?? ""),
                    new Claim(ClaimTypes.Surname, user.LastName ?? ""),
                    new Claim(ClaimTypes.UserData, "userdefault.png"),
                });
            }
            return SignInResult.Success;
        }


        public async Task<string> GenerateConfirmToken(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    return token;

                }
            }

            return string.Empty;
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string token)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(token))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, token);
                    if (result.Succeeded)
                    {
                        return IdentityResult.Success;
                    }

                }
            }
            return IdentityResult.Failed();
        }
    }
}
