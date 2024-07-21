using System.Text.Json.Serialization;
using Zincirimr.Data.Models;
using Zincirimr.Web.Helpers;

namespace Zincirimr.Web.Models
{
    public class SessionCart:Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            SessionCart cart = session.GetJson<SessionCart>("cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("cart",this);
        }

        public override void Remove(Product product)
        {
            base.Remove(product);
            Session.SetJson("cart",this);
        }

        public override void RemoveAll()
        {
            base.RemoveAll();
            Session.Remove("cart");
        }
    }
}
