using Zincirimr.Data.Models;

namespace Zincirimr.Web.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual void AddItem(Product product,int quantity)
        {
            var item = CartItems.FirstOrDefault(c => c.Product.Id == product.Id);
            if (item==null)
            {
                CartItems.Add(new CartItem(){Product = product, Quantity = 1});
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual double CalculateTotal()
        {
            var total = CartItems.Sum(c => c.Product.Price * c.Quantity);
            return total.Value;
        }
        public virtual void Remove(Product product)
        {
            var item= CartItems.FirstOrDefault(c => c.Product.Id == product.Id);
            if (item!=null)
            {
                if (item.Quantity==1)
                {
                    CartItems.Remove(item);
                }
                else
                {
                    item.Quantity--;
                }
            }
        }

        public virtual void RemoveAll()
        {
            CartItems.Clear();
        }
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
    }
}
