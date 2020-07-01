using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class OrderRepository :IOrderRepository
    {
        private readonly ShoppingCart shoppingCart;
        public AppDbContext AppDbContext { get; set; }

        public OrderRepository(AppDbContext appDbContext , ShoppingCart shoppingCart)
        {
            AppDbContext = appDbContext;
            this.shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = shoppingCart.ShoppingCartItems;
            order.OrderTotal = shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount= item.Amount,
                    PieId = item.Pie.PieId,
                    Price = item.Pie.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            AppDbContext.Orders.Add(order);
            AppDbContext.SaveChanges();
        }
    }
}
