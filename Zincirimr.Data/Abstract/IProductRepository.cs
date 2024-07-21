using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProduct(Product product);
        IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize);
        int GetProductCount(string category);
    }
}
