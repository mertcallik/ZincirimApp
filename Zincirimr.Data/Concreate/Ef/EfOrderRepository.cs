using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate.Ef
{
    public class EfOrderRepository:IOrderRepository
    {
        private readonly IdentityContext _context;

        public EfOrderRepository(IdentityContext context)
        {
            _context = context;
        }
        public IQueryable<Order> Orders => _context.Orders;
        public async Task SaveOrder(Order order)
        {
           await _context.Orders.AddAsync(order);
           await _context.SaveChangesAsync();
        }
    }
}
