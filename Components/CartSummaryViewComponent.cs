﻿namespace SportsStore.Components
{
    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}