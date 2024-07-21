using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zincirimr.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        public double? Price { get; set; }
        public string? Image { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new List<Category>();


    }
}
