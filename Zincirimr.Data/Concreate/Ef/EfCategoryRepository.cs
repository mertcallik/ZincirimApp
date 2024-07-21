using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;

namespace Zincirimr.Data.Concreate.Ef
{
    public class EfCategoryRepository:ICategoryRepository
    {
        private readonly IdentityContext _context;

        public EfCategoryRepository(IdentityContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categories;
    }
}
