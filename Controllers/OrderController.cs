namespace SportsStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SportsStore.Models;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;

    public class OrderController : Controller
    {
        private IOrderRepository repository;

        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            this.repository = repoService;
            this.cart = cartService;
        }

        [Authorize]
        public ViewResult List() => this.View(this.repository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = this.repository.Orders.FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                this.repository.SaveOrder(order);
            }

            return this.RedirectToAction(nameof(this.List));
        }

        public ViewResult Checkout() => this.View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (this.cart.Lines.Count() == 0)
            {
                this.ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (this.ModelState.IsValid)
            {
                order.Lines = this.cart.Lines.ToArray();
                this.repository.SaveOrder(order);
                return this.RedirectToAction(nameof(this.Completed));
            }
            else
            {
                return this.View(order);
            }
        }

        public ViewResult Completed()
        {
            this.cart.Clear();
            return this.View();
        }
    }
}