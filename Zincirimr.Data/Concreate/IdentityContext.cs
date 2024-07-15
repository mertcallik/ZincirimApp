using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
        {
            
        }

    }
}
