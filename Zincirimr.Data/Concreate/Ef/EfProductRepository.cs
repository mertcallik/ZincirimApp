using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate.Ef
{
    public class EfProductRepository : IProductRepository
    {
        private readonly IdentityContext _context;

        public EfProductRepository(IdentityContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task AddProduct(Product product)
        {
           await _context.Products.AddAsync(product);
          await _context.SaveChangesAsync();
        }

        public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            var products=_context.Products.Include(c => c.Categories).AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Categories.Any(c => c.Url == category));
            }

            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int GetProductCount(string category)
        {
            return category == null
                ? _context.Products.Count()
                : _context.Products.Where(p => p.Categories.Any(c => c.Url == category)).Count();
        }
    }
}
