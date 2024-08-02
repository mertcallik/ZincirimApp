using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate.Ef
{
    public class EfAdressRepository : IAddressRepository
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;


        public EfAdressRepository(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IQueryable<Address> Addresses { get; }

        public IEnumerable<Address> ShowAddressesByUserId(string? id)
        {
            var addresses = _context.Users.Include(x => x.Addresses).FirstOrDefault(x => x.Id == id).Addresses ?? new List<Address>();

            return addresses;
        }

        public async Task AddAddress(Address? address, string? userId)
        {
            if (address != null && !string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                address.AppUser = user;
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> DeleteAddress(int? id)
        {

            var adress = await _context.Addresses?.FirstOrDefaultAsync(a => a.Id == id);
            if (adress!=null)
            {
                if (adress.IsActive == false)
                {
                    _context.Addresses.Remove(adress);
                    await _context.SaveChangesAsync();
                    return "Adres başarıyla silindi";
                }
                return "Aktif Adres silinmeden önce başka bir aktif adres seçilmeliir";

            }
            return "Adres bulunamadı";

        }

        public async Task ActivateAdress(int? id, string? userId)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var user = _context.Users.Include(u => u.Addresses).FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    var userAddresses = user.Addresses;

                    // Tüm adresleri devre dışı bırak
                    foreach (var address in userAddresses)
                    {
                        address.IsActive = false;
                    }

                    // Aktif adresi belirle
                    var activeAddress = userAddresses.FirstOrDefault(a => a.Id == id);
                    if (activeAddress != null)
                    {
                        activeAddress.IsActive = true;
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
