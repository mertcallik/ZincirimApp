using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Abstract
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(AppUser user, string password);
        Task<SignInResult> Login(string email, string password, bool rememberMe, bool lockoutOnFailure);
        Task<string> GenerateConfirmToken(string userId);
        Task<IdentityResult> ConfirmEmail(string userId, string token);

    }
}
