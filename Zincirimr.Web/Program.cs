using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Concreate;
using Zincirimr.Data.Concreate.Ef;
using Zincirimr.Data.Models;
using Zincirimr.Web.Models.Mail;

namespace Zincirimr.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option => option.MigrationsAssembly("Zincirimr.Web")));
            builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                //password options
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                //user options
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöprsştuüvyz0123456789";

                //Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true; // Yeni kullanıcılar için kilitlenme aktif mi?

                //SignIn settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login"; // Giriş yolu özelleştirildi
                opt.LogoutPath = "/Account/Logout"; // Çıkış yolu özelleştirildi
                opt.AccessDeniedPath = "/Account/AccessDenied"; // Erişim engellendi yolu özelleştirildi
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });
            builder.Services.AddScoped<IAuthRepository, EfAuthRepository>();
            builder.Services.AddAutoMapper(typeof(Program));

            #region email
            builder.Services.AddScoped<IMailSender, SmtpMailSender>(s =>
                new SmtpMailSender(builder.Configuration["Brevo:server"],builder.Configuration.GetValue<int>("Brevo:port"),

                builder.Configuration["Brevo:login"],
                
                builder.Configuration["Brevo:password"],
                builder.Configuration.GetValue<bool>("Brevo:ssl"),

                    builder.Configuration["Brevo:sender"]

                )
            );
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
