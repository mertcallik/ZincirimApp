using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Abstract
{
    public interface IAddressRepository
    {
        IQueryable<Address> Addresses { get; }
        IEnumerable<Address> ShowAddressesByUserId(string? id);
        Task AddAddress(Address address, string? userId);
        Task<string> DeleteAddress(int? id);

        Task ActivateAdress(int? id,string? userId);

    }
}
