using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Zincirimr.Data.Models
{
    public class AppUser : IdentityUser
    {
        
        public string? FirstName { get; set; }=string.Empty;
        public string? LastName { get; set; }=string.Empty;
    }
}
